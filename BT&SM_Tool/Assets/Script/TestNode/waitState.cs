using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class waitState: GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("waitState�ł�AorS�ňڍs���܂�");
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("�ڍs���܂�");
            //�ړ���
            BTNext(m_SMManager, 1);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            //�T�C�Y�ύX��
            Debug.Log("�ڍs���܂�");
            BTNext(m_SMManager, 2);
        }
    }
}
