using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public event Action Server_OnDied;
    public event Action<int, int> Client_OnHealthChanged; 
    
    [SerializeField] private int maxHealth = 100;

    #region Server

    [SyncVar(hook = nameof(HealthChanged))] private int _currentHealth;

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
        Server_OnDied?.Invoke();
        Debug.Log($"{gameObject.name} dies");
    }

    #endregion

    #region Client

    private void HealthChanged(int oldHealth, int currentHealth)
    {
        Client_OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    #endregion
}
