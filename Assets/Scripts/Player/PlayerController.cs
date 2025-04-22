using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using RPG.Dialogue;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float maxNavMeshProjectionDistance = 1f;

    [HideInInspector]

    private void Update()
    {   
        if(InteractWithUI()) return;
        if(InteractWithComponent()) return;
        if(InteractWithMovement()) return;
    }

    private bool InteractWithUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    private Ray GetMouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

    private bool InteractWithComponent()
    {
        RaycastHit[] hits = RaycastAllSorted();
        foreach (RaycastHit hit in hits)
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if (raycastable.HandleRaycast(this))
                {
                    return true;
                }
            }
        }
        return false;
    }

    RaycastHit[] RaycastAllSorted()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }
        Array.Sort(distances, hits);
        return hits;
    }

    private bool InteractWithMovement()
    {
        Vector3 target;
        bool hasHit = RaycastNavMesh(out target);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<NavMeshAgent>().stoppingDistance = 0f;
                GetComponent<Movement>().MoveTo(target);
            }
            return true;
        }
        return false;
    }

    private bool RaycastNavMesh(out Vector3 target)
    {
        target = new Vector3();

        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (!hasHit) return false;

        NavMeshHit navMeshHit;
        bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
        if (!hasCastToNavMesh) return false;

        target = navMeshHit.position;

        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
        if (!hasPath) return false;
        if (path.status != NavMeshPathStatus.PathComplete) return false;

        return true;
    }

}
