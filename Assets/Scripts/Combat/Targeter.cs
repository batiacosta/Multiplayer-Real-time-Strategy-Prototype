using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable _target;
    public Targetable GetTarget() => _target;
    
    #region Server

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += GameOverHandler_ServerOnGameOver;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver -= GameOverHandler_ServerOnGameOver;
    }
[Server]
    private void GameOverHandler_ServerOnGameOver()
    {
        ClearTarget();
    }

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
