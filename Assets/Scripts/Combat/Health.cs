using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public event Action ServerOnDied;
    public event Action<int, int> ClientOnHealthChanged; 
    
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
        ServerOnDied?.Invoke();
    }

    #endregion

    #region Client

    private void HealthChanged(int oldHealth, int currentHealth)
    {
        ClientOnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    #endregion
}
