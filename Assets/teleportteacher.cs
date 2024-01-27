using UnityEngine;
using System.Collections;

public class DelayedCharacterMovement : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 0, 0); // Target position to move to
    private float delayTime = 33f; // Delay time in seconds
    public GameObject paperObject;

    // Start is called before the first frame update
    void Start()
    {


        // Start the coroutine to delay the movement
        StartCoroutine(DelayAndMoveCharacter());
    }

    IEnumerator DelayAndMoveCharacter()
    {



        // Wait for the specified delay time


        yield return new WaitForSeconds(delayTime);





        paperObject.SetActive(true);


        // Move the character to the target position
        transform.position = targetPosition;
    }
}
