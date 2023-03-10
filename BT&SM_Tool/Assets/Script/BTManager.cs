using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptFlow;
using System;
using UnityEditorInternal;
/// <summary>
/// �r�w�C�r�A�c���[�̃��B�W���A���X�N���v�e�B���O�𓮍삳����X�N���v�g
/// </summary>
public class BTManager : MonoBehaviour
{
    [SerializeField, Header("���s����f�[�^")]
    private GraphAsset graphAsset;
    private GraphViewScriptBase graphViewScriptBase;
    // Start is called before the first frame update
    void Start()
    {
        var startNode = graphAsset.nodes[0].Object;


        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        graphViewScriptBase.BTStart();
        //TODO ���򂪂ł��Ă��Ȃ��̂Ō����_�ł͂����܂�
    }

    // Update is called once per frame
    void Update()
    {
        //graphViewScriptBase.BTUpdate();
    }
}
