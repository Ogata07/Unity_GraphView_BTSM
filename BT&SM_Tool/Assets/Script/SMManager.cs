using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステートマシンのヴィジュアルスクリプティングを動作させるスクリプト
/// </summary>
public class SMManager : MonoBehaviour
{
    [SerializeField, Header("実行するデータ")]
    private GraphAsset graphAsset;
    private GraphViewScriptBase graphViewScriptBase;
    // Start is called before the first frame update
    void Start()
    {
        var startNode = graphAsset.nodes[0].Object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        graphViewScriptBase.BTStart();
    }

    // Update is called once per frame
    void Update()
    {
        graphViewScriptBase.BTStart();
    }
    //次のステートに移行
    public void Next()
    {
        //エッジを確認して次がある確認
        //ノードデータに繋がっているノードデータも渡してもよかったかも
        graphAsset.edges.FindAll(i=>i.outputNodeId==1);
        //次が無ければnullを返す(次がないのにNextを呼んでる方が悪い)
        //次がある場合は次のGraphViewScriptBaseを渡す
    }
}
