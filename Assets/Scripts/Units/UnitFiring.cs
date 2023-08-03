using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class UnitFiring : NetworkBehaviour
{
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSpwanTransform = null;
    [SerializeField] private float fireRange = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float rotationSpeed = 20f;

    private float _lastFiredTime;
    

    #region Server

    [ServerCallback] private void Update()
    {
        Targetable target = targeter.GetTarget();
        if (target == null) return;
        if (!CanFire()) return;
        Debug.Log("Fuego!!");
        Vector3 deltaPosition = target.transform.position - transform.position;
        Quaternion targetRotation =
            Quaternion.LookRotation(deltaPosition);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime);
        if (Time.time > (1 / fireRate) + _lastFiredTime)
        {
            Debug.Log("⁄⁄⁄Fuego x 1!!!");
            Vector3 targetPosition = target.GetAimAtPoint().position;
            Quaternion projectileOrientation = Quaternion.LookRotation(targetPosition - projectileSpwanTransform.position);
            GameObject projectileInstance = Instantiate(
                projectilePrefab, 
                projectileSpwanTransform.position, projectileOrientation);
            NetworkServer.Spawn(projectileInstance, connectionToClient);
            _lastFiredTime = Time.time;
        }
    }

    [Server]
    private bool CanFire()
    {
        float distance = (targeter.GetTarget().transform.position - transform.position).sqrMagnitude;
        if (distance <= (fireRange * fireRange))
        {
            Debug.Log("Puede disparar");
        }
        else
        {
            Debug.Log("Pailas");
        }
        
        return (distance <= (fireRange * fireRange));
    }

    #endregion

    #region Client



    #endregion
}
