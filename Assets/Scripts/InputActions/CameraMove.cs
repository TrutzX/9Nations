using System;
using Game;
using Tools;
using UnityEngine;

namespace InputActions
{
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

        public void MoveTo(NVector pos)
        {
            startPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            endPosition = new Vector3(pos.x,pos.y,transform.position.z);
        
            // Keep a note of the time the movement started.
            startTime = Time.time;
            counter = 0;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startPosition, endPosition);
        
            //change level?
            GameMgmt.Get().newMap.view.View(pos.level);
        }

        public void MoveBy(int x, int y)
        {
            MoveTo(new NVector((int)transform.position.x+x,(int)transform.position.y+y, GameMgmt.Get().newMap.view.ActiveLevel));
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
}
