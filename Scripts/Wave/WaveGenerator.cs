using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveGenerator : MonoBehaviour
{
    public GameObject Dot;

    [SerializeField] private float sinWidth;
    [SerializeField] private float sinHeight;
    [SerializeField] private float graphSize;

    private GameObject[] Dots;

    private void Awake()
    {
        Dots = new GameObject[1000];
        for (int i = 0; i < 1000; i++)
        {
            Dots[i] = Instantiate(Dot);
            Dots[i].transform.SetParent(gameObject.transform);
        }
    }

    private void Start()
    {
       DrawGraph();
    }

    /// <summary>
    /// ���� ���ϴ� �Լ�
    /// </summary>
    /// <param name="width"></param>
    public void SetWidth(float width)
    {
        sinWidth = width;
    }

    /// <summary>
    /// �� ��ũ��Ʈ�� DrawGraph�� ȣ���ϴ� ���� �����Լ�
    /// </summary>
    public void SetHeight(float height)
    {
        sinHeight = height;
    }

    private Vector2 GetSinPos(float X)
    {
        float Y = Mathf.Sin(X * (Mathf.PI / 180));
        //�Լ� �̹����� �°� ������¡
        if(X * sinWidth * (Mathf.PI / 180) < -108)
        {
            return new Vector2(-108, Mathf.Sin(108* (Mathf.PI/180)));
        }
        else if(X * sinWidth * (Mathf.PI / 180) > 115)
        {
            return new Vector2(115, Mathf.Sin(115 * Mathf.PI / 180));
        }
        return new Vector2(X*sinWidth * (Mathf.PI / 180), Y*sinHeight);
    }

    public void DrawGraph()
    {
        int cnt = 0;
        for (float i = 0; i < 2000; i += 2f)
        {
            if (cnt >= 1000) break;
            Dots[cnt++].transform.localPosition = GetSinPos(i - 1000);
        }
    }
}
