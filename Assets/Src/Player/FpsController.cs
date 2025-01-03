using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsController : MonoBehaviour
{
    public float lookXSens = 0.5f;
    public float lookYSens = 2f;
    public Transform playerBody;

    private Vector2 gamepadInput;
    private float xRotation = 0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        LookHandle();
    }

    private void LookHandle()
    {
        gamepadInput = Gamepad.current != null ? Gamepad.current.rightStick.ReadValue() : Vector2.zero;

        // Rotasi vertikal (pitch) untuk kamera
        xRotation -= gamepadInput.y * lookYSens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotasi horizontal (yaw) untuk karakter
        float horizontal = gamepadInput.x * lookXSens;

        // Hanya rotasi sumbu Y (tidak mengubah posisi)
        playerBody.localRotation = Quaternion.Euler(0f, playerBody.localRotation.eulerAngles.y + horizontal, 0f);
    }
}
