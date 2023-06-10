using System;
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
    //edgeDataからの作成ではなくEdgesDataからの生成に切り替えを(edgeDataがいらなくなる可能性あり)
    public void Create(EdgeData edgeData, GraphViewManager graphViewManager)
    {
        var node = graphViewManager.nodes.ToList();
        //Port作製
        Port inputPort = node[edgeData.inputNodeId].inputContainer.contentContainer.Q<Port>();
        Port outputPort = node[edgeData.outputNodeId].outputContainer.contentContainer.Q<Port>();
        //Edge作製
        Edge edge = ConnectPorts(inputPort, outputPort);
        //ラベル追加
        //TODO 削除する予定
        Label edgeLabel = new Label();
        edgeLabel.text = "0";
        edgeLabel.style.fontSize = LabelFontSizeValue;
        edgeLabel.style.marginTop = LabelMarginTopValue;
        edge.edgeControl.Add(edgeLabel);
        //GraphViewに追加
        graphViewManager.AddElement(edge);
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
