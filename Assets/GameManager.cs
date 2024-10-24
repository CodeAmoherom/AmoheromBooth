using CA.PlayerController;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PoseThumbnailGenerator thumbnailGenerator;
    public GameObject GameUI;
    public PlayerMovement PlayerMovement;
    public CinemachineVirtualCamera FullCam;
    public CinemachineFreeLook ThirdPersonCam;
    public Camera MainCamera;

    public PlayerLocomotioninput Locomotioninput;
    public Transform playerTransform;


    bool isInCam = false;

    private void Update()
    {
        if (FullCam.Priority < 1)
        {
            FullCam.transform.position = MainCamera.transform.position;
            FullCam.transform.rotation = MainCamera.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.C) && !isInCam)
        {
            
            print("Camera Button Down");
            GameUI.SetActive(false);

            PlayerMovement.enabled = false;

            FullCam.Priority = 10;
            ThirdPersonCam.Priority = 0;

            Locomotioninput.enabled = false;

            

            isInCam = true;
        }
        else if(Input.GetKeyDown(KeyCode.C) && isInCam)
        {
            print("Leaving Cam");
            GameUI.SetActive(true);
            PlayerMovement.enabled = true;
            FullCam.Priority = 0;
            ThirdPersonCam.Priority = 10;
            Locomotioninput.enabled = true;

            isInCam = false;
        }


    }

    private void PositionCamera()
    {
        
    }

}
