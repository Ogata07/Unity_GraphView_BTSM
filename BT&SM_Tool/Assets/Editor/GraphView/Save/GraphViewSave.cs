using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
///　GraphViewのデータを保存するときのまとめクラス
/// </summary>
public class GraphViewSave
{
    private  int number = 0;
    private  ControlNumberAdd controlNumberAdd = new ControlNumberAdd();
    private  SaveNode saveNode = new SaveNode();
    public  void SaveNodeElement(GraphAsset graphAsset, GraphView graphView) 
    {
        Debug.Log("セーブの開始");
        //ノード
        Debug.Log("ノードの数は"+graphView.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + graphView.edges.ToList().Count + "個");
        number = 0;

        controlNumberAdd.ControlNumber(graphView);
        saveNode.Save(graphAsset,graphView);
    }
}
