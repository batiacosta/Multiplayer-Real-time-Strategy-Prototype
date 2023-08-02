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

    public UnitMovement GetUnitMovement() => unitMovement;
    public Targeter GetTargeter() => targeter;

    #region Server

    public override void OnStartServer()
    {
        base.OnStartServer();
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        ServerOnUnitDeSpawned?.Invoke(this);
    }

    #endregion

    #region Client
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isClientOnly || !isOwned) return;
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (!isClientOnly || !isOwned) return;
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
