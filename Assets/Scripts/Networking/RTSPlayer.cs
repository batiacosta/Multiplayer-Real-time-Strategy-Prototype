using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField]private List<Unit> _units = new List<Unit>();

    #region Server

    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += Unit_ServerOnUnitSpawned;
        Unit.ServerOnUnitDeSpawned += UnitServerOnUnitDeSpawned;
    }

    

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= Unit_ServerOnUnitSpawned;
        Unit.ServerOnUnitDeSpawned -= UnitServerOnUnitDeSpawned;
    }
    private void Unit_ServerOnUnitSpawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
        
        _units.Add(unit);
    }
    private void UnitServerOnUnitDeSpawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
        
        _units.Remove(unit);
    }
    
    
    #endregion

    #region Client

    public override void OnStartClient()
    {
        if (!isClientOnly) return;
        
        Unit.AuthorityOnUnitSpawned += Unit_AuthorityOnUnitSpawned;
        Unit.AuthorityOnUnitDeSpawned += Unit_AuthorityOnUnitDeSpawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) return;
        
        Unit.AuthorityOnUnitSpawned -= Unit_AuthorityOnUnitSpawned;
        Unit.AuthorityOnUnitDeSpawned -= Unit_AuthorityOnUnitDeSpawned;
    }

    private void Unit_AuthorityOnUnitSpawned(Unit unit)
    {
        if(!isOwned) return;
        _units.Add(unit);
    }
    private void Unit_AuthorityOnUnitDeSpawned(Unit unit)
    {
        if(!isOwned) return;
        _units.Remove(unit);
    }

    

    #endregion
}
