
public class WaveHint : Hint
{
    string[] hints = { "<color=#ff0000ff>�ĵ�</color>�̶� �� ������ �߻��� ������ ���� ������ �����̴�.",
        "�ĵ����� ���� ���� ���� <color=#ff0000ff>����</color>,\n���� ���� ���� <color=#ff0000ff>��</color>�̶�� �Ѵ�.",
        "���� �߽����κ��� ���� �Ǵ� ������� �Ÿ��� <color=#ff0000ff>����</color> \n ���翡�� ���� �Ǵ� �񿡼� ����� �Ÿ��� <color=#ff0000ff>����</color>�̶� �Ѵ�.",
        "",
        "�ĵ��� �� ���常ŭ �����ϴ� �� �ɸ��� �ð��� <color=#ff0000ff>�ֱ�</color>����Ѵ�.",
        "������ �� ���� 1�� ���� �����ϴ� Ƚ���� <color=#ff0000ff>������</color>��� �Ѵ�. \n <color=#ff0000ff>������</color>�� 1/<color=#ff0000ff>�ֱ�</color>�̴�.",
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
    /// ���� ��ư ������ �� ó���Լ�
    /// </summary>
    public override void nextStep()
    {
        //ȣ����ڸ��� �ε��� + 1 
        curIdx++;

        //image�� �����ִٸ� �ϴ� ��
        if (ImgToShow.enabled == true)
        {
            ImgToShow.enabled = false;
        }

        //�ι�° �ε������� ���� ��ư ��Ÿ������
        if(curIdx == 1)
        {
            btn_prev.gameObject.SetActive(true);
        }

        if (curIdx == 3)
        {
            ImgToShow.enabled = true;
            ImgToShow.sprite = descriptionImgs[0];
        }
        if(curIdx == 6)
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
    /// ���� ��ư ������ ��, ó���Լ�
    /// </summary>
    public override void prevStep()
    {
        //ȣ����ڸ��� �ε��� - 1
        curIdx--;

        if (ImgToShow.enabled == true)
        {
            ImgToShow.enabled = false;
        }
        //5��° �ε������� ���� ��ư ��Ÿ������
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

        //������ �ε������� ���� ��ư ���������
        if (curIdx == 0)
        {
            btn_prev.gameObject.SetActive(false);
        }
    }
}
