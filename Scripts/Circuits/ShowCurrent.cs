using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowCurrent : MonoBehaviour
{
    public GameObject showAmparePrefab;
    [SerializeField]private TextMeshProUGUI currentVal;
    private void Awake()
    {
        currentVal = showAmparePrefab.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        showAmparePrefab.SetActive(false);
    }

    public void ShowAmpare(Vector3 pos, float current)
    {
        showAmparePrefab.SetActive(true);
        showAmparePrefab.transform.position = pos + Vector3.up*3;
        currentVal.text = current.ToString()+"A";
    }
    public void CloseWindow()
    {
        showAmparePrefab.SetActive(false);
    }
}
