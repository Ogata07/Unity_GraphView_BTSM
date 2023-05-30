using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;
/// <summary>
/// GraphAssetの内容をエディタウィンドウに表示する
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
    private static ScriptFieldCheck scriptFieldCheck = new ScriptFieldCheck();
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
        //for (int fieldNumber = 0; fieldNumber < fieldCount; fieldNumber++) { 
        //    //型名取得
        //    string TypeName = nodeData.fieldData[fieldNumber].typeName;
        //    //名前の取得
        //    string fieldName= nodeData.fieldData[fieldNumber].fieldName;
        //    //値の取得
        //    string Value = nodeData.fieldData[fieldNumber].valueData;
        //    switch (TypeName)
        //    {
        //        case "System.Single":
        //            float floatvalue =Convert.ToSingle(Value);
        //            node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, floatvalue));
        //            break;
        //        case "System.Int32":
        //            int intvalue = Convert.ToInt32(Value);
        //            node.extensionContainer.Add(new DataElement<IntegerField, int>(fieldName, intvalue));
        //            break;
        //        case "System.Boolean":
        //            //bool boolvalue = Convert.ToBoolean(Value);
        //            //node.extensionContainer.Add(new DataElement<Toggle, bool>(fieldName, boolvalue));
        //            break;
        //        case "UnityEngine.GameObject":
        //            //GameObject obhectvalue = Convert.(Value);
        //            //node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, obhectvalue));
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //スクリプトからfieldの作成　
        scriptFieldCheck.Check(nodeData.Object, node);
        //
        int fieldElementCount= node.extensionContainer.childCount;
        Debug.Log(fieldElementCount);
        
        //GraphView上のfieldと保存先のデータを比べて異なっていたら保存先のデータを挿入
        for (int chackCount = 0; chackCount < fieldElementCount; chackCount++) {
            VisualElement child = node.extensionContainer[chackCount];
            //Float型
            if (child is DataElement<FloatField, float>)
            {
                ////変換
                //var castfloat = childon as DataElement<FloatField, float>;
                ////名前の取得
                //string loadFieldName = castfloat.fieldNameLabel.text;
                ////保存先のデータから同じ名前のフィールドがないか探す
                //FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
                //if (nodeData1 != null)
                //{
                //    float floatvalue = Convert.ToSingle(nodeData1.valueData);
                //    castfloat.Field.value = floatvalue;
                //}
                chackSaveData<FloatField, float>(child, nodeData);
            }
            //Int型
            if (child is DataElement<IntegerField, int>)
            {
                chackSaveData<IntegerField, int>(child, nodeData);
            }
            //Bool型
            if (child is DataElement<Toggle, bool>)
            {
                chackSaveData<Toggle, bool>(child, nodeData);
            }
            //GameObject型
            if (child is ObjectElement) {
                ////変換
                var castfloat = child as DataElement<FloatField, float>;
                ////名前の取得
                string loadFieldName = castfloat.fieldNameLabel.text;
                ////保存先のデータから同じ名前のフィールドがないか探す
                //FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
            }
        }

        //extensionContainerに追加したら忘れず実行しないと隠されてしまう
        node.RefreshExpandedState();
        //画面に追加
        graphViewManager.AddElement(node);
    }
    static void chackSaveData<T,V>( VisualElement childon,  NodeData nodeData)
    where T : BaseField<V>, new()
    {

        //変換
        var castInt = childon as DataElement<T, V>;
        //名前の取得
        string loadFieldName = castInt.fieldNameLabel.text;
        //保存先のデータから同じ名前のフィールドがないか探す
        FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
        if (nodeData1 != null)
        {
            //dynamic
            if (typeof(V) == typeof(float)) 
                castInt.Field.value = (V)(object)Convert.ToSingle(nodeData1.valueData);
            if (typeof(V) == typeof(int))
                castInt.Field.value = (V)(object)Convert.ToInt32(nodeData1.valueData);
            if (typeof(V) == typeof(bool))
                castInt.Field.value = (V)(object)Convert.ToBoolean(nodeData1.valueData);
            //if(typeof(V) == typeof(GameObject))
        }
    }
    static TResult ConvertViaDynamic<T, TResult>(T number)
    {
        return (TResult)(dynamic)number;
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
