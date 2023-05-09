using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class moveState : GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    private float time =default;
    public float settime = 10;
    private Rigidbody rigidbody=null;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("moveState‚Å‚·");
        m_SMManager = manager;
        time = settime;
        rigidbody=m_SMManager.GetComponent<Rigidbody>();
    }
    public override void BTUpdate()
    {
        rigidbody.AddForce(Vector3.up);
        time-= Time.deltaTime;
        if(time<0)
            BTNext(m_SMManager);
    }
}
