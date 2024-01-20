using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AblelDrawing : MonoBehaviour
{
    private Button btn_ableToDraw; 
    public GameObject DrawLine;

    public void Awake()
    {
        btn_ableToDraw = GetComponent<Button>();
        btn_ableToDraw.onClick.AddListener(AbleDrawing);
    }

    private void AbleDrawing()
    {
        DrawLine.SetActive(true);
    }

    private void OnDisable()
    {
        btn_ableToDraw.onClick.RemoveListener(AbleDrawing);

    }
}
