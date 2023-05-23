using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking.Types;
/// <summary>
/// ステートマシンのヴィジュアルスクリプティングを動作させるスクリプト
/// </summary>
public class SMManager : MonoBehaviour
{
    [SerializeField, Header("実行するデータ")]
    private GraphAsset graphAsset;
    //各スクリプトの基底元(実行時はこれを実行させる)
    private GraphViewScriptBase graphViewScriptBase;
    //現在実行中のノードの管理番号
    private int activeNodeId = 0;
    //各スクリプトのStartの実行管理フラグ
    private bool startFlag = false;
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

    void Update()
    {

        //if (graphViewScriptBase != null)
        if (startFlag == false)
            graphViewScriptBase.BTUpdate();
        else
        {
            FieldValueSet();
            graphViewScriptBase.BTStart(this);
            startFlag = false;
        }
    }
    /// <summary>
    /// 次のステートに移行
    /// </summary>
    public void Next()
    {
        Debug.Log("次のノードに移行します");
        ScriptChange();
    }
    /// <summary>
    /// 次のステートに移行(複数版)
    /// </summary>
    /// <param name="count">エッジの管理番号を入力してください</param>
    public void Next(int count) {
        Debug.Log("次のノードに移行します");
        ScriptChange(count);
    }
    /// <summary>
    /// 現在のノードから繋がっているノードの管理番号を取得する
    /// </summary>
    private void ScriptChange()
    {
        //TODO ListをDictionaryで作り直した方がいいかも(エッジにも番号振って管理もあり)
        //エッジの中から現在の実行ノードに繋がっているエッジを探す
        //var outputEdge =graphAsset.edges.Find(i=>i.outputNodeId== activeNodeId);
        //探したらそれにつながっているinputNodeに繋がっているノード番号を取得する
        //var inputNodeId = outputEdge.inputNodeId;
        //その番号のノードスクリプトを実行する
        int inputNodeId = graphAsset.nodes[activeNodeId].edgesDatas[0].inputNodeId;
        ScriptSet(inputNodeId);
        
    }
    private void ScriptChange(int Number) {
        int inputNodeId=graphAsset.nodes[activeNodeId].edgesDatas[Number].inputNodeId;
        ScriptSet(inputNodeId);
    }
    /// <summary>
    /// 管理番号からスクリプトを取得する
    /// </summary>
    /// <param name="nodeId">移行先の管理番号</param>
    private void ScriptSet(int nodeId) {
        //移行先の管理番号をactiveNodeIdに渡す
        activeNodeId = nodeId;
        //管理番号元のスクリプトオブジェクトを取得する
        var startNode = graphAsset.nodes[nodeId].Object;
        //スクリプトの名前を取得
        var scriptName = startNode.name;
        //名前からインスタンス生成をする
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        //キャストして渡す
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        //Startを実行するのでtrueにする
        startFlag = true;
    }
    /// <summary>
    /// 各種実行クラスのスクリプトからFieldを読み込んで保存しているデータ上書きさせる   
    /// </summary>
    private void FieldValueSet() {
        //スクリプトから各種fieldを取得する
        //保存しているデータを取得する
        //同じ名前を比べて保存しているデータで上書きする
        //管理番号でFieldがあるのか調べる
        if (graphAsset.nodes[activeNodeId].fieldData.Count>0) {
            for(int fieldcount=0;fieldcount< graphAsset.nodes[activeNodeId].fieldData.Count; fieldcount++)
            {
                string fieldName = graphAsset.nodes[activeNodeId].fieldData[fieldcount].fieldName.ToString();
                String fieldType=graphAsset.nodes[activeNodeId].fieldData[fieldcount].typeName.ToString();
                string value= graphAsset.nodes[activeNodeId].fieldData[fieldcount].valueData.ToString();
                graphViewScriptBase
                    .GetType()
                    .GetField(fieldName)
                    .SetValue(graphViewScriptBase, StringChange(fieldType,value));

            }
        }
    }
    /// <summary>
    /// String型の値を対応した型に変換しなおして返却するクラスです
    /// </summary>
    /// <param name="TypeName">型の名前(.Net形式で)</param>
    /// <param name="Value">Fieldの値</param>
    /// <returns></returns>
    private object StringChange(string TypeName,String Value) {
        //object型作成
        object value;
        //String型の値をTypeの型に変換する
        switch (TypeName)
        {
            case "System.Single":
                value = Convert.ToSingle(Value);
                break;
            default:
                value = null;
                break;
        }
        //返却
        return value;
    }
    private void OnGUI()
    {
        GUILayout.Label($"現在実行中のノードの管理番号: {activeNodeId}");
        GUILayout.Label($"実行中のスクリプト↓: {graphAsset.nodes[activeNodeId].Object}");
    }
}
