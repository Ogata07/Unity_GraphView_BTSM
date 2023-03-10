using JetBrains.Annotations;
using PlasticGui.WorkspaceWindow.Replication;
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
    //各ノードに管理番号を付与
    private static void ControlNumberAdd(GraphView m_GraphView) {
        int Number = 0;
        var NodeList = m_GraphView.nodes.ToList();
        //順番がおかしくなるので変更
        /*
        foreach (var node in NodeList) {
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                castScriptNode.NodeID = Number;
                
            }
            Number++;
        }
        */
        //スタートノードから順番を探索して管理番号を付与する
        //(ビヘイビアツリー限定)

        //スタートノードを取得
        var StartNode = NodeList.Find(x=>x.title == "StartNode") as StartNode;
        //つながっているノードを取得(0番)
        var NextNode = StartNode.OutputPort.connections.FirstOrDefault().input.node;
        
        //ノードが繋がっているか？
        if (NextNode != null) {
            AddNumbar(NextNode,Number);
            Debug.Log("現在の管理番号は" + Number + "です");
            Number++;
            //次に繋がっている確認する
            NextNode=ChackNode(NextNode);
            while(NextNode!=null)
            {
                AddNumbar(NextNode, Number);
                Debug.Log("現在の管理番号は" + Number + "です");
                Number++;

                //次に繋がっている確認する
                NextNode = ChackNode(NextNode);
            }
            //TODO 現在は分岐ノードを作っていないので分岐には未対応
        }
    }
    //管理番号を付与する
    private static void AddNumbar(Node node,int Numbar) {
        //管理番号を付与する
        //スクリプトノードしか番号を振れない
        if (node is ScriptNode){
            ScriptNode castScriptNode = node as ScriptNode;
            castScriptNode.NodeID = Numbar;
        }
        else
            Debug.LogError("番号を振るのに対応していません");
    }
    //次のノードを検索する
    private static Node ChackNode(Node node) {
        //繋がっているノードを取得
        if (node is ScriptNode) {
            ScriptNode castScriptNode = node as ScriptNode;
            if (castScriptNode.OutputPort.connections.FirstOrDefault() != null) {
                Node inputNode = castScriptNode.OutputPort.connections.FirstOrDefault().input.node;
                return inputNode;
            }
        }
        return null;
    }
    //ノードの保存
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //ウィンドウ上のノードのリスト
        var fieldNodeList = m_GraphView.nodes.ToList();
        //除外対象を排除
        fieldNodeList.RemoveAll(node => node is StartNode);
       
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
        //管理番号のソート
        m_GraphAsset.nodes.Sort((node1,node2)=>node1.controlNumber-node2.controlNumber);
    }
    //エッジの保存
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //ウィンドウ上のエッジのリスト
        var fieldEdgslist = m_GraphView.edges.ToList();
        Debug.Log(fieldEdgslist.Count());
        //リストの初期化
        m_GraphAsset.edges = new List<EdgeData>();
        //除外対象を排除
        fieldEdgslist.RemoveAll(i => i.output.node is StartNode);
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
