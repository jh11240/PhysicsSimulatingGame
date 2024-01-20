using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetAllObject : MonoBehaviour
{
    public Transform circuitObjs;
    public Transform Lines;
    private Button Btn_reset;
    public CircuitNodeManager circuitNodeManager;

    private void Awake()
    {
        Btn_reset=GetComponent<Button>();
        Btn_reset.onClick.AddListener(reset);
    }

    private void reset()
    {
        circuitNodeManager.CircuitStarted = false;
        circuitNodeManager.RSum = 0;
        foreach(Transform children in circuitObjs)
        {
            Destroy(children.gameObject);
        }
        foreach(Transform children in Lines)
        {
            Destroy(children.gameObject);
        }
    }
}
