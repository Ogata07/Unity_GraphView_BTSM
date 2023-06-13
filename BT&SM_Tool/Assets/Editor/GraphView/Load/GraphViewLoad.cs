using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// GraphAssetの内容をエディタウィンドウに表示する
/// </summary>
public  class GraphViewLoad 
{
    private  CreateNode  createNode=new CreateNode();
    private  CreateEdge createEdge=new CreateEdge();
    public  void LoadNodeElement(GraphAsset graphAsset) {
        GraphEditorWindow.ShowWindow(graphAsset);
    }
    //データからの作成
    public  void CreateGraphView(GraphViewManager graphViewManager) {
        GraphAsset loadGraphAsset = graphViewManager.graphAsset;

        foreach (var node in loadGraphAsset.nodes) {
            createNode.Create(node, graphViewManager);
        }
        foreach (var node in loadGraphAsset.nodes) {
            createEdge.Create(node, graphViewManager);
        }
    }
}
