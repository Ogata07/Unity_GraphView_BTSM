using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// GraphAssetの内容をエディタウィンドウに表示する
/// </summary>
public static class GraphViewLoad 
{
    private static CreateNode  createNode=new CreateNode();
    public static void LoadNodeElement(GraphAsset graphAsset) {
        GraphEditorWindow.ShowWindow(graphAsset);
    }
    private static ScriptFieldCheck scriptFieldCheck = new ScriptFieldCheck();
    //データからの作成
    public static void CreateGraphView(GraphViewManager graphViewManager) {
        GraphAsset loadGraphAsset = graphViewManager.m_GraphAsset;

        foreach (var node in loadGraphAsset.nodes) {
            //CreateNode2(node,graphViewManager);
            createNode.Create(node, graphViewManager);
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
    //private static void CreateNode2(NodeData nodeData,GraphViewManager graphViewManager) {
    //    var node = new ScriptNode();
    //    //ノードの位置
    //    node.SetPosition(new Rect(nodeData.position, new UnityEngine.Vector2(100,100)));
    //    //名前（予定）
    //    //スクリプト
    //    if (nodeData.Object != null)
    //        node.ObjectField.value = nodeData.Object;
    //    //管理番号
    //    node.NodeID = nodeData.controlNumber;
    //    //スタートノードのみ
    //    if (node.NodeID == 0) {
    //        graphViewManager.sm_StartNode = node;
    //        node.name = "Start";
    //        graphViewManager.NodeTitleColorChange(node, graphViewManager.startColorCode);
    //    }
    //    //fieldエレメント追加
    //    //fieldエレメント追加
    //    //追加する数を集計
    //    int fieldCount=nodeData.fieldData.Count;
    //    //スクリプトからfieldの作成　
    //    scriptFieldCheck.Check(nodeData.Object, node);
    //    //
    //    int fieldElementCount= node.extensionContainer.childCount;
    //    Debug.Log(fieldElementCount);
        
    //    //GraphView上のfieldと保存先のデータを比べて異なっていたら保存先のデータを挿入
    //    for (int chackCount = 0; chackCount < fieldElementCount; chackCount++) {
    //        VisualElement child = node.extensionContainer[chackCount];
    //        //Float型
    //        if (child is DataElement<FloatField, float>)
    //        {
    //            ChackSaveData<FloatField, float>(child, nodeData);
    //        }
    //        //Int型
    //        if (child is DataElement<IntegerField, int>)
    //        {
    //            ChackSaveData<IntegerField, int>(child, nodeData);
    //        }
    //        //Bool型
    //        if (child is DataElement<Toggle, bool>)
    //        {
    //            ChackSaveData<Toggle, bool>(child, nodeData);
    //        }
    //        //GameObject型
    //        if (child is ObjectElement) {
    //            ////変換
    //            var castObject = child as ObjectElement;
    //            ////名前の取得
    //            string loadFieldName = castObject.fieldNameLabel.text;
    //            ////保存先のデータから同じ名前のフィールドがないか探す
    //            FieldDataObject nodeData1 = nodeData.fieldDataObject.Find(f => f.fieldName == loadFieldName);
    //            if (nodeData1 != null && nodeData1.valueData != null)
    //            {
    //                GameObject objectValue = (GameObject)nodeData1.valueData;
    //                castObject.objectField.value = objectValue;
    //            }
    //        }
    //    }

    //    //extensionContainerに追加したら忘れず実行しないと隠されてしまう
    //    node.RefreshExpandedState();
    //    //画面に追加
    //    graphViewManager.AddElement(node);
    //}
    //static void ChackSaveData<T,V>( VisualElement childon,  NodeData nodeData)
    //where T : BaseField<V>, new()
    //{
    //    //変換
    //    var castInt = childon as DataElement<T, V>;
    //    //名前の取得
    //    string loadFieldName = castInt.fieldNameLabel.text;
    //    //保存先のデータから同じ名前のフィールドがないか探す
    //    FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
    //    if (nodeData1 != null)
    //    {
    //        //dynamic
    //        if (typeof(V) == typeof(float)) 
    //            castInt.Field.value = (V)(object)Convert.ToSingle(nodeData1.valueData);
    //        if (typeof(V) == typeof(int))
    //            castInt.Field.value = (V)(object)Convert.ToInt32(nodeData1.valueData);
    //        if (typeof(V) == typeof(bool))
    //            castInt.Field.value = (V)(object)Convert.ToBoolean(nodeData1.valueData);
    //    }
    //}
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
