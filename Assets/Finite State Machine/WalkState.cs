using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Transform enemy;
    public float chaseRange = 5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 2f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        enemy = animator.gameObject.GetComponent<Transform>();
        //dot product
        Vector3 dirOfEnemyToPlayer = player.position - enemy.position;
        Vector3 dirOfEnemy = enemy.forward;
        float dotProduct = Vector3.Dot(dirOfEnemy, dirOfEnemyToPlayer);

        float distanceToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);
        agent.SetDestination(player.position);
        if (distanceToPlayer > chaseRange) {
            animator.SetTrigger("Sneak_Cycle_1");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
