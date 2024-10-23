using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PoseThumbnailGenerator : MonoBehaviour
{
    public Camera thumbnailCam;
    public GameObject model;
    public RenderTexture renderTexture;
    public Animator animator;
    public Image thumbnailImage;

    private Texture2D texture2D;

    public void CaptureTumbnail(string poseName)
    {
        animator.Play(poseName);
        StartCoroutine(CaptureRoutine(poseName));
    }

    private IEnumerator CaptureRoutine(string posename)
    {
        yield return new WaitForEndOfFrame();

        animator = model.GetComponentInChildren<Animator>();

        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        Sprite sprite = Sprite.Create(texture2D, new Rect(0,0, texture2D.width, texture2D.height), new Vector2(0.5f,0.5f));
        thumbnailImage.sprite = sprite;

        byte[] bytes = texture2D.EncodeToPNG();
        string path = Application.dataPath + "/Thumbnails/";
        string filePath = path + "Thumbnail_" + posename + ".png";


        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllBytes(filePath, bytes);


        RenderTexture.active = null;
    }
}
