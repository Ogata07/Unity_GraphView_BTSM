using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class waitState: GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    public override void SMStart(SMManager manager)
    {
        Debug.Log("waitStateですAorSで移行します");
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("移行します");
            //移動に
            SMNext(m_SMManager, 0);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            //サイズ変更に
            Debug.Log("移行します");
            SMNext(m_SMManager, 1);
        }
    }
}
