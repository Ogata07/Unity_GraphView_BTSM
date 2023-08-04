using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
///　GraphViewのデータを保存するときのまとめクラス
/// </summary>
public class GraphViewSave
{
    private  int number = 0;
    private  ControlNumberAdd controlNumberAdd = new ControlNumberAdd();
    private  SaveNode saveNode = new ();
    private SaveExternalVariable saveExternalVariable = new ();
    public  void SaveNodeElement(GraphAsset graphAsset, GraphViewManager graphViewManager) 
    {
        Debug.Log("セーブの開始"); 
        //ノード
        Debug.Log("ノードの数は"+ graphViewManager.nodes.ToList().Count+"個");
        //エッジ
        Debug.Log("エッジの数は" + graphViewManager.edges.ToList().Count + "個");
        number = 0;

        //TODO 未完成ですロード部分はまだ
        var window = graphViewManager.SetGraphEditorWindow;
        var backBord = window.rootVisualElement.Q<TestBackBord>("TestBackBord");
        var name = backBord as TestBackBord;
        graphAsset.keyValues=new System.Collections.Generic.List<ExternalVariable> ();
        foreach (var i in name.visualElements) { 
            Debug.Log(i);
            saveExternalVariable.ChackVariable(i,graphAsset);
        }


        controlNumberAdd.ControlNumber(graphAsset, graphViewManager);
        saveNode.Save(graphAsset, graphViewManager);
    }
}
