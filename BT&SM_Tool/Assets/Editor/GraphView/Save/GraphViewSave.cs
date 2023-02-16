using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
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
        ControlNumberAdd(m_GraphView);
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset,m_GraphView);
    }
    //TODO セーブ時に管理番号を付与しているが他で使うことはないのか？
    //各ノードに管理番号を付与
    private static void ControlNumberAdd(GraphView m_GraphView) {
        var NodeList=m_GraphView.nodes.ToList();
        int Number = 0;
        foreach (var node in NodeList) {
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                castScriptNode.NodeID = Number;
                
            }
            Number++;
        }
    }
    //ノードの保存
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //ウィンドウ上のノードのリスト
        var fieldNodeList = m_GraphView.nodes.ToList();
        //リストの初期化
        m_GraphAsset.nodes = new List<NodeData>();
        foreach (var node in fieldNodeList)
        {
            //場所の追加
            m_GraphAsset.nodes.Add(new NodeData());
            int listNumber = m_GraphAsset.nodes.ToList().Count - 1;

            //位置の保存
            m_GraphAsset.nodes[listNumber].position = node.GetPosition().position;


            //スクリプトの保存
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                m_GraphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
            }

            //管理番号の保存
            if (node is ScriptNode)
            {
                ScriptNode castScriptNode = node as ScriptNode;
                m_GraphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;
            }
        }
    }
    //エッジの保存
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //ウィンドウ上のエッジのリスト
        var fieldEdgslist = m_GraphView.edges.ToList();
        Debug.Log(fieldEdgslist.Count());
        //リストの初期化
        m_GraphAsset.edges = new List<EdgeData>();
        //テスト用に簡素で
        foreach (var edge in fieldEdgslist.Select((v, i) => new { value = v, Index = i }))
        {
            //場所の追加
            m_GraphAsset.edges.Add(new EdgeData());
            if (edge.value.input.node is ScriptNode){
                ScriptNode castScriptNode= edge.value.input.node as ScriptNode;
                m_GraphAsset.edges[edge.Index].inputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("input側での接続先が保存できませんでした。管理番号が振られていない可能性があります");
            if (edge.value.output.node is ScriptNode){
                ScriptNode castScriptNode = edge.value.output.node as ScriptNode;
                m_GraphAsset.edges[edge.Index].outputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("output側での接続先が保存できませんでした。管理番号が振られていない可能性があります");

        }
    }
    private static Type ListReset<Type>(Type ListData)
    where Type:IComparer{
        return default;
    }
}
