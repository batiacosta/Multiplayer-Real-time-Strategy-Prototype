
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float chasingRange = 10f;
    
    #region Server
    [ServerCallback]
    private void Update()
    {

        Targetable target = targeter.GetTarget();
        if (target != null)
        {
            float distance = (target.transform.position - transform.position).sqrMagnitude;
            if (distance > chasingRange * chasingRange)
            {
                //  Chase target
                agent.SetDestination(target.transform.position);
            }
            else if(agent.hasPath)
            {
                agent.ResetPath();
            }
            return;
        }

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