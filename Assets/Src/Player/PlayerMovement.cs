using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float runSpeed = 5f;

    public float rayDistance = 1.2f;
    public float jumpForce = 5f;
    public Transform playerBody;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        Debug.Log($"Is Grounded: {isGrounded}");
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector2 gamePadLeftStick = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() : Vector2.zero;
        Vector3 move = new Vector3(gamePadLeftStick.x, 0f, gamePadLeftStick.y);

        float run = (Gamepad.current != null && Gamepad.current.leftShoulder.isPressed) ? runSpeed : 0;

        float finalSpeed = movementSpeed + run;
        playerBody.transform.Translate(move * finalSpeed * Time.deltaTime, Space.World);
    }

    private void HandleJump()
    {
        // isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance);

        if (Gamepad.current != null && Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
