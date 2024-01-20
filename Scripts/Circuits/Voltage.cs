using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voltage : MonoBehaviour
{
    public float voltage=10;
    public bool isClockWise;
    public CircuitNode[] twoNodes;
    private CircuitNode firstNode;
    private CircuitNode secondNode;

    private void Awake()
    {
        twoNodes = GetComponentsInChildren<CircuitNode>();
        foreach(CircuitNode node in twoNodes)
        {
            if (node.isFirstNode)
            {
                firstNode = node;
            }
            else
                secondNode = node;
        }

        //방향 정하기
    }
}
