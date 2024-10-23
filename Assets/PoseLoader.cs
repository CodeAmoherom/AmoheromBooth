using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseLoader : MonoBehaviour
{
    public GameObject PoseButtonContainer;
    // Start is called before the first frame
    void Start()
    {
        StartCoroutine(CaptureAllPoses());
    }

    public IEnumerator CaptureAllPoses()
    {
        // Loop through each button in the container
        foreach (Transform buttonTransform in PoseButtonContainer.transform)
        {
            // Get the PoseThumbnailGenerator component attached to the button
            PoseThumbnailGenerator poseThumbnailGenerator = buttonTransform.GetComponent<PoseThumbnailGenerator>();

            if (poseThumbnailGenerator != null)
            {
                // Capture the thumbnail for this pose
                poseThumbnailGenerator.CaptureThumbnail();
                print($"Capturing POSE: {poseThumbnailGenerator.poseName}");

                // Wait for the thumbnail to be captured before moving to the next (based on its delay)
                yield return new WaitForSeconds(poseThumbnailGenerator.DelaySeconds);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
