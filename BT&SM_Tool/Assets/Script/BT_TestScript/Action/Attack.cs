using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class Attack : GraphViewScriptBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("攻撃");
    }
}
    