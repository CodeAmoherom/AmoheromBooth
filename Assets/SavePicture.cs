using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePicture : MonoBehaviour
{
    public Camera Camera;

    public int CameraWidth = 3840;
    public int CameraHeight = 2160;
    public int CameraBitDepth = 32;

    public void SaveCameraView()
    {
        RenderTexture screenTexture = new RenderTexture(CameraWidth, CameraHeight, CameraBitDepth);
        screenTexture.antiAliasing = 16;
        Camera.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        Camera.Render();

        Texture2D renderedTexture = new Texture2D(CameraWidth, CameraHeight, TextureFormat.RGBA32, false);
        renderedTexture.filterMode = FilterMode.Trilinear;
        renderedTexture.ReadPixels(new Rect(0, 0, CameraWidth, CameraHeight), 0,0);
        RenderTexture.active = null;

        byte[] pixlArray = renderedTexture.EncodeToPNG();

        string path = Path.Combine(Application.dataPath, "Camera");
        string filePath = Path.Combine(path, DateTime.UtcNow.ToString("dd MM HH mm ss ffff") + ".png");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllBytes(filePath, pixlArray);
        Camera.targetTexture = null;
        screenTexture.Release();
        Destroy(renderedTexture );
        Destroy(screenTexture );
        Resources.UnloadUnusedAssets();
    }
}
