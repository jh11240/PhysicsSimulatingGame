
public class CircuitsHintt : Hint
{
    string[] hints = { "<color=#ff0000ff>파동</color>이란 한 곳에서 발생한 진동이 퍼져 나가는 현상이다.",
        "파동에서 가장 높은 곳을 <color=#ff0000ff>마루</color>,\n가장 낮은 곳을 <color=#ff0000ff>골</color>이라고 한다.",
        "진동 중심으로부터 마루 또는 골까지의 거리를 <color=#ff0000ff>진폭</color> \n 마루에서 마루 또는 골에서 골까지 거리를 <color=#ff0000ff>파장</color>이라 한다.",
        "",
        "파동이 한 파장만큼 진행하는 데 걸리는 시간을 <color=#ff0000ff>주기</color>라고한다.",
        "매질의 각 점이 1초 동안 진동하는 횟수를 <color=#ff0000ff>진동수</color>라고 한다. \n <color=#ff0000ff>진동수</color>는 1/<color=#ff0000ff>주기</color>이다.",
        ""
    };

    private void Awake()
    {
        btn_next.onClick.AddListener(nextStep);
        btn_prev.onClick.AddListener(prevStep);
    }

    private void OnEnable()
    {
        ImgToShow.enabled = false;
        curIdx = 0;
        txtDescription.text = hints[curIdx];
        btn_prev.gameObject.SetActive(false);
    }
    /// <summary>
    /// 다음 버튼 눌렀을 때 처리함수
    /// </summary>
    public override void nextStep()
    {
        //호출되자마자 인덱스 + 1 
        curIdx++;

        //image가 켜져있다면 일단 끔
        if (ImgToShow.enabled == true)
        {
            ImgToShow.enabled = false;
        }

        //두번째 인덱스부터 이전 버튼 나타나게함
        if (curIdx == 1)
        {
            btn_prev.gameObject.SetActive(true);
        }

        if (curIdx == 3)
        {
            ImgToShow.enabled = true;
            ImgToShow.sprite = descriptionImgs[0];
        }
        if (curIdx == 6)
        {
            ImgToShow.enabled = true;
            txtDescription.text = "";
            ImgToShow.sprite = descriptionImgs[0];
            btn_next.gameObject.SetActive(false);
            return;
        }
        txtDescription.text = hints[curIdx];
    }

    /// <summary>
    /// 이전 버튼 눌렸을 때, 처리함수
    /// </summary>
    public override void prevStep()
    {
        //호출되자마자 인덱스 - 1
        curIdx--;

        if (ImgToShow.enabled == true)
        {
            ImgToShow.enabled = false;
        }
        //5번째 인덱스부터 다음 버튼 나타나게함
        if (curIdx == 5)
        {
            btn_next.gameObject.SetActive(true);
        }

        if (curIdx == 3)
        {
            ImgToShow.enabled = true;

            txtDescription.text = "";
            ImgToShow.sprite = descriptionImgs[0];
            curIdx--;
            return;
        }
        txtDescription.text = hints[curIdx];

        //마지막 인덱스에선 다음 버튼 사라지게함
        if (curIdx == 0)
        {
            btn_prev.gameObject.SetActive(false);
        }
    }
}
