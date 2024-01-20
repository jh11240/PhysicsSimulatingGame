using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Hint : MonoBehaviour
{
    // 다음,이전 버튼들
    public Button btn_next;
    public Button btn_prev;

    // 화면에 디스플레이할 설명 이미지
    public Image ImgToShow;

    // 설명 이미지 소스들
    public Sprite[] descriptionImgs;

    //설명
    public TextMeshProUGUI txtDescription;

    //설명 idx
    protected int curIdx = 0;


    //다음 버튼 눌렀을 시 처리함수
    public abstract void nextStep();

    //이전 버튼  눌렀을 시 처리함수
    public abstract void prevStep();
}
