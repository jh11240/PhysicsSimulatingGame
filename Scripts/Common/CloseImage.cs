using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseImage : MonoBehaviour
{
    private RectTransform imgRect;
    private Vector3 clickPos;

    public RectTransform btn_Hint;
    public RectTransform btn_Prob;
    private void Awake()
    {
        imgRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(btn_Hint, clickPos) || RectTransformUtility.RectangleContainsScreenPoint(btn_Prob, clickPos))
                return;
            if (!RectTransformUtility.RectangleContainsScreenPoint(imgRect, clickPos))
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
