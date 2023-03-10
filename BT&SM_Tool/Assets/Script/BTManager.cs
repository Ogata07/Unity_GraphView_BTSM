using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
using System;
using UnityEditorInternal;
/// <summary>
/// ビヘイビアツリーのヴィジュアルスクリプティングを動作させるスクリプト
/// </summary>
public class BTManager : MonoBehaviour
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
        //TODO 分岐ができていないので現時点ではここまで
    }

    // Update is called once per frame
    void Update()
    {
        //graphViewScriptBase.BTUpdate();
    }
}
