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
}