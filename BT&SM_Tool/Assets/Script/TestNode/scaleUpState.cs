using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class scaleUpState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    private float time = default;
    public float settime = 5;
    private int a = 0;
    private decimal b = 0;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("scaleUpState‚Å‚·");
        m_SMManager = manager;
        time = settime;
    }
    public override void BTUpdate()
    {
        if (m_SMManager.transform.localScale.x < 1)
        {
            m_SMManager.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        time -= Time.deltaTime;
        if (time < 0)
            BTNext(m_SMManager);
    }
}
