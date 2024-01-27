using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoClip[] videoClips; // Assign your 8 video clips in the Inspector.
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = true;

        // Choose a random video clip from the array.
        int randomIndex = Random.Range(0, videoClips.Length);
        videoPlayer.clip = videoClips[randomIndex];

        // Play the chosen video.
        videoPlayer.Play();
    }
}
