using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class UnitBase : NetworkBehaviour
{
    public static event Action<int> ServerOnPlayerDied;
    public static event Action<UnitBase> ServerOnBaseSpawned;
    public static event Action<UnitBase> ServerOnBaseDeSpawned;
    
    [SerializeField] private Health healt = null;

    #region Server
    
    public override void OnStartServer()
    {
        healt.ServerOnDied += Health_ServerOnDied;
        healt.ClientOnHealthChanged += Health_ClientOnHealthChanged;
        ServerOnBaseSpawned?.Invoke(this);
    }
    
    public override void OnStopServer()
    {
        healt.ServerOnDied -= Health_ServerOnDied;
        healt.ClientOnHealthChanged -= Health_ClientOnHealthChanged;
        ServerOnBaseDeSpawned?.Invoke(this);
    }

    [Server]
    private void Health_ServerOnDied()
    {
        ServerOnPlayerDied?.Invoke(connectionToClient.connectionId);
        NetworkServer.Destroy(gameObject);
    }
    
    [Server]
    private void Health_ClientOnHealthChanged(int arg1, int arg2)
    {
        
    }

    #endregion

    #region Client



    #endregion
}
