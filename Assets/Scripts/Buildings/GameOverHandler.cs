using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameOverHandler : NetworkBehaviour
{

    private List<UnitBase> _bases = new List<UnitBase>();
    #region Server

    public override void OnStartServer()
    {
        UnitBase.ServerOnBaseSpawned += UnitBase_ServerOnBaseSpawned;
        UnitBase.ServerOnBaseDeSpawned += UnitBase_ServerOnBaseDeSpawned;
    }

    public override void OnStopServer()
    {
        UnitBase.ServerOnBaseSpawned -= UnitBase_ServerOnBaseSpawned;
        UnitBase.ServerOnBaseDeSpawned -= UnitBase_ServerOnBaseDeSpawned;
    }

    [Server]
    private void UnitBase_ServerOnBaseSpawned(UnitBase unitBase)
    {
        _bases.Add(unitBase);
    }

    [Server]
    private void UnitBase_ServerOnBaseDeSpawned(UnitBase unitBase)
    {
        _bases.Remove(unitBase);

        if (_bases.Count == 1)
        {
            Debug.Log("Game Over");
        }
    }

    #endregion

    #region Client
    

    #endregion
}
