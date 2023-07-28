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

    #region Server

    

    #endregion

    #region Client

    [Client]
    public void Select()
    {
        onSelected?.Invoke();
    }
    [Client]
    public void Deselect()
    {
        onDeselected?.Invoke();
    }

    #endregion
}
