using UnityEngine;

public class ObjectMovementAudioTrigger : MonoBehaviour
{
    public float movementThreshold = 0.1f; // Threshold for movement detection
    private Vector3 lastPosition; // To store the last position of the object

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position; // Initialize the last known position
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object has moved more than the threshold
        if (Vector3.Distance(lastPosition, transform.position) > movementThreshold)
        {
            // Find the NPC or mesh with the "Teacher" tag
            GameObject teacherObject = GameObject.FindGameObjectWithTag("Teacher");
            if (teacherObject != null)
            {
                // Try to find the audio source named "apple" within the teacherObject
                AudioSource appleAudio = teacherObject.GetComponentInChildren<AudioSource>(true);
                if (appleAudio != null && appleAudio.clip != null && appleAudio.clip.name == "apple")
                {
                    appleAudio.Play(); // Play the audio clip
                }
            }

            lastPosition = transform.position; // Update the last known position
        }
    }
}
