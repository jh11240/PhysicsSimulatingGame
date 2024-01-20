using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement:MonoBehaviour
{
    public GameObject[] Stage;
    public GameObject panel;
    

    public void Loadgame(int i)
    {
        panel.SetActive(false);
        Instantiate<GameObject>(Stage[i]);
    }

}
