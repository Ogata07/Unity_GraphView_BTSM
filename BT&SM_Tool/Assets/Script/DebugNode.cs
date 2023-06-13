using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class DebugNode : GraphViewScriptBase
{
    private int time = 100;
    private SMManager sMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("DebugNodeです");
        sMManager=manager;
    }
    public override void BTUpdate()
    {
        time--;
        if (time < 0)
            BTNext(sMManager);
    }
}
