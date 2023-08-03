using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private Rigidbody rigidBody = null;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float launchForce = 10f;
    private void Start()
    {
        rigidBody.velocity = transform.forward * launchForce;
    }

    #region Server

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    #endregion

    #region Client

    

    #endregion
}
