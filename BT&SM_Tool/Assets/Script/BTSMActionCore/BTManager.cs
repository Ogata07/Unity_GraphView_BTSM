using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEditorInternal;
/// <summary>
/// ビヘイビアツリーのヴィジュアルスクリプティングを動作させるスクリプト
/// </summary>
public class BTManager : MonoBehaviour
{
    [SerializeField, Header("実行するデータ")]
    private GraphAsset graphAsset;
    private GraphViewScriptBase graphViewScriptBase;
    //現在実行中のノードの管理番号
    private int activeNodeId = 0;
    private List<GraphViewScriptBase> graphViewScriptBases = new();
    private List<ConditionBase> conditionBases = new(); 
    // Start is called before the first frame update
    void Start()
    {
        /*
        var startNode = graphAsset.nodes[0].@object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        graphViewScriptBase.BTStart();
        */
        //TODO 分岐ができていないので現時点ではここまで
        //BT_Conditionのスクリプトを取得する
        for (int i = 0; i <= graphAsset.nodes.Count-1; i++) {

            if (graphAsset.nodes[i].scriptID == NodeType.BT_Condition) {
                var startNode = graphAsset.nodes[i].@object;
                var scriptName = startNode.name;
                var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
                graphViewScriptBases.Add((GraphViewScriptBase)activeScript) ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Normal");
        foreach (GraphViewScriptBase unit in graphViewScriptBases) {
            Debug.Log("mawasityuu");
            unit.BTUpdate();
            if (unit is ConditionBase) {
                Debug.Log("dekru");
            }
        }
        Search();
    }
    private void Search() {
        //現在のnode番号をリセット
        activeNodeId = 0;
        //Node0の要素を読み取る
        var startNode = graphAsset.nodes[activeNodeId];
        //今回はNode0はSelectorNodeで固定
        Selector(startNode.stringValue);
        //読み取った動作を実行する
        //①繰り返し
        //決定したNodeを読み取る
        //読み取った動作を実行する
        //次のノードがある場合は①に戻る
        //次のノードがなければ挿入して実行する
    }
    private int Selector(string value) {
        Debug.Log(value);
        var returnValue = 100;
        if (value == "Priority")
            returnValue = Priority();
        if(value == "Random")
            returnValue=Random();
        return returnValue;
    }
    /// <summary>
    /// Nodeの位置が上の方を返却する
    /// </summary>
    private int Priority() {
        Debug.Log("Priorityが選択されました");
        List<int> list = new();
        //現在のnodeとつながっているのを取得する
        foreach (var i in graphAsset.nodes[activeNodeId].edgesDatas) {
            list.Add(i.inputNodeId);
        }
        list.Sort();
        return list[0];
    }
    /// <summary>
    /// ランダムにノードを決定する
    /// </summary>
    private int Random() {
        List<int> list = new();
        //現在のnodeとつながっているのを取得する
        foreach (var i in graphAsset.nodes[activeNodeId].edgesDatas)
        {
            list.Add(i.inputNodeId);
        }
        System.Random random = new();
        var randomvalue= random.Next(list.Count);
        return list[randomvalue];
    }
}
