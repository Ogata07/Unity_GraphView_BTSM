using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
/// <summary>
/// 現地点にとどまって待機モーションを実行する
/// </summary>
public class Idle : GraphViewScriptBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("待機");
    }
}
    