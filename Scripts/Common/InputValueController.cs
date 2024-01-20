using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class InputValueController : MonoBehaviour
{
    public TMP_InputField[] inputs;

    protected abstract void Simulate();
}
