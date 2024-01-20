using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoNodeController : MonoBehaviour
{
    //저항값 circuitnode들은 부모 오브젝트의 twoNodeController의 resistance 공유함.
    public float resistance;

    //자식 노드 두개
    public CircuitNode firstNode;
    public CircuitNode secondNode;
    private CircuitNode[] twoNodes;
    private bool isNotified=false;
    //시계방향에서 자식 circuitnode들 중 하나라도 방문을 했는지.
    public bool cwFirstVisited=false;
    //반시계방향에서 자식 circuitnode들 중 하나라도 방문을 했는지.
    public bool ccwFirstVisited=false;

    private void Awake()
    {
        twoNodes = GetComponentsInChildren<CircuitNode>();
        firstNode= twoNodes[0];
        secondNode = twoNodes[1];
        resistance = 5;
    }

    //처음 저항에 연결되면 다른 노드에게 연결해주는 함수
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
