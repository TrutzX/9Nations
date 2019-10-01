using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    // Transforms to act as start and end markers for the journey.
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Movement speed in units/sec.
    public float speed = 0.1F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private float counter;
    
    public static CameraMove Get()
    {
        return GameObject.FindObjectOfType<Camera>().GetComponent<CameraMove>();
    }

    public void MoveTo(int x, int y)
    {
        startPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        endPosition = new Vector3(x,y,transform.position.z);
        
        // Keep a note of the time the movement started.
        startTime = Time.time;
        counter = 0;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    public void MoveBy(int x, int y)
    {
        MoveTo((int)transform.position.x+1,(int)transform.position.y+1);
    }

    // Follows the target position like with a spring
    void Update()
    {
        //has pos?
        if (startTime == 0)
        {
            return;
        }

        counter += 0.1f;
        
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPosition, endPosition, counter);
        
        //Debug.LogWarning(transform.position);
        //reset?
        //if (startPosition.x == transform.position.x && )

        if (counter > 1)
        {
            startTime = 0;
        }
    }

    public bool IsMoving()
    {
        return startTime != 0;
    }
}
