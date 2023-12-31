using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;
    private TargetPlayer targetPlayerScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.position);
        targetPlayerScript = GetComponentInChildren<TargetPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player is nearby, start running towards them
        if(targetPlayerScript.target != null) {
            transform.LookAt(targetPlayerScript.target.transform);
            transform.position += transform.forward * agent.speed * 0.5f * Time.deltaTime;
        }
    }
}
