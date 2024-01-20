using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject followTarget;
    private RectTransform rect;
    public float upwardPoint=1;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Follow()
    {
        rect.position = Camera.main.WorldToScreenPoint(followTarget.transform.position+Vector3.up*upwardPoint);
    }
    private void Update()
    {
        Follow();
    }
}
