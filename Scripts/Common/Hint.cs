using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Hint : MonoBehaviour
{
    // ����,���� ��ư��
    public Button btn_next;
    public Button btn_prev;

    // ȭ�鿡 ���÷����� ���� �̹���
    public Image ImgToShow;

    // ���� �̹��� �ҽ���
    public Sprite[] descriptionImgs;

    //����
    public TextMeshProUGUI txtDescription;

    //���� idx
    protected int curIdx = 0;


    //���� ��ư ������ �� ó���Լ�
    public abstract void nextStep();

    //���� ��ư  ������ �� ó���Լ�
    public abstract void prevStep();
}
