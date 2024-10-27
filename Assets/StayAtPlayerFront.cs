using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StayAtPlayerFront : MonoBehaviour
{
    public Camera PoseCamera;
    public GameObject Player;

    public Vector3 CamOffset = new Vector3(0, 0.82f, 3.58f);
    public Vector3 CamRotOffset = new Vector3(0, 180,0);
    // Start is called before the first frame update
    void Start()
    {
        PoseCamera.transform.position = Player.transform.position + Player.transform.rotation * CamOffset;
        PoseCamera.transform.rotation = Player.transform.rotation * Quaternion.Euler(CamRotOffset);
    }

    // Update is called once per frame
    void Update()
    {
        PoseCamera.transform.position = Player.transform.position + Player.transform.rotation * CamOffset;
        PoseCamera.transform.rotation = Player.transform.rotation * Quaternion.Euler(CamRotOffset);
    }
}
