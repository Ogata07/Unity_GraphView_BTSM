using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
///　エディタウィンドウの内容をGraphAssetに保存する
/// </summary>
public class GraphViewSave
{
    public void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("セーブの開始");
        //ノード
        Debug.Log("ノードの数は"+m_GraphView.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + m_GraphView.edges.ToList().Count + "個");
        SaveNode(m_GraphView);
    }
    private void SaveNode(GraphView m_GraphView) {
        //ウィンドウ上のノードのリスト
        var fieldNodelist = m_GraphView.nodes.ToList();

    }
    private void SaveEdgs() { 

    }
}
