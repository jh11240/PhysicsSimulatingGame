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

        //voltage�� nextNode�� ���� ����
        foreach(CircuitNode node in voltage)
        {
            if(node.isFirstNode)
                startNode = node.NextNode;
        }

        //���� ��� groupFirstNode�ֱ�.
        groupFirstNode.Add(startNode);
        curNode = startNode;
        RSum = 0;
        CwPropagate();
    }

    //cwPropagate�� �ð�������� ����Լ�
    void CwPropagate()
    {
        //��Ʈ���
        if(curNode.thisNodeKind == circuitKind.Voltage)
        {
            curNode = curNode.PrevNode;
            RSum=0;
            //���������� 
            CcwPropagate();
            return;
        }

        //�̹� �湮�� ���� return
        if (curNode.cwNodeVisted)
        {
            return;
        }

        //�̹� �湮�� �����
        curNode.cwNodeVisted = true;

        //��ü ��忡 ���� ��� ����
        entireNode.Add(curNode);

        //curNode
        if(curNode.thisNodeKind == circuitKind.Resistance)
        {
            //�����ε� �̹� �湮�� ����� ���� ���� �̵�
            if (curNode.CwIsFirstNodeInResistance)
            {
                curNode = curNode.NextNode;
                CwPropagate();
                return;
            }

            //�湮�� �� �� ����� cwVisited
            curNode.CwIsFirstNodeInResistance = true;

            //rSum�� ���� curnode ���� �����ְ�
            RSum += curNode.Resistance;

            //curNode�� ���׿� rSum������
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

            //���� �� �����߿� ù��° ���̶��
            if (mergeNode.visited==0)
            {
                mergeNode.visited++;
                mergeNode.firstNodeResSum = curNode.PrevNode.Resistance;
                return;
            }
            
            //�ι�° �湮�̶��
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

    //���������� ����Լ�
    void CcwPropagate()
    {
        //��Ʈ���
        if (curNode.thisNodeKind == circuitKind.Voltage)
        {
            Debug.Log("�ٵ��Ҵ�!!");
            return;
        }

        //curNode
        if (curNode.thisNodeKind == circuitKind.Resistance)
        {
            //�����ε� �̹� �湮�� ����� ���� ���� �̵�
            if (curNode.CcwIsFirstNodeInResistance)
            {
                curNode = curNode.PrevNode;
                CcwPropagate();
                return;
            }

            //�湮�� �� �� ����� ccwVisited
            curNode.CcwIsFirstNodeInResistance = true;

            //������尡 split ���� split��� ���� rSum�� ������
            if (curNode.NextNode.thisNodeKind == circuitKind.SplitNode)
            {
                splitNode = curNode.NextNode.GetComponentInParent<SplitNode>();
                curNode.Resistance += splitNode.resSum;
            }
            //�Ϲ� resistance��� �ű�
            else
            {
                //RSum�� ���� prevnode ���� �����ְ�
                RSum = RSum > curNode.Resistance ? RSum : curNode.Resistance;

                //curNode�� ���׿� rSum������
                curNode.Resistance = RSum;
            }
            curNode = curNode.PrevNode;
            CcwPropagate();
        }

        //splitNode
        else if( curNode.thisNodeKind == circuitKind.SplitNode)
        {
            splitNode = curNode.GetComponentInParent<SplitNode>();
            //���� �� �����߿� ù��° ���̶��
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

        //mergeNode �����Ŀ����� �ݴ�� �۶߸��� ��Ȱ
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
