using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SneakState : StateMachineBehaviour
{
    List<Transform> waypoints =  new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    Transform enemy;
    public float chaseRange = 5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.gameObject.GetComponent<Transform>();

        GameObject[] listWaypoints = GameObject.FindGameObjectsWithTag("WayPoints");
        foreach (GameObject waypoint in listWaypoints)
        {
            waypoints.Add(waypoint.transform);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //dot product
        Vector3 dirOfEnemyToPlayer = player.position - enemy.position;
        Vector3 dirOfEnemy = enemy.forward;
        float dotProduct = Vector3.Dot(dirOfEnemy, dirOfEnemyToPlayer);

        float distanceToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);

        if (dotProduct > 0 && distanceToPlayer < chaseRange)
        {
            animator.SetTrigger("Walk_Cycle_1");
        } else {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
