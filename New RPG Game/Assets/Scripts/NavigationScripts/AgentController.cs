using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

        //agent settings here
        agent.enabled = false;

        //obstical settings here
        obstacle.enabled = true;
        obstacle.shape = NavMeshObstacleShape.Capsule;
        obstacle.carving = true;
    }
    
    //When combat starts only the active entity in the world will be set to active and allowed to move all other entitys must be statonary and have apropirate collison to avoid.
    public void AgentIsActive()
    {
        obstacle.enabled = false;
        obstacle.carving = false;

        agent.enabled = true;
    }

    public void AgentInactive()
    {
        agent.enabled = false;
        obstacle.enabled = true;
        obstacle.carving = true;
    }
}
