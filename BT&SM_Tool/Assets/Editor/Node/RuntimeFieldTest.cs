using ScriptFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���s����Field�e�퓮�삪�����Ă��邩�e�X�g�p�X�N���v�g
/// </summary>
public class RuntimeFieldTest : GraphViewScriptBase
{
    public float floatValue = 1.2f;
    public int intValue = 1;
    public bool boolValue = true;
    public Vector3 vector3 = new Vector3();
    public GameObject gameObject = default;

    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("RuntimeFieldTest�ł�");
        m_SMManager = manager;
        Debug.Log("1:" + floatValue);
        Debug.Log("2:" + intValue);
        Debug.Log("3:" + boolValue);
        Debug.Log("4:" + vector3);
        Debug.Log("5:" + gameObject);
    }
    public override void BTUpdate()
    {

    }
}
