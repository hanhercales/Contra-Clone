using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speed;
    private ParallaxCamera parallaxCamera;

    private void Start()
    {
        parallaxCamera = FindObjectOfType<ParallaxCamera>();
        if (parallaxCamera != null)
        {
            parallaxCamera.onCameraTranslate += Move;
        }
    }

    void Move(float deltaMovement)
    {
        Vector3 newPosition = transform.position;
        newPosition.x += deltaMovement * speed;
        transform.position = newPosition;
    }
}
