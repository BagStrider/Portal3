using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Camera _camera;

    private Rigidbody _rb;

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
        float x = Input.GetAxis("Mouse X") * _rotationSpeed;
        float z = Input.GetAxis("Mouse Y") * _rotationSpeed;

        z = Mathf.Clamp(_camera.transform.rotation.eulerAngles.z, -90, 90);
        Debug.Log(_camera.transform.rotation.eulerAngles.z);
        _camera.transform.Rotate(-Vector3.right, z);
        transform.Rotate(transform.up, x);
    }

    private void MovementInput()
    {
        float x = Input.GetAxis("Horizontal") * _speed;
        float z = Input.GetAxis("Vertical") * _speed;

        _rb.AddForce((x * transform.right + transform.forward * z));
    }
    private void JumpInput()
    {
        if (Input.GetAxisRaw("Jump") == 1)
        {
            _rb.AddForce(transform.up * _speed / 2f, ForceMode.Impulse);
        }
    }
}
