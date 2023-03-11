using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ScriptableObjects;
using StateControllers;
using States;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraControlSO _config;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera; 
    [SerializeField] private Transform _cameraTarget;

    private PlayerInput _playerInput;
    private float _mouseX, _mouseY;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _cinemachineCamera.enabled = true;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void LateUpdate()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        Vector2 mouseDelta = _playerInput.KeyboardMouse.MouseLook.ReadValue<Vector2>();
        
        _cameraTarget.transform.rotation *= Quaternion.AngleAxis(mouseDelta.x * _config.MouseSensitivity, Vector3.up);
        _cameraTarget.transform.rotation *= Quaternion.AngleAxis(-mouseDelta.y * _config.MouseSensitivity, Vector3.right);
            
        var angles = _cameraTarget.transform.localEulerAngles;
        angles.z = 0;
             
        //Clamping the Up/Down rotation
        if (angles.x > 180 && angles.x < _config.UpAngleClamp) angles.x = _config.UpAngleClamp;
        else if(angles.x < 180 && angles.x > _config.DownAngleClamp) angles.x = _config.DownAngleClamp;
            
        transform.rotation = Quaternion.Euler(0, _cameraTarget.transform.rotation.eulerAngles.y, 0);
        _cameraTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}