using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;
    [SerializeField] private UnitMovement unitMovement = null;

    #region Server

    

    #endregion

    #region Client

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

    public UnitMovement GetUnitMovement() => unitMovement;
}
