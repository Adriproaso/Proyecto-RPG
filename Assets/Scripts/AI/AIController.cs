using UnityEngine;

public enum NPCs
{
    Companion,
    Roam,
    Stand
}

public class AIController : MonoBehaviour
{
    [SerializeField]
    NPCs npc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (npc == NPCs.Companion)
        {
            GetComponent<Movement>().MoveTo(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
