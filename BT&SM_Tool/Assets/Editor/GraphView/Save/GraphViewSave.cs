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
using UnityEngine.UIElements;
/// <summary>
///　エディタウィンドウの内容をGraphAssetに保存する
/// </summary>
public static class GraphViewSave
{
    //TODO 名前空間でもいい？
    private static int number = 0;
    private static ControlNumberAdd controlNumberAdd = new ControlNumberAdd();
    private static SaveNode saveNode = new SaveNode();
    public static void SaveNodeElement(GraphAsset graphAsset, GraphView graphView) 
    {
        Debug.Log("セーブの開始");
        //ノード
        Debug.Log("ノードの数は"+graphView.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + graphView.edges.ToList().Count + "個");
        number = 0;

        controlNumberAdd.ControlNumber(graphView);
        //ControlNumberAdd(graphView);
        saveNode.Save(graphAsset,graphView);
        SaveEdgs(graphAsset,graphView);

    }
    //各ノードに管理番号を付与
    //private static void ControlNumberAdd(GraphView graphView) {
    //    //int Number = 0;
    //    var nodeList = graphView.nodes.ToList();
    //    //(ステートマシン限定)(特定のノードを排除してそれ以外を順番に管理番号を付与する)
    //    //スタートノードに番号を振る(0番)
    //    var deleteStartNode = nodeList.Find(x => x.title == "StartNode") as StartNode;
    //    nodeList.Remove(deleteStartNode);
    //    var startNode = nodeList.Find(x => x.name == "Start") as ScriptNode;

    //    //
    //    if (startNode is ScriptNode)
    //    {
    //        ScriptNode castScriptNode = startNode as ScriptNode;
    //        castScriptNode.NodeID = number;
    //        number++;
    //    }
    //    nodeList.Remove(startNode);
    //    foreach (Node node in nodeList)
    //    {
    //        AddNumbar(node);
    //    }
    //}
    //管理番号を付与する
    //private static void AddNumbar(Node node) {
    //    //管理番号を付与する
    //    //スクリプトノードしか番号を振れない
    //    //TODO セーブした後に追加するとうまく挙動しない
    //    if (node is ScriptNode){
    //        ScriptNode castScriptNode = node as ScriptNode;
    //            castScriptNode.NodeID = number;
    //            number++;
    //    }
    //    else
    //        Debug.LogError("番号を振るのに対応していません");
    //}
    //次のノードを検索する
    //private static Node ChackNode(Node node) {
    //    //すでに番号が付与されているノードに降らないようにしないといけない
    //    //繋がっているノードを取得
    //    if (node is ScriptNode) {
    //        ScriptNode castScriptNode = node as ScriptNode;
    //        if (castScriptNode.OutputPort.connections.FirstOrDefault() != null) {
    //            Node inputNode = castScriptNode.OutputPort.connections.FirstOrDefault().input.node;
    //            return inputNode;
    //        }
    //    }
    //    return null;
    //}

    /// <summary>
    /// 管理番号が振られていないノードがなくなるまで繰り返す
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    //private static Node ChacksNode(Node node) {
    //    //ループしているノードを作ろうとするとすたっくしてしまう
    //    var castScriptNode = node as ScriptNode;
    //    //繋がっているノードの数を数える
    //    var nextNodeCount = castScriptNode.OutputPort.connections.Count();
    //    var nexrNodeList = castScriptNode.OutputPort.connections.ToList();
    //    //複数に対応しているノード番号付与を作る
    //    if (nextNodeCount != 0)
    //    {
    //        for (int actionCount = 0; actionCount < nextNodeCount; actionCount++)
    //        {
    //            //つながっているノードに番号を付与する
    //            AddNumbar(nexrNodeList[actionCount].input.node);
    //            ChacksNode(nexrNodeList[actionCount].input.node);
    //        }
    //    }
    //    return null;
    //}
    /// <summary>
    /// スタートノードをのぞいたすべてのノードに管理番号を付与する
    /// </summary>
    private static void ListAddNumbar() { 
    
    }
    //ノードの保存
    //private static void SaveNode(GraphAsset graphAsset,GraphView graphView) {
    //    //ウィンドウ上のノードのリスト
    //    var fieldNodeList = graphView.nodes.ToList();
    //    //除外対象を排除
    //    fieldNodeList.RemoveAll(node => node is StartNode);
       
    //    //リストの初期化
    //    graphAsset.nodes = new List<NodeData>();

    //    foreach (var node in fieldNodeList)
    //    {
    //        //場所の追加
    //        graphAsset.nodes.Add(new NodeData());
    //        int listNumber = graphAsset.nodes.ToList().Count - 1;

    //        //位置の保存
    //        graphAsset.nodes[listNumber].position = node.GetPosition().position;
    //        if (node is ScriptNode) {
    //            ScriptNode castScriptNode = node as ScriptNode;
    //            //スクリプトの保存
    //            graphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
    //            //管理番号の保存
    //            graphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;

