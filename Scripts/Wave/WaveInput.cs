using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInput : InputValueController
{
    private Button btn_Play;
    public WaveGenerator waveGenerator;

    int amplitude=0, waveLength = 0;

    private void Awake()
    {
        btn_Play = GetComponent<Button>();
        btn_Play.onClick.AddListener(Simulate);

    }
    protected override void Simulate()
    {
        amplitude = int.Parse(inputs[0].text);
        waveLength = int.Parse(inputs[1].text);

        waveGenerator.SetHeight(amplitude);
        waveGenerator.SetWidth(waveLength);

        waveGenerator.DrawGraph();
    }
}
