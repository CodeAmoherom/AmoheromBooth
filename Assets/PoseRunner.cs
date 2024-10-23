using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseRunner : MonoBehaviour
{
    public PoseThumbnailGenerator PoseGenRef;
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
        while (true)
        {
            // Check for movement input (e.g., WASD or arrow keys)
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Movement input detected, immediately switch to "Locomotion"
                PoseGenRef.animator.Play("Locomotion");
                yield break; // Exit the coroutine once Locomotion is triggered
            }

            // Keep checking on the next frame
            yield return null;
        }

        // Get the current animation's length
        AnimatorStateInfo stateInfo = PoseGenRef.animator.GetCurrentAnimatorStateInfo(0);

        // Wait for the animation to finish
        yield return new WaitForSeconds(stateInfo.length);

        // After the pose animation finishes, play "Locomotion"
        PoseGenRef.animator.Play("Locomotion");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
