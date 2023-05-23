using ScriptFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking.Types;
/// <summary>
/// �X�e�[�g�}�V���̃��B�W���A���X�N���v�e�B���O�𓮍삳����X�N���v�g
/// </summary>
public class SMManager : MonoBehaviour
{
    [SerializeField, Header("���s����f�[�^")]
    private GraphAsset graphAsset;
    //�e�X�N���v�g�̊�ꌳ(���s���͂�������s������)
    private GraphViewScriptBase graphViewScriptBase;
    //���ݎ��s���̃m�[�h�̊Ǘ��ԍ�
    private int activeNodeId = 0;
    //�e�X�N���v�g��Start�̎��s�Ǘ��t���O
    private bool startFlag = false;
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

    void Update()
    {

        //if (graphViewScriptBase != null)
        if (startFlag == false)
            graphViewScriptBase.BTUpdate();
        else
        {
            FieldValueSet();
            graphViewScriptBase.BTStart(this);
            startFlag = false;
        }
    }
    /// <summary>
    /// ���̃X�e�[�g�Ɉڍs
    /// </summary>
    public void Next()
    {
        Debug.Log("���̃m�[�h�Ɉڍs���܂�");
        ScriptChange();
    }
    /// <summary>
    /// ���̃X�e�[�g�Ɉڍs(������)
    /// </summary>
    /// <param name="count">�G�b�W�̊Ǘ��ԍ�����͂��Ă�������</param>
    public void Next(int count) {
        Debug.Log("���̃m�[�h�Ɉڍs���܂�");
        ScriptChange(count);
    }
    /// <summary>
    /// ���݂̃m�[�h����q�����Ă���m�[�h�̊Ǘ��ԍ����擾����
    /// </summary>
    private void ScriptChange()
    {
        //TODO List��Dictionary�ō�蒼����������������(�G�b�W�ɂ��ԍ��U���ĊǗ�������)
        //�G�b�W�̒����猻�݂̎��s�m�[�h�Ɍq�����Ă���G�b�W��T��
        //var outputEdge =graphAsset.edges.Find(i=>i.outputNodeId== activeNodeId);
        //�T�����炻��ɂȂ����Ă���inputNode�Ɍq�����Ă���m�[�h�ԍ����擾����
        //var inputNodeId = outputEdge.inputNodeId;
        //���̔ԍ��̃m�[�h�X�N���v�g�����s����
        int inputNodeId = graphAsset.nodes[activeNodeId].edgesDatas[0].inputNodeId;
        ScriptSet(inputNodeId);
        
    }
    private void ScriptChange(int Number) {
        int inputNodeId=graphAsset.nodes[activeNodeId].edgesDatas[Number].inputNodeId;
        ScriptSet(inputNodeId);
    }
    /// <summary>
    /// �Ǘ��ԍ�����X�N���v�g���擾����
    /// </summary>
    /// <param name="nodeId">�ڍs��̊Ǘ��ԍ�</param>
    private void ScriptSet(int nodeId) {
        //�ڍs��̊Ǘ��ԍ���activeNodeId�ɓn��
        activeNodeId = nodeId;
        //�Ǘ��ԍ����̃X�N���v�g�I�u�W�F�N�g���擾����
        var startNode = graphAsset.nodes[nodeId].Object;
        //�X�N���v�g�̖��O���擾
        var scriptName = startNode.name;
        //���O����C���X�^���X����������
        var activeScript = Activator.CreateInstance(Type.GetType(scriptName));
        //�L���X�g���ēn��
        graphViewScriptBase = activeScript as GraphViewScriptBase;
        //Start�����s����̂�true�ɂ���
        startFlag = true;
    }
    /// <summary>
    /// �e����s�N���X�̃X�N���v�g����Field��ǂݍ���ŕۑ����Ă���f�[�^�㏑��������   
    /// </summary>
    private void FieldValueSet() {
        //�X�N���v�g����e��field���擾����
        //�ۑ����Ă���f�[�^���擾����
        //�������O���ׂĕۑ����Ă���f�[�^�ŏ㏑������
        //�Ǘ��ԍ���Field������̂����ׂ�
        if (graphAsset.nodes[activeNodeId].fieldData.Count>0) {
            for(int fieldcount=0;fieldcount< graphAsset.nodes[activeNodeId].fieldData.Count; fieldcount++)
            {
                string fieldName = graphAsset.nodes[activeNodeId].fieldData[fieldcount].fieldName.ToString();
                String fieldType=graphAsset.nodes[activeNodeId].fieldData[fieldcount].typeName.ToString();
                string value= graphAsset.nodes[activeNodeId].fieldData[fieldcount].valueData.ToString();
                graphViewScriptBase
                    .GetType()
                    .GetField(fieldName)
                    .SetValue(graphViewScriptBase, StringChange(fieldType,value));

            }
        }
    }
    /// <summary>
    /// String�^�̒l��Ή������^�ɕϊ����Ȃ����ĕԋp����N���X�ł�
    /// </summary>
    /// <param name="TypeName">�^�̖��O(.Net�`����)</param>
    /// <param name="Value">Field�̒l</param>
    /// <returns></returns>
    private object StringChange(string TypeName,String Value) {
        //object�^�쐬
        object value;
        //String�^�̒l��Type�̌^�ɕϊ�����
        switch (TypeName)
        {
            case "System.Single":
                value = Convert.ToSingle(Value);
                break;
            default:
                value = null;
                break;
        }
        //�ԋp
        return value;
    }
    private void OnGUI()
    {
        GUILayout.Label($"���ݎ��s���̃m�[�h�̊Ǘ��ԍ�: {activeNodeId}");
        GUILayout.Label($"���s���̃X�N���v�g��: {graphAsset.nodes[activeNodeId].Object}");
    }
}
