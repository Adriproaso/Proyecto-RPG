using UnityEngine;
using UnityEngine.AI;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField]
        Dialogue dialogue;
        [SerializeField]
        string conversantName;

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null) return false;

            if (Input.GetMouseButtonDown(0))
            {
                var distance = Vector3.Distance(callingController.gameObject.transform.position, transform.position);
                if (distance > 5)
                {
                    callingController.GetComponent<NavMeshAgent>().stoppingDistance = 5f;
                    callingController.GetComponent<Movement>().MoveTo(this.transform.position);
                }
                if (distance <= 5)
                {
                    callingController.transform.LookAt(this.transform);
                    //callingController.transform.rotation = Quaternion.Slerp(callingController.transform.rotation, Quaternion.LookRotation((transform.position - callingController.transform.position).normalized), Time.deltaTime * 5f);
                    callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
                }
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }

    }
}
