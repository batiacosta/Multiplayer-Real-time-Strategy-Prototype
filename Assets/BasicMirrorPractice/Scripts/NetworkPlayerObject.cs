using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NetworkPlayerObject : NetworkBehaviour
{
    [SerializeField] private TextMeshPro nameText = null;
    [SerializeField] private Renderer playerRenderer = null;
    
    [SyncVar (hook = nameof(SetNameText))] 
    [SerializeField] private string displayName = "Missing name";
    [SyncVar (hook = nameof(SetColor))]
    [SerializeField] private Color playerColor = Color.blue;
    
    #region Sever

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetPlayerColor(Color color)
    {
        playerColor = color;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayname)
    {
        if (newDisplayname.Length < 2) return;
        
        RpcLogNewName(newDisplayname);
        SetDisplayName(newDisplayname);
    }
    
    #endregion

    #region Client

    private void SetColor(Color oldColor, Color color)
    {
        playerRenderer.material.color = color;
    }
    private void SetNameText(string oldPlayerName, string playerName)
    {
        nameText.text = playerName;
    }

    [ContextMenu("Set MyName")]
    private void SetMyName()
    {
        CmdSetDisplayName("My new name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
    

    

}
