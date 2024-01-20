using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitNode : MonoBehaviour
{
    public List<CircuitNode> twoNodes;
    public CircuitNode connectedNode;

    //역전파때 사용됨 
    public float resSum;
    public int howManySplit;
    public float firstNodeResSum;
    public float secondNodeResSum;

    public int visited = 0;

    private void Awake()
    {
        visited = 0;
    }

    public void CalculateRes()
    {
        resSum = firstNodeResSum * secondNodeResSum / (firstNodeResSum + secondNodeResSum);
    }


}
