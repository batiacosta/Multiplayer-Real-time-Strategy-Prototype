using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDeSpawned;
    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDeSpawned;
    
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;
    [SerializeField] private UnitMovement unitMovement = null;
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private Health health = null;

    public UnitMovement GetUnitMovement() => unitMovement;
    public Targeter GetTargeter() => targeter;

    #region Server

    public override void OnStartServer()
    {
        base.OnStartServer();
        ServerOnUnitSpawned?.Invoke(this);
        health.ServerOnDied += Health_OnDied;
    }

    
    public override void OnStopServer()
    {
        base.OnStopServer();
        ServerOnUnitDeSpawned?.Invoke(this);
        health.ServerOnDied -= Health_OnDied;
    }
    [Server]
    private void Health_OnDied()
    {
        NetworkServer.Destroy(gameObject);
    }


    #endregion

    #region Client
    
    public override void OnStartAuthority()
    {
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!isOwned) return;
        AuthorityOnUnitDeSpawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if(!isOwned) return;
        onSelected?.Invoke();
    }
    [Client]
    public void Deselect()
    {
        if(!isOwned) return;
        onDeselected?.Invoke();
    }
    
    #endregion

    
}