    //            //対象ノードのアウトプットにつながっているすべてのエッジを保存
    //            var edgeslist=castScriptNode.OutputPort.connections.ToList();
    //            if (edgeslist.Count >= 0) { 
    //                for (int listCount= 0; listCount < edgeslist.Count; listCount++) {
    //                    //保存場所の追加
    //                    graphAsset.nodes[listNumber].edgesDatas.Add(new EdgesData());
    //                    ScriptNode castInputNode= edgeslist[listCount].input.node as ScriptNode;
    //                    //管理番号の保存
    //                    graphAsset.nodes[listNumber].edgesDatas[listCount].controlNumber = listCount;
    //                    //インプット番号の保存(アウトプット番号はこのノードの管理番号なので保存しなくてよい)
    //                    graphAsset.nodes[listNumber].edgesDatas[listCount].inputNodeId = castInputNode.NodeID;
    //                }
    //            }
    //            //対象ノードのextensionContainerにつながっているFieldを保存
    //            int fieldCount = castScriptNode.extensionContainer.childCount;
    //            if (fieldCount >= 0) {
    //                for (int addCount = 0; addCount < fieldCount; addCount++) {


    //                    //TODO　現在はどちらもStringでの保存ほかの方法が見つかればそれに変更
    //                    var fieldElement = castScriptNode.extensionContainer[addCount];

    //                    //fieldElementの名前を取得
    //                    string fieldElementName= fieldElement.name.ToString();
    //                    if (fieldElement is DataElement<FloatField, float>) {
    //                        //保存場所の追加
    //                        graphAsset.nodes[listNumber].fieldData.Add(new FieldData());
    //                        var castFieldElement = fieldElement as DataElement<FloatField,float>;
    //                        //型の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].typeName = "System.Single";
    //                        ////名前の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].fieldName= castFieldElement.fieldNameLabel.text;
    //                        ////値の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].valueData = castFieldElement.Field.value.ToString();
    //                        continue;
    //                    }
    //                    if(fieldElement is DataElement<IntegerField, int>){
    //                        //保存場所の追加
    //                        graphAsset.nodes[listNumber].fieldData.Add(new FieldData());
    //                        var castFieldElement = fieldElement as DataElement<IntegerField, int>;
    //                        //型の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].typeName = "System.Int32";
    //                        ////名前の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].fieldName = castFieldElement.fieldNameLabel.text;
    //                        ////値の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].valueData = castFieldElement.Field.value.ToString();
    //                        continue;
    //                    }
    //                    if(fieldElement is DataElement<Toggle, bool>)
    //                    {
    //                        //保存場所の追加
    //                        graphAsset.nodes[listNumber].fieldData.Add(new FieldData());
    //                        var castFieldElement = fieldElement as DataElement<Toggle, bool>;
    //                        //型の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].typeName = "System.Boolean";
    //                        ////名前の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].fieldName = castFieldElement.fieldNameLabel.text;
    //                        ////値の保存
    //                        graphAsset.nodes[listNumber].fieldData[addCount].valueData = castFieldElement.Field.value.ToString();
    //                        continue;
    //                    }
    //                    if (fieldElement is ObjectElement)
    //                    {
    //                        graphAsset.nodes[listNumber].fieldDataObject.Add(new FieldDataObject());
    //                        var castFieldElement = fieldElement as ObjectElement;
    //                        //型の保存
    //                        graphAsset.nodes[listNumber].fieldDataObject[0].typeName = "UnityEngine.GameObject";
    //                        //名前の保存
    //                        graphAsset.nodes[listNumber].fieldDataObject[0].fieldName = castFieldElement.fieldNameLabel.text;
    //                        //値の保存
    //                        var valueob = castFieldElement.objectField.value;
    //                        Object @object= valueob as Object;

    //                        graphAsset.nodes[listNumber].fieldDataObject[0].valueData = @object;
    //                        continue;
    //                    }
    //                    Debug.LogError("未分類のFieldElementがありました");
    //                }
    //            }


    //        }

    //    }
    //    //管理番号のソート
    //    graphAsset.nodes.Sort((node1,node2)=>node1.controlNumber-node2.controlNumber);
    //}
    //エッジの保存
    private static void SaveEdgs(GraphAsset graphAsset, GraphView graphView) {
        //ウィンドウ上のエッジのリスト
        var fieldEdgslist = graphView.edges.ToList();
        Debug.Log(fieldEdgslist.Count());
        //リストの初期化
        graphAsset.edges = new List<EdgeData>();
        //除外対象を排除
        fieldEdgslist.RemoveAll(i => i.output.node is StartNode);
        //テスト用に簡素で
        foreach (var edge in fieldEdgslist.Select((v, i) => new { value = v, Index = i }))
        {
            //場所の追加
            graphAsset.edges.Add(new EdgeData());
            if (edge.value.input.node is ScriptNode){
                ScriptNode castScriptNode= edge.value.input.node as ScriptNode;
                graphAsset.edges[edge.Index].inputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("input側での接続先が保存できませんでした。管理番号が振られていない可能性があります");
            if (edge.value.output.node is ScriptNode){
                ScriptNode castScriptNode = edge.value.output.node as ScriptNode;
                graphAsset.edges[edge.Index].outputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("output側での接続先が保存できませんでした。管理番号が振られていない可能性があります");

        }
    }
    private static Type ListReset<Type>(Type listData)
    where Type:IComparer{
        return default;
    }
}
