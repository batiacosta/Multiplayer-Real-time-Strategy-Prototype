using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public event Action Server_OnDie; 
    
    [SerializeField] private int maxHealth = 100;

    #region Server

    [SyncVar] private int _currentHealth;

    public override void OnStartServer()
    {
        _currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damage)
    {
        if (_currentHealth == 0) return;
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        if (_currentHealth != 0) return;
        Server_OnDie?.Invoke();
        Debug.Log($"{gameObject.name} dies");
    }

    #endregion

    #region Client



    #endregion
}
