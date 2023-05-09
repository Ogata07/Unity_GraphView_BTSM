using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class scaleDownState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    private float time = default;
    public float settime = 5;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("scaleDownState‚Å‚·");
        m_SMManager = manager;
        time = settime;
    }
    public override void BTUpdate()
    {
        if (m_SMManager.transform.localScale.x > 0) {
            m_SMManager.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
        time -= Time.deltaTime;
        if (time < 0)
            BTNext(m_SMManager);
    }
}
