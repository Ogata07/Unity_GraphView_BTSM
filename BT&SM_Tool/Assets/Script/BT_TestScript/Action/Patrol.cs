using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
/// <summary>
/// 巡回データを取得して巡回を開始する
/// </summary>
public class Patrol : GraphViewScriptBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("巡回");
    }
}
    