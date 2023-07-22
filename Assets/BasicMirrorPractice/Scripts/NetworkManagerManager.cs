using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        NetworkPlayerObject player = conn.identity.GetComponent<NetworkPlayerObject>();
        player.SetDisplayName($"Player {numPlayers}");
        player.SetPlayerColor(SetColor());
    }
    private Color SetColor()
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        Color newColor = new Color(r, g, b, 1f);
        return newColor;
    }
}
