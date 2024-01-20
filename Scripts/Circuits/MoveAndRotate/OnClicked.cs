using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnClicked : MonoBehaviour
{
    private bool isObjMoving;
    private bool isFirstTouch;
    private Collider2D objCollider2D;
    private CircuitNode[] circuitNodes;
    public GameObject btn_EditObj;
    public GameObject UIInputValue;
    public TMP_InputField inputValue;
    private RectTransform btn_EditObjRect;

    private CircuitNodeManager circuitNodeManager;

    private void Awake()
    {
        isFirstTouch = true;
        isObjMoving = false;
        objCollider2D= GetComponent<Collider2D>();
        btn_EditObjRect = btn_EditObj.GetComponent<RectTransform>();
        circuitNodes = GetComponentsInChildren<CircuitNode>();
        circuitNodeManager = GetComponent<CircuitNodeManager>();
    }

    /// <summary>
    /// 버튼에 직접 onclick에 달아줌 움직이게 하기
    /// </summary>
    public void SetObjectMoving()
    {
        isObjMoving = true;
        btn_EditObj.SetActive(false);
    }
    
    /// <summary>
    /// 버튼에 직접 onclick에 달아줌 회전시키기
    /// </summary>
    public void SetObjectRotate()
    {
        gameObject.transform.Rotate(new Vector3(0,0,90));
        btn_EditObj.SetActive(false);

    }

    public void SetObjectSetValue()
    {
        string value = inputValue.text;
        int val = int.Parse(value);
        foreach(CircuitNode node in circuitNodes)
        node.Resistance = val;
        UIInputValue.SetActive(false);
    }

    public void SetObjectDelete()
    {
        Destroy(gameObject);
    }

    public void InputValueSetActive()
    {
        UIInputValue.SetActive(true);
    }
    private void Update()
    {
        if (!isObjMoving && Input.GetMouseButtonDown(0))
        {
            //만약 colliider의 반지름보다 크면 바깥을 터치한것이므로 리턴
            if (Vector3.Distance((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.position) > objCollider2D.bounds.size.x / 2)
            {
                //만약 btn_edit이 꺼져있다면 return
                if (!btn_EditObj.activeInHierarchy) return;
                //켜져있고 해당 부분제외하고 다른부분 터치중이라면
                if (!RectTransformUtility.RectangleContainsScreenPoint(btn_EditObjRect, Input.mousePosition))
                {
                    btn_EditObj.SetActive(false);
                    return;
                }
                return;
            }

            //맨 처음 놓을때 클릭처리됨
            if (isFirstTouch)
            {
                isFirstTouch = false;
                return;
            }
            btn_EditObj.SetActive(true);
        }

        if (!isObjMoving) return;

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            isObjMoving = false;
        }
    }
}
