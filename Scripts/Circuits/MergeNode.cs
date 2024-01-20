using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeNode : MonoBehaviour
{
    public List<CircuitNode> twoNodes;
    public CircuitNode connectedNode;
    public float resSum;
    public float firstNodeResSum;
    public float secondNodeResSum;

    public int visited=0;

    private void Awake()
    {
        visited = 0;
    }

    public void CalculateRes()
    {
        resSum = firstNodeResSum * secondNodeResSum / (firstNodeResSum + secondNodeResSum);
    }

}
