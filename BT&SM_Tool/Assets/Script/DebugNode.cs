using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class DebugNode : GraphViewScriptBase
{
    private int time = 10;
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("DebugNode‚Å‚·");
        m_SMManager=manager;
    }
    public override void BTUpdate()
    {
        time--;
        if (time > 0)
            BTNext(m_SMManager);
    }
}
