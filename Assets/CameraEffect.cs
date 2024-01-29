using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychedelicCameraEffect : MonoBehaviour
{
    public float fovChangeSpeed = 5f;
    public float colorChangeSpeed = 1f;

    private Camera cam;
    private float initialFOV;
    private float time;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialFOV = cam.fieldOfView;
    }

    void Update()
    {
        // Change field of view for a zooming effect
        cam.fieldOfView = initialFOV + Mathf.Sin(time * fovChangeSpeed) * 10;

        // Cycle through colors
        cam.backgroundColor = Color.HSVToRGB(Mathf.Sin(time * colorChangeSpeed), 1, 1);

        time += Time.deltaTime;
    }
}
