using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoNodeController : MonoBehaviour
{
    //���װ� circuitnode���� �θ� ������Ʈ�� twoNodeController�� resistance ������.
    public float resistance;

    //�ڽ� ��� �ΰ�
    public CircuitNode firstNode;
    public CircuitNode secondNode;
    private CircuitNode[] twoNodes;
    private bool isNotified=false;
    //�ð���⿡�� �ڽ� circuitnode�� �� �ϳ��� �湮�� �ߴ���.
    public bool cwFirstVisited=false;
    //�ݽð���⿡�� �ڽ� circuitnode�� �� �ϳ��� �湮�� �ߴ���.
    public bool ccwFirstVisited=false;

    private void Awake()
    {
        twoNodes = GetComponentsInChildren<CircuitNode>();
        firstNode= twoNodes[0];
        secondNode = twoNodes[1];
        resistance = 5;
    }

    //ó�� ���׿� ����Ǹ� �ٸ� ��忡�� �������ִ� �Լ�
    public void Notify(CircuitNode node)
    {
        if (isNotified) return;

        if (firstNode == node)
        {
            firstNode.SetNextNode(secondNode);
            secondNode.SetPrevNode(firstNode);
        }

        else
        {
            secondNode.SetNextNode(firstNode);
            firstNode.SetPrevNode(secondNode);
        }

        isNotified = true;
    }
}
