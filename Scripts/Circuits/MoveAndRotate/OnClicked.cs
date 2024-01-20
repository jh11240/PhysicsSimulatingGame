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
    /// ��ư�� ���� onclick�� �޾��� �����̰� �ϱ�
    /// </summary>
    public void SetObjectMoving()
    {
        isObjMoving = true;
        btn_EditObj.SetActive(false);
    }
    
    /// <summary>
    /// ��ư�� ���� onclick�� �޾��� ȸ����Ű��
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
            //���� colliider�� ���������� ũ�� �ٱ��� ��ġ�Ѱ��̹Ƿ� ����
            if (Vector3.Distance((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.position) > objCollider2D.bounds.size.x / 2)
            {
                //���� btn_edit�� �����ִٸ� return
                if (!btn_EditObj.activeInHierarchy) return;
                //�����ְ� �ش� �κ������ϰ� �ٸ��κ� ��ġ���̶��
                if (!RectTransformUtility.RectangleContainsScreenPoint(btn_EditObjRect, Input.mousePosition))
                {
                    btn_EditObj.SetActive(false);
                    return;
                }
                return;
            }

            //�� ó�� ������ Ŭ��ó����
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
