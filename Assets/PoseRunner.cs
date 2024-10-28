using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseRunner : MonoBehaviour
{
    public PoseThumbnailGenerator PoseGenRef;
    public bool isAnipose = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayPoseAttached()
    {
        StartCoroutine(PlayPoseAndWait());
    }

    private IEnumerator PlayPoseAndWait()
    {
        // Play the pose animation
        PoseGenRef.animator.Play(PoseGenRef.poseName);

        // Continuously check for movement input during the pose animation
        while (true) // Keep the loop running until we exit it
        {
            print("InputCheckRunning");
            // Check for movement input (e.g., WASD or arrow keys)
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Movement input detected, immediately switch to "Locomotion"
                PoseGenRef.animator.StopPlayback();
                break; // Exit the coroutine once Locomotion is triggered
            }

            if (isAnipose)
            {
                // Get the current animation's length and check if it's still playing
                AnimatorStateInfo stateInfo = PoseGenRef.animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.normalizedTime >= 1f) // Check if the animation has finished
                {
                    break; // Exit the loop if the animation has completed
                }
            }
            yield return null;
        }
        print("Input Check Off");
        PoseGenRef.animator.Play("Locomotion");
        yield return null;
    }

}
