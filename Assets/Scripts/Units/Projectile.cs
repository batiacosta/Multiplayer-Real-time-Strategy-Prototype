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
    [SerializeField] private int damage = 20;
    private void Start()
    {
        rigidBody.velocity = transform.forward * launchForce;
    }

    

    #region Server

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }
    
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
        {
            if (networkIdentity.connectionToClient == connectionToClient)
            {
                //  We hit our own unit
                return;
            }  
        }

        if (other.TryGetComponent<Health>(out Health healthComponent))
        {
            healthComponent.DealDamage(damage);
            DestroySelf();
        }
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
