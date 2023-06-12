using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 生成時のEdge部分を担当
/// </summary>
public class CreateEdge 
{
    private const int LabelFontSizeValue = 64;
    private const int LabelMarginTopValue = -34;
    public void Create(NodeData nodeData,GraphViewManager graphViewManager) {
        int edgeCount = nodeData.edgesDatas.Count;
        List<Node> node = graphViewManager.nodes.ToList();
        if (edgeCount > 0) {
            for (int createEdgeCount = 0; createEdgeCount < edgeCount; createEdgeCount++)
            {
                //Port作製
                Port inputPort = node[nodeData.edgesDatas[createEdgeCount].inputNodeId].inputContainer.contentContainer.Q<Port>();
                Port outputPort = node[nodeData.controlNumber].outputContainer.contentContainer.Q<Port>();
                //Edge作製
                Edge edge = ConnectPorts(inputPort, outputPort);
                //GraphViewに追加
                graphViewManager.AddElement(edge);
            }
        }

    }
    public Edge ConnectPorts(Port inputport, Port outputport)
    {
        //エッジの作成
        Edge tempEdge = new Edge
        {
            output = outputport,
            input = inputport
        };
        //ノードに接続
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        return tempEdge;
    }
}
