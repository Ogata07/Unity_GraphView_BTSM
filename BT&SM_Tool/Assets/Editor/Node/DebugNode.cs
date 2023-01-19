using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : GraphViewScriptBase
{
    public override void BTStart()
    {
        Debug.Log("OK="+Time.realtimeSinceStartup);
    }
}
