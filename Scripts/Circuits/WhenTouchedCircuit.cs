using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenTouchedCircuit : MonoBehaviour
{
    public CircuitNodeManager circuitNodeManager;
    public Transform CircuitObjectsTr;
    public Transform DrawingContainerTr;
    private List<CircuitNode> firstNodes;
    public float rSum;
    public float totalCurrent;
    private ShowCurrent showCurrent;

    private void Awake()
    {
        showCurrent = GetComponent<ShowCurrent>();
    }

    /// <summary>
    /// 일단 clockwise 회로라고 가정 아니면 가까운것중에 voltage에서 방향 정보가져와서 가까운 노드 찾아야함
    /// </summary>
    /// <param name="mousepos"></param>
    /// <returns></returns>
    public CircuitNode FindNearestNode(Vector3 mousepos)
    {
        CircuitNode nearestNode=firstNodes[0];
        Vector3 mouseposWorld = Camera.main.ScreenToWorldPoint(mousepos);
        float nearestDist=Vector3.Distance(nearestNode.gameObject.transform.position,mouseposWorld);

        foreach(CircuitNode node in firstNodes)
        {
            if(node.gameObject.transform.position.x < mouseposWorld.x)
            {
                if (Vector3.Distance(node.gameObject.transform.position, mouseposWorld) < nearestDist)
                {
                    nearestNode = node;
                    nearestDist = Vector3.Distance(node.gameObject.transform.position, mouseposWorld);
                }
            }
        }
        return nearestNode;
    }
    public void CalculateAllRes()
    {
        //전체 저항
        rSum = 0;
        int splitVisited = 0;
        //calculate whole group resistance
        foreach (CircuitNode elem in circuitNodeManager.groupFirstNode)
        {
            if (elem.thisNodeKind == circuitKind.Resistance)
            {
                rSum += elem.Resistance;
            }
            else if (elem.thisNodeKind == circuitKind.SplitNode)
            {
                splitVisited++;
                if (splitVisited == 2) continue;
                if (splitVisited == 3)
                {
                    splitVisited = 0;
                    continue;
                }
                SplitNode splitNode = elem.GetComponentInParent<SplitNode>();
                rSum += splitNode.resSum;
            }
        }
    }

    public void ScreenMouseRay()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 origin = mousePosition;
        mousePosition.z = Mathf.Infinity;

        firstNodes = circuitNodeManager.groupFirstNode;

        Vector3 targetPos = origin;
            targetPos.z = 0;
            CircuitNode node = FindNearestNode(targetPos);

        CalculateAllRes();

        //voltage
        float voltage = circuitNodeManager.voltageVal;
        totalCurrent = voltage / rSum;

        //splitNode
        if(node.thisNodeKind == circuitKind.SplitNode)
        {
            Debug.Log("split node");
            SplitNode splitNode = node.GetComponentInParent<SplitNode>();

            //만약 해당 노드가 첫번째 노드라면
            if(node == firstNodes[0])
            {
                //범위 넘어간다면 전체 전류 띄워야함
                if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < node.transform.position.x)
                {
                    Debug.Log("first" + totalCurrent);
                    showCurrent.ShowAmpare(Input.mousePosition,totalCurrent);
                    return;
                } 
            }
            
            float resSimpleSum = splitNode.firstNodeResSum + splitNode.secondNodeResSum,
                  firstRate = splitNode.firstNodeResSum / resSimpleSum,
                  secondRate = splitNode.secondNodeResSum / resSimpleSum;

            //첫번째 branch라면
            if (node.isFirstNode)
            {
                showCurrent.ShowAmpare(Input.mousePosition, firstRate*totalCurrent);
            }
            //두번째 branch라면
            else if(!node.isFirstNode)
            {
                showCurrent.ShowAmpare(Input.mousePosition, secondRate*totalCurrent);
            }
        }
        else
        {
            Debug.Log("else node");
            showCurrent.ShowAmpare(Input.mousePosition, totalCurrent);
        }

        return;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(circuitNodeManager.CircuitStarted)
            ScreenMouseRay();
            else
            showCurrent.CloseWindow();
        }
    }
}
