using System;
using Unity.VisualScripting;
using UnityEditor.Graphs.AnimationBlendTree;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 保存データから生成した時のNodeの生成をするクラス
/// </summary>
public class CreateNode
{
    private  ScriptFieldCheck scriptFieldCheck = new ScriptFieldCheck();
    private ConvertSaveData convertSaveData = new ConvertSaveData();
    private  Vector2 defaltSize = new Vector2(100,100);
    public  void Create(NodeData nodeData, GraphViewManager graphViewManager) {
        if (nodeData.scriptID == NodeType.SM) {
            ScriptNode node = new ScriptNode();
            //初期設定
            SetNodeInitial(nodeData, node);

            //スタートノードのみ色を変える
            if (node.NodeID == 0)
            {
                graphViewManager.Sm_StartNode = node;
                node.name = "Start";
                graphViewManager.NodeTitleColorChange(node, graphViewManager.StartColorCode);
            }
            //FieldElementの追加
            if (nodeData.@object != null)
                SetNodeFields(nodeData, node);
            //extensionContainerに追加したら忘れず実行しないと隠されてしまう
            node.RefreshExpandedState();
            //画面に追加
            graphViewManager.AddElement(node);
        }
        if (nodeData.scriptID == NodeType.BT_Selector) {
            SelectorNode node = new SelectorNode();
            //ノードの位置
            node.SetPosition(new Rect(nodeData.position, defaltSize));
            //管理番号
            node.NodeID = nodeData.controlNumber;
            //
            //node.enumField.value = (SelectorNode.SelectorTipe)int.Parse(nodeData.stringValue);
            try { 
            node.enumField.value = (SelectorNode.SelectorTipe)Enum.Parse(typeof(SelectorNode.SelectorTipe), nodeData.stringValue);
            }
            catch (ArgumentException)
            {
                Debug.LogError("SelectorNodeのSelectorTipeに含まれていない値がstringValueに保存されています");
            }
            //node.enumField.value = (SelectorNode.SelectorTipe)2;
            //画面に追加
            graphViewManager.AddElement(node);
        }

    }
    /// <summary>
    /// Nodeの初期設定を行う
    /// </summary>
    /// <param name="nodeData">データ本体</param>
    /// <param name="node">追加する場所</param>
    private  void SetNodeInitial(NodeData nodeData, ScriptNode node) {
        //ノードの位置
        node.SetPosition(new Rect(nodeData.position, defaltSize));
        //スクリプト
        if (nodeData.@object != null)
            node.ObjectField.value = nodeData.@object;
        //名前（予定）
        //管理番号
        node.NodeID = nodeData.controlNumber;
    }
    /// <summary>
    /// データからFieldElementを追加する
    /// </summary>
    /// <param name="nodeData">データ本体</param>
    /// <param name="node">追加する場所</param>
    private  void SetNodeFields(NodeData nodeData,ScriptNode node) {
        //追加する数を集計
        int fieldCount = nodeData.fieldData.Count;
        //スクリプトからfieldの作成　

        scriptFieldCheck.Check(nodeData.@object, node);
        //
        int fieldElementCount = node.extensionContainer.childCount;
        Debug.Log(fieldElementCount);

        //GraphView上のfieldと保存先のデータを比べて異なっていたら保存先のデータを挿入
        for (int chackCount = 0; chackCount < fieldElementCount; chackCount++)
        {
            VisualElement child = node.extensionContainer[chackCount];
            //Float型
            if (child is DataElement<FloatField, float>)
            {
                convertSaveData.ChackSaveData<FloatField, float>(child, nodeData);
            }
            //Int型
            if (child is DataElement<IntegerField, int>)
            {
                convertSaveData.ChackSaveData<IntegerField, int>(child, nodeData);
            }
            //Bool型
            if (child is DataElement<Toggle, bool>)
            {
                convertSaveData.ChackSaveData<Toggle, bool>(child, nodeData);
            }
            //GameObject型
            if (child is ObjectElement)
            {
                ////変換
                var castObject = child as ObjectElement;
                ////名前の取得
                string loadFieldName = castObject.fieldNameLabel.text;
                ////保存先のデータから同じ名前のフィールドがないか探す
                FieldDataObject nodeData1 = nodeData.fieldDataObject.Find(f => f.fieldName == loadFieldName);
                if (nodeData1 != null && nodeData1.valueData != null)
                {
                    GameObject objectValue = (GameObject)nodeData1.valueData;
                    castObject.objectField.value = objectValue;
                }
            }
        }

    }

}

