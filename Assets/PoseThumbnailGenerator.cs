using CA.PlayerController;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PoseThumbnailGenerator : MonoBehaviour
{
    public PlayerAnimation PlayerBaseAnimator;
    public Camera thumbnailCam;
    public RenderTexture renderTexture;
    public Animator animator;
    public Image thumbnailImage;

    private Texture2D texture2D;

    public string poseName;
    public float DelaySeconds = 0f;

    public void CaptureThumbnail()
    {
        animator = PlayerBaseAnimator._animator;
        if (animator != null)
        {
            // Play the pose animation
            animator.Play(poseName);
        }

        // Start the coroutine to capture the pose with a delay
        StartCoroutine(CaptureRoutine(poseName, DelaySeconds));
    }

    private IEnumerator CaptureRoutine(string posename, float delaySecs)
    {
        // Wait for the specified delay before capturing the thumbnail
        yield return new WaitForSeconds(delaySecs);

        // Set up the texture to capture the image from the render texture
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;

        // Capture the image
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        // Convert it to a sprite for the thumbnail
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        thumbnailImage.sprite = sprite;

        // Save the image as a PNG
        byte[] bytes = texture2D.EncodeToPNG();
        string path = Application.dataPath + "/Thumbnails/";
        string filePath = path + "Thumbnail_" + posename + ".png";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllBytes(filePath, bytes);

        // Clear the render texture
        RenderTexture.active = null;

        // After the capture, play the "Locomotion" animation
        animator.Play("Locomotion");
    }

}
