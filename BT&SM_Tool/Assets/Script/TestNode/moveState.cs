using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class moveState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    private float time =default;
    private float settime = 10;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("moveState‚Å‚·");
        m_SMManager = manager;
        time = settime;
    }
    public override void BTUpdate()
    {
        this.transform.position += new Vector3(0, 0.1f, 0);
        time-= Time.deltaTime;
        if(time<0)
            BTNext(m_SMManager);
    }
}
