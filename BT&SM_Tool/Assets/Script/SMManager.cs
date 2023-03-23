using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ステートマシンのヴィジュアルスクリプティングを動作させるスクリプト
/// </summary>
public class SMManager : MonoBehaviour
{
    [SerializeField, Header("実行するデータ")]
    private GraphAsset graphAsset;
    private GraphViewScriptBase graphViewScriptBase;
    private int activeNodeId = 0;
    private bool startFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
        var startNode = graphAsset.nodes[activeNodeId].Object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));

        graphViewScriptBase = activeScript as GraphViewScriptBase;
        //ScriptSet(activeNodeId);
        //if (graphViewScriptBase!=null)
        graphViewScriptBase.BTStart(this);
        startFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //BTStartを実行していないならそっちを優先(flag？)
        //if (graphViewScriptBase != null)
        if (startFlag == false)
            graphViewScriptBase.BTUpdate();
        else
        {
            graphViewScriptBase.BTStart(this);
            startFlag = false;
        }
    }
    //次のステートに移行
    public void Next()
    {
        Debug.Log("次のノードに移行します");
        ScriptChange();
        //エッジを確認して次がある確認
        //ノードデータに繋がっているノードデータも渡してもよかったかも
        //graphAsset.edges.FindAll(i=>i.outputNodeId==1);
        //次が無ければnullを返す(次がないのにNextを呼んでる方が悪い)
        //次がある場合は次のGraphViewScriptBaseを渡す
    }
    private void ScriptChange()
    {
        //TODO ListをDictionaryで作り直した方がいいかも(エッジにも番号振って管理もあり)
        //エッジの中から現在の実行ノードに繋がっているエッジを探す
        var outputEdge =graphAsset.edges.Find(i=>i.outputNodeId== activeNodeId);
        //探したらそれにつながっているinputNodeに繋がっているノード番号を取得する
        var inputNodeId = outputEdge.inputNodeId;
        //その番号のノードスクリプトを実行する
        ScriptSet(inputNodeId);
    }
    private void ScriptSet(int nodeId) {
        activeNodeId=nodeId;
        var startNode = graphAsset.nodes[nodeId].Object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        startFlag = true;
    }
}
