using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;

public class waitState: GraphViewScriptBase
{
    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("waitStateですAorSで移行します");
        m_SMManager = manager;
    }
    public override void BTUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("移行します");
            //移動に
            BTNext(m_SMManager, 1);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            //サイズ変更に
            Debug.Log("移行します");
            BTNext(m_SMManager, 2);
        }
    }
}
