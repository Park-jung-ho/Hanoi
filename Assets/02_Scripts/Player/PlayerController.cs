using System;
using BACKND;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Vector3 InputDir;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        InputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        InputDir *= moveSpeed;
        InputDir.y = rb.linearVelocity.y;
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
        if (InputDir != Vector3.zero) rb.linearVelocity = InputDir;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
