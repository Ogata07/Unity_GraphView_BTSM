using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
/// <summary>
/// 巡回範囲に入ったプレイヤーを追跡する
/// </summary>
public class Tracking : GraphViewScriptBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("追跡です");
    }
}
    