using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �X�e�[�g�}�V���̃��B�W���A���X�N���v�e�B���O�𓮍삳����X�N���v�g
/// </summary>
public class SMManager : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        graphViewScriptBase.BTStart();
    }
    //���̃X�e�[�g�Ɉڍs
    public void Next()
    {
        //�G�b�W���m�F���Ď�������m�F
        //�m�[�h�f�[�^�Ɍq�����Ă���m�[�h�f�[�^���n���Ă��悩��������
        graphAsset.edges.FindAll(i=>i.outputNodeId==1);
        //�����������null��Ԃ�(�����Ȃ��̂�Next���Ă�ł��������)
        //��������ꍇ�͎���GraphViewScriptBase��n��
    }
}
