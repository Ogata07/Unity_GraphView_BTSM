using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
/// 各Nodeに管理番号を付与するクラス
/// </summary>
public class ControlNumberAdd
{
    private  int number = 0;
    //各ノードに管理番号を付与
    public  void ControlNumber(GraphView graphView)
    {
        number = 0;
        var nodeList = graphView.nodes.ToList();
        //(ステートマシン限定)(特定のノードを排除してそれ以外を順番に管理番号を付与する)
        //スタートノードに番号を振る(0番)
        var deleteStartNode = nodeList.Find(x => x.title == "StartNode") as StartNode;
        nodeList.Remove(deleteStartNode);
        var startNode = nodeList.Find(x => x.name == "Start") as ScriptNode;

        //
        if (startNode is ScriptNode)
        {
            ScriptNode castScriptNode = startNode as ScriptNode;
            castScriptNode.NodeID = number;
            number++;
        }
        nodeList.Remove(startNode);
        foreach (Node node in nodeList)
        {
            AddNumbar(node);
        }
    }
    //管理番号を付与する
    private  void AddNumbar(Node node)
    {
        //管理番号を付与する
        //スクリプトノードしか番号を振れない
        //TODO セーブした後に追加するとうまく挙動しない
        if (node is ScriptNode)
        {
            ScriptNode castScriptNode = node as ScriptNode;
            castScriptNode.NodeID = number;
            number++;
        }
        else
            Debug.LogError("番号を振るのに対応していません");
    }

}
