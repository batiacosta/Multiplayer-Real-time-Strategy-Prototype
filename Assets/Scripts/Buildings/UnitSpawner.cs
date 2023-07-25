using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform placeToSpawn = null;

    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unit = Instantiate(unitPrefab, placeToSpawn.position, placeToSpawn.rotation);
        
        NetworkServer.Spawn(unit, connectionToClient);
    }

    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (!isOwned) return;   //  I'm the client who owns this event and not another client
        
        CmdSpawnUnit();
    }

    #endregion

    
}
