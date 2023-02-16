using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeNode : GraphViewScriptBase
{
    public override void BTStart()
    {
        Debug.Log("RealTimeNodeÇ≈Ç∑ÅÅ" + Time.realtimeSinceStartup);
    }
}
