using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class moveState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log("moveState‚Å‚·");
        BTNext(m_SMManager, 2);
    }
}
