using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class DebugNode : GraphViewScriptBase
{
    private int time = 100;
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("DebugNode‚Å‚·");
        m_SMManager=manager;
    }
    public override void BTUpdate()
    {
        time--;
        //Debug.Log("Ÿ‚Ìƒm[ƒhˆÚs‚Ü‚Å"+ time);
        if (time < 0)
            BTNext(m_SMManager);
    }
}
