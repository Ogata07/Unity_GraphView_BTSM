using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class scaleDownState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    private float time = default;
    private float settime = 10;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("scaleDownState‚Å‚·");
        m_SMManager = manager;
        time = settime;
    }
    public override void BTUpdate()
    {
        if (this.transform.localScale.x > 0) { 
        this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        time -= Time.deltaTime;
        if (time < 0)
            BTNext(m_SMManager);
    }
}
