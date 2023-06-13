using ScriptFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 実行時のField各種動作が動いているかテスト用スクリプト
/// </summary>
public class RuntimeFieldTest : GraphViewScriptBase
{
    public float floatValue = 1.2f;
    public int intValue = 1;
    public bool boolValue = false;
    public Vector3 vector3 = new Vector3();
    public GameObject gameObject = default;

    private SMManager m_SMManager = default;
    public override void BTStart(SMManager manager)
    {
        Debug.Log("RuntimeFieldTestです");
        m_SMManager = manager;
        Debug.Log("1:" + floatValue);
        Debug.Log("2:" + intValue);
        Debug.Log("3:" + boolValue);
        Debug.Log("4:" + vector3);
        Debug.Log("5:" + gameObject);
    }
    public override void BTUpdate()
    {
        Debug.Log("1:" + floatValue);
        Debug.Log("2:" + intValue);
        Debug.Log("3:" + boolValue);
        Debug.Log("4:" + vector3);
        Debug.Log("5:" + gameObject);
    }
}
