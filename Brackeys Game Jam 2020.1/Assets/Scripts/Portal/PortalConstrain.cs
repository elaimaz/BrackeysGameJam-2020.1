using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalConstrain : MonoBehaviour
{
//    public float radius = 400f;
//    Vector3 centerPosition = transform.localPosition;
    public Vector2 center;
    public Vector2 position;
    public float maxDistance = 19;

    float actualDistance = 0;

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        actualDistance = Vector2.Distance(center, position);
//        if (distance > radius) //If the distance is less than the radius, it is already within the circle.
//         {
//         Vector3 fromOriginToObject = newLocation - centerPosition; //~GreenPosition~ - *BlackCenter*
//         fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
//         newLocation = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
//         }
    
        
        if (actualDistance > maxDistance)
        {
            Vector2 centerToPosition = position - center;
             centerToPosition.Normalize();
             transform.position = center + centerToPosition * maxDistance;
        }
    }
}
//
