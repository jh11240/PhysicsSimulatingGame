using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject ball;


    private void LateUpdate()
    {
        if(ball.transform.position.x > 10)
        transform.position = new Vector3(ball.transform.position.x, transform.position.y, transform.position.z);
    }
}
