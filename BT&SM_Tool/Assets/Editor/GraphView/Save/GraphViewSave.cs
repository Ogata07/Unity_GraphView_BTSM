using JetBrains.Annotations;
using PlasticGui.WorkspaceWindow.Replication;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
/// <summary>
///　エディタウィンドウの内容をGraphAssetに保存する
/// </summary>
public static class GraphViewSave
{
    //TODO 名前空間でもいい？
    private static int Number = 0;
    public static void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("セーブの開始");
        //ノード
        Debug.Log("ノードの数は"+m_GraphView.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + m_GraphView.edges.ToList().Count + "個");
        Number = 0;
        ControlNumberAdd(m_GraphView);
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset,m_GraphView);

    }
    //各ノードに管理番号を付与
    private static void ControlNumberAdd(GraphView m_GraphView) {
        //int Number = 0;
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
        //(ステートマシン限定)(特定のノードを排除してそれ以外を順番に管理番号を付与する)
        //スタートノードに番号を振る(0番)
        var StartNode = NodeList.Find(x => x.title == "StartNode") as StartNode;
        NodeList.Remove(StartNode);
        foreach(Node node in NodeList)
        {
            AddNumbar(node);
        }
        //(ステートマシン限定)(順番に振るのは延期するかも)
        /*
        //スタートノードに番号を振る(0番)
        var StartNode = NodeList.Find(x => x.title == "StartNode") as StartNode;
        //つながっているノードを取得(0番)
        var NextNode = StartNode.OutputPort.connections.FirstOrDefault().input.node;
        AddNumbar(NextNode);
        var CastNextNode = NextNode as ScriptNode;
        ChacksNode(CastNextNode);
        /*
        //繋がっているノードの数を数える
        var NextNodeCount = CastNextNode.OutputPort.connections.Count();
        var NexrNodeList = CastNextNode.OutputPort.connections.ToList();
        //var NextNodeCount = StartNode.OutputPort.connections.Count();
        //var NexrNodeList = StartNode.OutputPort.connections.ToList();

        Debug.Log("aa"+NexrNodeList.Count);
        //複数に対応しているノード番号付与を作る
        //例2つの接続ノード
        
        for (int actionCount = 0; actionCount < NextNodeCount; actionCount++) {
            //つながっているノードに番号を付与する
            AddNumbar(NexrNodeList[actionCount].input.node,Number);
            Number++;
        }
        */
        //各ノードに繋がっているノードを探す
        //前に戻って見つかったノードに番号付与を行う
        //繰り返し行う(番号付与したノードの数を数えてそれで判定してもいいかも)

        //(ビヘイビアツリー限定)
        //スタートノードから順番を探索して管理番号を付与する

        //スタートノードを取得
        //var StartNode = NodeList.Find(x=>x.title == "StartNode") as StartNode;
        //つながっているノードを取得(0番)
        //var NextNode = StartNode.OutputPort.connections.FirstOrDefault().input.node;

        /*
        //ノードが繋がっているか？
        if (NextNode != null) {
            AddNumbar(NextNode,Number);
            Debug.Log("現在の管理番号は" + Number + "です");
            //次に繋がっている確認する
            NextNode=ChackNode(NextNode);
            while(NextNode!=null)
            {
                AddNumbar(NextNode, Number);
                Debug.Log("現在の管理番号は" + Number + "です");

                //次に繋がっている確認する
                NextNode = ChackNode(NextNode);
            }
            //TODO 現在は分岐ノードを作っていないので分岐には未対応
        }
        */
        
    }
    //管理番号を付与する
    private static void AddNumbar(Node node) {
        //管理番号を付与する
        //スクリプトノードしか番号を振れない
        //TODO セーブした後に追加するとうまく挙動しない
        if (node is ScriptNode){
            ScriptNode castScriptNode = node as ScriptNode;

            if (castScriptNode.NodeID== 0) {
                Debug.Log("ssss"+castScriptNode.NodeID);
                castScriptNode.NodeID = Number;
                Number++;

            }
        }
        else
            Debug.LogError("番号を振るのに対応していません");
    }
    //次のノードを検索する
    private static Node ChackNode(Node node) {
        //すでに番号が付与されているノードに降らないようにしないといけない
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

    /// <summary>
    /// 管理番号が振られていないノードがなくなるまで繰り返す
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static Node ChacksNode(Node node) {
        //ループしているノードを作ろうとするとすたっくしてしまう
        var castScriptNode = node as ScriptNode;
        //繋がっているノードの数を数える
        var NextNodeCount = castScriptNode.OutputPort.connections.Count();
        var NexrNodeList = castScriptNode.OutputPort.connections.ToList();
        //複数に対応しているノード番号付与を作る
        if (NextNodeCount != 0)
        {
            for (int actionCount = 0; actionCount < NextNodeCount; actionCount++)
            {
                //つながっているノードに番号を付与する
                AddNumbar(NexrNodeList[actionCount].input.node);
                ChacksNode(NexrNodeList[actionCount].input.node);
            }
        }
        return null;
    }
    /// <summary>
    /// スタートノードをのぞいたすべてのノードに管理番号を付与する
    /// </summary>
    private static void ListAddNumbar() { 
    
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
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                //スクリプトの保存
                m_GraphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
                //管理番号の保存
                m_GraphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;

                //対象ノードのアウトプットにつながっているすべてのエッジを保存
                var edgeslist=castScriptNode.OutputPort.connections.ToList();
                if (edgeslist.Count >= 0) { 
                    for (int listCount= 0; listCount < edgeslist.Count; listCount++) {
                        //保存場所の追加
                        m_GraphAsset.nodes[listNumber].edgesDatas.Add(new EdgesData());
                        ScriptNode castInputNode= edgeslist[listCount].input.node as ScriptNode;
                        //管理番号の保存
                        m_GraphAsset.nodes[listNumber].edgesDatas[listCount].controlNumber = listCount;
                        //インプット番号の保存(アウトプット番号はこのノードの管理番号なので保存しなくてよい)
                        m_GraphAsset.nodes[listNumber].edgesDatas[listCount].inputNodeId = castInputNode.NodeID;
                    }
                }
                //対象ノードのextensionContainerにつながっているFieldを保存
                int fieldCount = castScriptNode.extensionContainer.childCount;
                if (fieldCount >= 0) {
                    for (int i = 0; i < fieldCount; i++) {
                        //保存場所の追加
                        m_GraphAsset.nodes[listNumber].fieldData.Add(new FieldData());
                        //TODO　現在はどちらもStringでの保存ほかの方法が見つかればそれに変更
                        var fieldElement = castScriptNode.extensionContainer[i];
                        var castFieldElement = fieldElement as DataElement<FloatField,float>;
                        //型名の保存
                        m_GraphAsset.nodes[listNumber].fieldData[i].TypeName = "float";
                        //値の保存
                        m_GraphAsset.nodes[listNumber].fieldData[i].ValueData = castFieldElement.Field.value.ToString();
                    }
                }


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
