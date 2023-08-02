using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Targeter targeter = null;
    
    #region Server
    [ServerCallback]
    private void Update()
    {
        if (!agent.hasPath) return;
        if (agent.remainingDistance > agent.stoppingDistance) return;
        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        targeter.ClearTarget();
        if (!NavMesh.SamplePosition(position, out NavMeshHit navMeshHit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(navMeshHit.position);
    }
    #endregion

    #region Client
    

    #endregion
    
}