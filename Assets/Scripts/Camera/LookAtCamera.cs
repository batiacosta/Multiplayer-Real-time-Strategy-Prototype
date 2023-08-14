using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        var cameraRotation = _mainCameraTransform.rotation;
        transform.LookAt(
            transform.position + cameraRotation * Vector3.forward,
            cameraRotation * Vector3.up
            );
    }
}
