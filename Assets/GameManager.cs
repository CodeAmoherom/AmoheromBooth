using CA.PlayerController;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PoseThumbnailGenerator thumbnailGenerator;
    public GameObject GameUI;
    public GameObject CameraUI;
    public PlayerMovement PlayerMovement;
    
    public CinemachineFreeLook ThirdPersonCam;

    public CinemachineVirtualCamera FullCam;
    public Camera MainCamera;

    public Vector3 PhotoCamRotationOffset = new Vector3(0, 180, 0);
    public Vector3 PhotoCamLocationOffset = new Vector3(0, 0, 0);

    public PlayerLocomotioninput Locomotioninput;
    public Transform playerTransform;

    public bool isLoading = false;
    public GameObject LoadingVideo;

    public bool isMouseCaptureOff = false;

    bool isInCam = false;

    public void ShowLoadingScreen()
    {
        LoadingVideo.gameObject.SetActive(true);
        Locomotioninput.enabled = false;
        PlayerMovement.enabled = false;
    }

    public void HideLoadingScreen()
    {
        LoadingVideo.gameObject.SetActive(false);
        Locomotioninput.enabled = true;
        PlayerMovement.enabled = true;
    }

    private void Update()
    {
        if (!FullCam.enabled)
        {
            FullCam.transform.position = 
                playerTransform.position + playerTransform.rotation * PhotoCamLocationOffset;
            FullCam.transform.rotation = 
                playerTransform.transform.rotation;
        }


        if (Input.GetKeyDown(KeyCode.C) && !isInCam)
        {
            
            print("Camera Button Down");
            GameUI.SetActive(false);

            PlayerMovement.enabled = false;

            FullCam.Priority = 10;
            ThirdPersonCam.Priority = 0;

            Locomotioninput.enabled = false;

            CameraUI.gameObject.SetActive(true);
            
            isInCam = true;
        }
        else if(Input.GetKeyDown(KeyCode.C) && isInCam)
        {
            print("Leaving Cam");
            GameUI.SetActive(true);
            
            FullCam.Priority = 0;
            ThirdPersonCam.Priority = 10;

            if (!isLoading)
            {
                PlayerMovement.enabled = true;
                Locomotioninput.enabled = true;
            }
            CameraUI.gameObject.SetActive(false);
            isInCam = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            print("Mouse Capture Off");
            isMouseCaptureOff = true;
            ThirdPersonCam.enabled = false;

        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            print("Mouse Capture On");
            isMouseCaptureOff = false;
            ThirdPersonCam.enabled = true;
        }

    }

    private void PositionCamera()
    {
        
    }

}
