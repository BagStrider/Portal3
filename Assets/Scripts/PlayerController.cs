using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _mouseAcceleration = 3f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _minUp;
    [SerializeField] private float _maxUp;

    private Rigidbody _rb;
    private float _xRotation;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MouseInput();

        MovementInput();
     
        JumpInput();
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
    private void MovementInput()
    {
        Vector3 x = Input.GetAxis("Horizontal") * transform.right;
        Vector3 y = Input.GetAxis("Vertical") * transform.forward;

        _rb.AddForce((x + y).normalized * _speed);
    }

    private void JumpInput()
    {
        if (Input.GetAxisRaw("Jump") == 1)
        {
            _rb.AddForce(transform.up * _speed / 2f, ForceMode.Impulse);
        }
    }
}
