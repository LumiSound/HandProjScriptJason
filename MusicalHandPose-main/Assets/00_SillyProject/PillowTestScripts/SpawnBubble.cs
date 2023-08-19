using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    public ParticleSystem bubbleParticleSystem;
    public float movementThreshold = 0.01f;

    private Vector3 lastPosition;
    private bool isMoving;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // Check if the distance moved exceeds the movement threshold
        if (distanceMoved >= movementThreshold)
        {
            // Object is moving, enable particle system
            if (!isMoving)
            {
                bubbleParticleSystem.Play();
                isMoving = true;
            }
        }
        else
        {
            // Object is not moving, disable particle system
            if (isMoving)
            {
                bubbleParticleSystem.Stop();
                isMoving = false;
            }
        }

        // Update the last position for the next frame
        lastPosition = transform.position;
    }
}