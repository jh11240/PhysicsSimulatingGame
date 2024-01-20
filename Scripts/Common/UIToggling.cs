using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggling : MonoBehaviour
{
    public GameObject obj;
    private Button btn_toggle;

    private void Awake()
    {
        btn_toggle = GetComponent<Button>();
        btn_toggle.onClick.AddListener(()=> { Toggle(obj); });
    }

    public void Toggle(GameObject obj)
    {
        obj.SetActive(obj.activeInHierarchy ? false : true);
    }
}
