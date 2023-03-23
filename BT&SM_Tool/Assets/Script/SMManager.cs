using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �X�e�[�g�}�V���̃��B�W���A���X�N���v�e�B���O�𓮍삳����X�N���v�g
/// </summary>
public class SMManager : MonoBehaviour
{
    [SerializeField, Header("���s����f�[�^")]
    private GraphAsset graphAsset;
    private GraphViewScriptBase graphViewScriptBase;
    private int activeNodeId = 0;
    private bool startFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
        var startNode = graphAsset.nodes[activeNodeId].Object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));

        graphViewScriptBase = activeScript as GraphViewScriptBase;
        //ScriptSet(activeNodeId);
        //if (graphViewScriptBase!=null)
        graphViewScriptBase.BTStart(this);
        startFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //BTStart�����s���Ă��Ȃ��Ȃ炻������D��(flag�H)
        //if (graphViewScriptBase != null)
        if (startFlag == false)
            graphViewScriptBase.BTUpdate();
        else
        {
            graphViewScriptBase.BTStart(this);
            startFlag = false;
        }
    }
    //���̃X�e�[�g�Ɉڍs
    public void Next()
    {
        Debug.Log("���̃m�[�h�Ɉڍs���܂�");
        ScriptChange();
        //�G�b�W���m�F���Ď�������m�F
        //�m�[�h�f�[�^�Ɍq�����Ă���m�[�h�f�[�^���n���Ă��悩��������
        //graphAsset.edges.FindAll(i=>i.outputNodeId==1);
        //�����������null��Ԃ�(�����Ȃ��̂�Next���Ă�ł��������)
        //��������ꍇ�͎���GraphViewScriptBase��n��
    }
    private void ScriptChange()
    {
        //TODO List��Dictionary�ō�蒼����������������(�G�b�W�ɂ��ԍ��U���ĊǗ�������)
        //�G�b�W�̒����猻�݂̎��s�m�[�h�Ɍq�����Ă���G�b�W��T��
        var outputEdge =graphAsset.edges.Find(i=>i.outputNodeId== activeNodeId);
        //�T�����炻��ɂȂ����Ă���inputNode�Ɍq�����Ă���m�[�h�ԍ����擾����
        var inputNodeId = outputEdge.inputNodeId;
        //���̔ԍ��̃m�[�h�X�N���v�g�����s����
        ScriptSet(inputNodeId);
    }
    private void ScriptSet(int nodeId) {
        activeNodeId=nodeId;
        var startNode = graphAsset.nodes[nodeId].Object;
        var scriptName = startNode.name;
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        startFlag = true;
    }
}
