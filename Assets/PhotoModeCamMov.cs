using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PhotoModeCamMov : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;

    float yaw = 0;
    float pitch = 0;

    public CinemachineVirtualCamera virtualCamera;

    // Update is called once per frame
    void Update()
    {
        if (virtualCamera.Priority > 1)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float upDown = 0f;

            if (Input.GetKey(KeyCode.Q)) upDown = 1;
            if (Input.GetKey(KeyCode.E)) upDown = -1;
            Vector3 move = new Vector3(horizontal, upDown, vertical);
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);

            if (Input.GetMouseButton(1))
            {
                yaw += lookSpeed * Input.GetAxis("Mouse X");
                pitch -= lookSpeed * Input.GetAxis("Mouse Y");

                pitch = Mathf.Clamp(pitch, -89f, 89f);
            }

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
