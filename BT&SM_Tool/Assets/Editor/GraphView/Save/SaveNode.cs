using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveNode 
{
    public  void Save(GraphAsset graphAsset, GraphView graphView)
    {
        //ウィンドウ上のノードのリスト
        var fieldNodeList = graphView.nodes.ToList();

        //除外対象を排除
        fieldNodeList.RemoveAll(node => node is StartNode);

        //リストの初期化
        graphAsset.nodes = new List<NodeData>();

        foreach (var node in fieldNodeList)
        {
            //場所の追加
            graphAsset.nodes.Add(new NodeData());
            int listNumber = graphAsset.nodes.ToList().Count - 1;

            //位置の保存
            graphAsset.nodes[listNumber].position = node.GetPosition().position;
            if (node is ScriptNode)
            {
                ScriptNode castScriptNode = node as ScriptNode;
                //スクリプトの保存
                graphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
                //管理番号の保存
                graphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;

                ////対象ノードのアウトプットにつながっているすべてのエッジを保存
                var edgeslist = castScriptNode.OutputPort.connections.ToList();
                if (edgeslist.Count >= 0)
                {
                    for (int listCount = 0; listCount < edgeslist.Count; listCount++)
                    {
                        //保存場所の追加
                        graphAsset.nodes[listNumber].edgesDatas.Add(new EdgesData());
                        ScriptNode castInputNode = edgeslist[listCount].input.node as ScriptNode;
                        //管理番号の保存
                        graphAsset.nodes[listNumber].edgesDatas[listCount].controlNumber = listCount;
                        //インプット番号の保存(アウトプット番号はこのノードの管理番号なので保存しなくてよい)
                        graphAsset.nodes[listNumber].edgesDatas[listCount].inputNodeId = castInputNode.NodeID;
                    }
                }
                //対象ノードのextensionContainerにつながっているFieldを保存
                int fieldCount = castScriptNode.extensionContainer.childCount;
                if (fieldCount >= 0)
                {
                    for (int addCount = 0; addCount < fieldCount; addCount++)
                    {


                        //TODO　現在はどちらもStringでの保存ほかの方法が見つかればそれに変更
                        var fieldElement = castScriptNode.extensionContainer[addCount];

                        //fieldElementの名前を取得
                        string fieldElementName = fieldElement.name.ToString();
                        if (fieldElement is DataElement<FloatField, float> floatElement)
                        {
                            AddFieldData(graphAsset, listNumber, "System.Single", floatElement.fieldNameLabel.text, floatElement.Field.value.ToString());
                            continue;
                        }
                        if (fieldElement is DataElement<IntegerField, int> intElement)
                        {
                            AddFieldData(graphAsset,listNumber, "System.Int32", intElement.fieldNameLabel.text, intElement.Field.value.ToString());
                            continue;
                        }
                        if (fieldElement is DataElement<Toggle, bool> boolElement)
                        {
                            AddFieldData(graphAsset, listNumber, "System.Boolean", boolElement.fieldNameLabel.text, boolElement.Field.value.ToString());
                            continue; 
                        }

                        if (fieldElement is ObjectElement)
                        {
                            graphAsset.nodes[listNumber].fieldDataObject.Add(new FieldDataObject());
                            var castFieldElement = fieldElement as ObjectElement;
                            //型の保存
                            graphAsset.nodes[listNumber].fieldDataObject[0].typeName = "UnityEngine.GameObject";
                            //名前の保存
                            graphAsset.nodes[listNumber].fieldDataObject[0].fieldName = castFieldElement.fieldNameLabel.text;
                            //値の保存
                            var valueob = castFieldElement.objectField.value;
                            Object @object = valueob as Object;

                            graphAsset.nodes[listNumber].fieldDataObject[0].valueData = @object;
                            continue;
                        }
                        Debug.LogError("未分類のFieldElementがありました");
                    }
                }


            }

        }
        //管理番号のソート
        graphAsset.nodes.Sort((node1, node2) => node1.controlNumber - node2.controlNumber);
    }
    void AddFieldData(GraphAsset graphAsset, int listNumber,string typeName, string fieldName, string value)
    {
        graphAsset.nodes[listNumber].fieldData.Add(new FieldData()
        {
            typeName = typeName,
            fieldName = fieldName,
            valueData = value
        });
    }
}
