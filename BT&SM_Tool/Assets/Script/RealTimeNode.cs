using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class RealTimeNode : GraphViewScriptBase
{
    private int time = 100;
    private SMManager m_SMManager = default;

    public override void BTStart(SMManager manager)
    {       
        Debug.Log("RealTimeNode�ł���" + Time.realtimeSinceStartup);
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        time--;
        //Debug.Log("���̃m�[�h�ڍs�܂�" + time);
        if (time < 0)
            BTNext(m_SMManager);
    }
}
