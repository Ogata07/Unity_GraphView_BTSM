using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
///　エディタウィンドウの内容をGraphAssetに保存する
/// </summary>
public static class GraphViewSave
{
    public static void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("セーブの開始");
        //ノード
        Debug.Log("ノードの数は"+m_GraphView.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + m_GraphView.edges.ToList().Count + "個");
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset, m_GraphView);
    }
    //ノードの保存
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //ウィンドウ上のノードのリスト
        var fieldNodelist = m_GraphView.nodes.ToList();
        //リストの初期化
        m_GraphAsset.nodes = new List<NodeData>();
        //テスト用に簡素で
        foreach (var node in fieldNodelist.Select((v,i)=> new {value=v,Index=i})) 
        {
            //場所の追加
            m_GraphAsset.nodes.Add(new NodeData());
            //ノードの位置を挿入
            m_GraphAsset.nodes[node.Index].position = node.value.GetPosition().position;
        }
    }
    //エッジの保存
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //ウィンドウ上のエッジのリスト
        var fieldEdgslist = m_GraphView.edges.ToList();
        //リストの初期化
        m_GraphAsset.edges = new List<EdgeData>();
        //テスト用に簡素で
        foreach (var edge in fieldEdgslist.Select((v, i) => new { value = v, Index = i }))
        {
            //場所の追加
            m_GraphAsset.edges.Add(new EdgeData());
            //ノードの生成番号を取得
            var a = edge.value.output.node as TestNode;
            //エッジのインノードをいれる
            m_GraphAsset.edges[edge.Index].inputNodeId = a.NodeID;
            //エッジのアウトノードをいれる
            m_GraphAsset.edges[edge.Index].outputNodeId = a.NodeID;

        }
    }
    private static Type ListReset<Type>(Type ListData)
    where Type:IComparer{
        return default;
    }
}
