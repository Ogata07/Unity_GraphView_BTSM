using System;
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
    /// <summary>
    /// ノードの生成
    /// </summary>
    /// <param name="nodeData"></param>
    /// <param name="graphViewManager"></param>
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
        //fieldエレメント追加
        //fieldエレメント追加
        //追加する数を集計
        int fieldCount=nodeData.fieldData.Count;
        //回数分生成を回す
        for (int fieldNumber = 0; fieldNumber < fieldCount; fieldNumber++) { 
            //型名取得
            string TypeName = nodeData.fieldData[fieldNumber].typeName;
            //名前の取得
            string fieldName= nodeData.fieldData[fieldNumber].fieldName;
            //値の取得
            string Value = nodeData.fieldData[fieldNumber].valueData;
            switch (TypeName)
            {
                case "System.Single":
                    float floatvalue =Convert.ToSingle(Value);
                    node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, floatvalue));
                    break;
                case "System.Int32":
                    int intvalue = Convert.ToInt32(Value);
                    node.extensionContainer.Add(new DataElement<IntegerField, int>(fieldName, intvalue));
                    break;
                case "System.Boolean":
                    //bool boolvalue = Convert.ToBoolean(Value);
                    //node.extensionContainer.Add(new DataElement<Toggle, bool>(fieldName, boolvalue));
                    break;
                case "UnityEngine.GameObject":
                    //GameObject obhectvalue = Convert.(Value);
                    //node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, obhectvalue));
                    break;
                default:
                    break;
            }
        }
        //extensionContainerに追加したら忘れず実行しないと隠されてしまう
        node.RefreshExpandedState();
        //画面に追加
        graphViewManager.AddElement(node);
    } 
    /// <summary>
    /// エッジの生成
    /// </summary>
    /// <param name="edgeData"></param>
    /// <param name="graphViewManager"></param>
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
