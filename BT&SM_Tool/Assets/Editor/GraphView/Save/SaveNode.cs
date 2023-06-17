using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
/// <summary>
/// GraphViewのNodeのデータを保存するクラス
/// </summary>
public class SaveNode 
{
    private SaveField saveField = new SaveField();
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
                graphAsset.nodes[listNumber].@object = castScriptNode.ObjectField.value;
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
                        VisualElement fieldElement = castScriptNode.extensionContainer[addCount];
                        saveField.ChackField(fieldElement,graphAsset,listNumber);
                    }
                }
            }

        }
        //管理番号のソート
        graphAsset.nodes.Sort((node1, node2) => node1.controlNumber - node2.controlNumber);
    }
}
