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
        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
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
        File.WriteAllBytes(Application.dataPath + "/Thumbnails/" + "PoseThumbnail.png", bytes);

        RenderTexture.active = null;
    }
}
