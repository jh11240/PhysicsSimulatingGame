using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchedUIObject : MonoBehaviour
{
    public GameObject Drawline;
    public GameObject objToCopySrc;
    private Button btn_Instantiate;
    public Transform circuitObjects;
    public RectTransform notInstantiatingArea;

    private bool isObjMoving=false;
    private GameObject objToSet;

    private void Awake()
    {
        btn_Instantiate = GetComponent<Button>();
        btn_Instantiate.onClick.AddListener(onClicked);
    }

    public void onClicked()
    {
        Drawline.SetActive(false);
        InstantiateCircuitObj();
        isObjMoving = true;
    }

    public void InstantiateCircuitObj()
    {
        objToSet=Instantiate(objToCopySrc, Vector3.zero,Quaternion.identity,circuitObjects);
    }

    private void Update()
    {
        if (!isObjMoving) return;

        objToSet.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objToSet.transform.position=new Vector3(objToSet.transform.position.x,objToSet.transform.position.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(notInstantiatingArea,Input.mousePosition))
            {
                isObjMoving = false;
            }
        }
    }
    private void OnDisable()
    {
        btn_Instantiate.onClick.RemoveListener(onClicked);
    }
}
