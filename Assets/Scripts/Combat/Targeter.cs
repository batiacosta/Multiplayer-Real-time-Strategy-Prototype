using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable _target;
    public Targetable GetTarget() => _target;
    
    #region Server

    [Command] public void CmdSetTarget(GameObject targetGameObject)
    {
        if (!targetGameObject.TryGetComponent<Targetable>(out Targetable target)) return;
        _target = target;
    }
    
    [Server] public void ClearTarget()
    {
        _target = null;
    }

    #endregion

    #region Client

    

    #endregion
}
