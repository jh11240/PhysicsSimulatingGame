using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitNodeManager : MonoBehaviour
{
    public List<CircuitNode> entireNode;
    public List<CircuitNode> groupFirstNode;
    public float RSum=0;
    public int howManySplits;
    public CircuitNode curNode;
    public CircuitNode prevNode;
    public CircuitNode startNode;
    public float voltageVal;
    private SplitNode splitNode;
    private MergeNode mergeNode;
    public bool CircuitStarted=false;

    private void Awake()
    {
        entireNode = new List<CircuitNode>();
        groupFirstNode = new List<CircuitNode>();
        howManySplits = 0;
    }

    public void StartEstablishCircuit()
    {
        Debug.Log("start");
        CircuitStarted = true;
        entireNode.Clear();
        groupFirstNode.Clear();
        CircuitNode[] voltage = GameObject.FindGameObjectWithTag("Voltage").GetComponentsInChildren<CircuitNode>();
        voltageVal = voltage[0].GetComponentInParent<Voltage>().voltage;

        //voltage의 nextNode값 부터 시작
        foreach(CircuitNode node in voltage)
        {
            if(node.isFirstNode)
                startNode = node.NextNode;
        }

        //시작 노드 groupFirstNode넣기.
        groupFirstNode.Add(startNode);
        curNode = startNode;
        RSum = 0;
        CwPropagate();
    }

    //cwPropagate가 시계방향으로 재귀함수
    void CwPropagate()
    {
        //볼트라면
        if(curNode.thisNodeKind == circuitKind.Voltage)
        {
            curNode = curNode.PrevNode;
            RSum=0;
            //역방향으로 
            CcwPropagate();
            return;
        }

        //이미 방문한 노드면 return
        if (curNode.cwNodeVisted)
        {
            return;
        }

        //이미 방문한 노드라면
        curNode.cwNodeVisted = true;

        //전체 노드에 현재 노드 포함
        entireNode.Add(curNode);

        //curNode
        if(curNode.thisNodeKind == circuitKind.Resistance)
        {
            //저항인데 이미 방문한 노드라면 다음 노드로 이동
            if (curNode.CwIsFirstNodeInResistance)
            {
                curNode = curNode.NextNode;
                CwPropagate();
                return;
            }

            //방문을 안 한 노드라면 cwVisited
            curNode.CwIsFirstNodeInResistance = true;

            //rSum에 현재 curnode 저항 더해주고
            RSum += curNode.Resistance;

            //curNode의 저항에 rSum더해줌
            curNode.Resistance = RSum;

            curNode = curNode.NextNode;

            CwPropagate();
        }

        //splitNode
        else if(curNode.thisNodeKind == circuitKind.SplitNode)
        {

            splitNode = curNode.GetComponentInParent<SplitNode>();

            if(!groupFirstNode.Contains(curNode))
            groupFirstNode.Add(curNode);


            groupFirstNode.Add(splitNode.twoNodes[0]);

            groupFirstNode.Add(splitNode.twoNodes[1]);

            
            foreach(CircuitNode node in splitNode.twoNodes)
            {
                RSum = 0;
                curNode = node.NextNode;
                CwPropagate();
            }

        }

        //mergeNode
        else
        {
            mergeNode = curNode.GetComponentInParent<MergeNode>();

            //만약 두 병렬중에 첫번째 줄이라면
            if (mergeNode.visited==0)
            {
                mergeNode.visited++;
                mergeNode.firstNodeResSum = curNode.PrevNode.Resistance;
                return;
            }
            
            //두번째 방문이라면
            else if(mergeNode.visited==1)
            {
                mergeNode.visited++;
                mergeNode.secondNodeResSum = curNode.PrevNode.Resistance;
                groupFirstNode.Add(mergeNode.connectedNode);
                curNode = mergeNode.connectedNode.NextNode;

                mergeNode.CalculateRes();
                CwPropagate();
            }
        }
    }

    //역방향으로 재귀함수
    void CcwPropagate()
    {
        //볼트라면
        if (curNode.thisNodeKind == circuitKind.Voltage)
        {
            Debug.Log("다돌았다!!");
            return;
        }

        //curNode
        if (curNode.thisNodeKind == circuitKind.Resistance)
        {
            //저항인데 이미 방문한 노드라면 다음 노드로 이동
            if (curNode.CcwIsFirstNodeInResistance)
            {
                curNode = curNode.PrevNode;
                CcwPropagate();
                return;
            }

            //방문을 안 한 노드라면 ccwVisited
            curNode.CcwIsFirstNodeInResistance = true;

            //이전노드가 split 노드면 split노드 계산된 rSum값 더해줌
            if (curNode.NextNode.thisNodeKind == circuitKind.SplitNode)
            {
                splitNode = curNode.NextNode.GetComponentInParent<SplitNode>();
                curNode.Resistance += splitNode.resSum;
            }
            //일반 resistance라면 옮김
            else
            {
                //RSum에 현재 prevnode 저항 더해주고
                RSum = RSum > curNode.Resistance ? RSum : curNode.Resistance;

                //curNode의 저항에 rSum더해줌
                curNode.Resistance = RSum;
            }
            curNode = curNode.PrevNode;
            CcwPropagate();
        }

        //splitNode
        else if( curNode.thisNodeKind == circuitKind.SplitNode)
        {
            splitNode = curNode.GetComponentInParent<SplitNode>();
            //만약 두 병렬중에 첫번째 줄이라면
            if (splitNode.visited == 0)
            {
                splitNode.visited++;
                splitNode.firstNodeResSum = curNode.NextNode.Resistance;
                return;
            }
            else if(splitNode.visited == 1)
            {
                splitNode.visited++;
                splitNode.secondNodeResSum = curNode.NextNode.Resistance;
                curNode = splitNode.connectedNode.PrevNode;
                splitNode.CalculateRes();
                CcwPropagate();
            }

        }

        //mergeNode 역전파에서는 반대로 퍼뜨리는 역활
        else if(curNode.thisNodeKind == circuitKind.mergeNode)
        {
            mergeNode = curNode.GetComponentInParent<MergeNode>();

            foreach (CircuitNode node in mergeNode.twoNodes)
            {
                RSum = 0;
                curNode = node.PrevNode;
                CcwPropagate();
            }

        }
    }
}
