using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
/// <summary>
///　死亡アニメーションを再生する
/// </summary>
public class Die : GraphViewScriptBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("死亡");
    }
}
    