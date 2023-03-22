using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class RealTimeNode : GraphViewScriptBase
{
    public override void BTStart()
    {       
        Debug.Log("RealTimeNode�ł���" + Time.realtimeSinceStartup);
    }
    public override void BTNext(SMManager sMManager)
    {
        base.BTNext(sMManager);
    }
}
