using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class RealTimeNode : GraphViewScriptBase
{
    private int time = 100;
    private SMManager sMManager = default;

    public override void BTStart(SMManager manager)
    {       
        Debug.Log("RealTimeNodeです＝" + Time.realtimeSinceStartup);
        sMManager = manager;
    }
    public override void BTUpdate()
    {
        time--;
        //Debug.Log("次のノード移行まで" + time);
        if (time < 0)
            BTNext(sMManager);
    }
}
