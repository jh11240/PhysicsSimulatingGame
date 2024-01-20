using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum circuitKind
{
    Voltage,
    Resistance,
    SplitNode,
    mergeNode
}
public class CircuitNode : MonoBehaviour
{
    public bool cwNodeVisted;
    public bool isFirstNode;
    public bool ccwNodeVisted;

    //circuit Node들의 저항은 twoNodeController의 저항과 공유
    private float resistance;
    public circuitKind thisNodeKind;
    public List<CircuitNode> neighborNodes;

    public CircuitNode NextNode;
    public CircuitNode PrevNode;
    public TwoNodeController twoNodeController;


    private void Awake()
    {

        cwNodeVisted = false;
        ccwNodeVisted = false;

        if (thisNodeKind == circuitKind.Resistance)
        {
            twoNodeController = GetComponentInParent<TwoNodeController>();
            resistance = twoNodeController.resistance;
        }
    }

    public float Resistance{
        get
        {
            return twoNodeController.resistance;
        }
        set
        {
            twoNodeController.resistance = value;
        }
    }
    public bool CwIsFirstNodeInResistance
    {
        get
        {
            return twoNodeController.cwFirstVisited;
        }
        set
        {
            twoNodeController.cwFirstVisited = value;
        }
    }
    public bool CcwIsFirstNodeInResistance
    {
        get
        {
            return twoNodeController.ccwFirstVisited;
        }
        set
        {
            twoNodeController.ccwFirstVisited = value;
        }
    }

    public void SetNextNode(CircuitNode nextNode)
    {
        NextNode = nextNode;
    }

    public void SetPrevNode(CircuitNode prevNode)
    {
        PrevNode = prevNode;
    }

    public void SetNeighbour(CircuitNode node)
    {
        neighborNodes.Add(node);
    }

}