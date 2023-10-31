using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    public Transform patrolRoute;
    public Transform player;
    public List<Transform> locations;
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int Lives = 3;

    public int EnemyLives
    {
        get
        {
            return Lives;

        }
        set
        {
            Lives = value;
            if (Lives <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
        private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        if(agent.remainingDistance <0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    public void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    public void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            agent.destination = player.position;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("Player Is Out Of Range");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("BULLET LG GAI");
        }
    }
}
