using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
/// <summary>
/// GraphAssetの内容をエディタウィンドウに表示する
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
    //データからの作成
    public static void CreateGraphView(GraphViewManager graphViewManager) {
        GraphAsset loadGraphAsset = graphViewManager.m_GraphAsset;

        foreach (var node in loadGraphAsset.nodes) {
            CreateNode(node,graphViewManager);
        }
        foreach (var edge in loadGraphAsset.edges) {
            CreateEdge(edge, graphViewManager);
        }
    }
    private static void CreateNode(NodeData nodeData,GraphViewManager graphViewManager) {
        var node = new ScriptNode();
        //ノードの位置
        node.SetPosition(new Rect(nodeData.position, new UnityEngine.Vector2(100,100)));
        //名前（予定）
        //スクリプト
        if (nodeData.Object != null)
            node.ObjectField.value = nodeData.Object;
        //管理番号
        node.NodeID = nodeData.controlNumber;
        //画面に追加
        graphViewManager.AddElement(node);
    } 
    private static void CreateEdge(EdgeData edgeData,GraphViewManager graphViewManager) {
        var node = graphViewManager.nodes.ToList();
        Debug.Log("現在生成されているノードは"+node.Count+"個です");
        //TODO　そもそもPortを取った方が早いかも？
        //Port作製
        Port inputPort = node[edgeData.inputNodeId].inputContainer.contentContainer.Q<Port>();
        Port outputPort = node[edgeData.outputNodeId].outputContainer.contentContainer.Q<Port>();
        //Edge作製
        var edge = ConnectPorts(inputPort,outputPort);
        //ラベル追加
        UnityEngine.UIElements.Label edgeLabel = new UnityEngine.UIElements.Label();
        edgeLabel.text = "0";
        edgeLabel.style.fontSize= 64;
        edgeLabel.style.marginTop = -32;
        edge.edgeControl.Add(edgeLabel);
        //Label
        //edge.Add(btn1);

        //GraphViewに追加
        graphViewManager.AddElement(edge);
    }
    private static Edge ConnectPorts(Port inputport, Port outputport) {
        //エッジの作成
        var tempEdge = new Edge
        {
            output = outputport,
            input=inputport
        };
        //ノードに接続
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        return tempEdge;
    }
}
