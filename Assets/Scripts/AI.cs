using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    private Animator animator;
    public GameObject[] waypointsArray;
    [SerializeField] private float maxPursuitDistance = 10.0f;
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("The NavMeshAgent is not on a NavMesh. Please ensure the agent starts on a valid NavMesh surface.");
            return;
        }
    }
    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    void Pursuit()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.TransformVector(targetDir), this.transform.forward);
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if (target.GetComponent<vThirdPersonController>().moveSpeed < 0.01f ) {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<vThirdPersonController>().moveSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }
    private void Update()
    {
        Vector3 toPlayer = target.transform.position - transform.position;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        float dotProduct = Vector3.Dot(toPlayer.normalized, transform.forward);
        if (agent != null && distance <= maxPursuitDistance && distance > 2 && dotProduct > 0)
        {
            Seek(target.transform.position);
            animator.SetTrigger("Walk_Cycle_1");
        } else
        {
            agent.ResetPath();
            animator.SetTrigger("Sleep");
        }
        if (distance <= 2)
        {
            animator.SetTrigger("Attack_1");
        }
    }
}
