using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
}
