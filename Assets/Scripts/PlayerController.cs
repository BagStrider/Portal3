using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight;

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _mouseAcceleration;
    [SerializeField] private float _minUp;
    [SerializeField] private float _maxUp;

    private CharacterController _characterController;
    private float _xRotation;
    private Vector3 _velocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseInput();

        if(_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = (transform.right * x + transform.forward * z) * _speed;

        if(Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move((_velocity + motion) * Time.deltaTime);
    }

    private void MouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseAcceleration;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseAcceleration;

        _xRotation -= mouseY;

        _camera.transform.localRotation = Quaternion.Euler(_xRotation,0,0);
        _xRotation= Mathf.Clamp(_xRotation, _minUp, _maxUp);

        transform.Rotate(Vector3.up * mouseX);
    }


}
