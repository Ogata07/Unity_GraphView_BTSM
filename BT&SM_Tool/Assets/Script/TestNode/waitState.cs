using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class waitState: GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        BTNext(m_SMManager, 2);
    }
}
