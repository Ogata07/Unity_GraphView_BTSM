using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
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
        var node = new TestNode();
        //ノードの位置
        node.SetPosition(new Rect(nodeData.position, new UnityEngine.Vector2(100,100)));
        //名前（予定）
        //管理番号（予定）
        graphViewManager.AddElement(node);
    } 
    private static void CreateEdge(EdgeData edgeData,GraphViewManager graphViewManager) {
        var node = graphViewManager.nodes.ToList();
        Debug.Log("現在生成されているノードは"+node.Count+"個です");
        //TODO 今回は各ノードで管理番号を持っていないのでおかしくなります
        //そもそもPortを取った方が早いかも？
        Port inputPort = node[0].inputContainer.contentContainer.Q<Port>();
        Port outputPort = node[1].outputContainer.contentContainer.Q<Port>();
        var edge = ConnectPorts(inputPort,outputPort);
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
