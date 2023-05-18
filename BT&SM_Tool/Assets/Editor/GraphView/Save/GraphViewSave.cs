using JetBrains.Annotations;
using PlasticGui.WorkspaceWindow.Replication;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
/// <summary>
///�@�G�f�B�^�E�B���h�E�̓��e��GraphAsset�ɕۑ�����
/// </summary>
public static class GraphViewSave
{
    //TODO ���O��Ԃł������H
    private static int Number = 0;
    public static void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("�Z�[�u�̊J�n");
        //�m�[�h
        Debug.Log("�m�[�h�̐���"+m_GraphView.nodes.ToList().Count+"��");
        //�G�b�W
        Debug.Log("�G�b�W�̐���" + m_GraphView.edges.ToList().Count + "��");
        Number = 0;
        ControlNumberAdd(m_GraphView);
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset,m_GraphView);

    }
    //�e�m�[�h�ɊǗ��ԍ���t�^
    private static void ControlNumberAdd(GraphView m_GraphView) {
        //int Number = 0;
        var NodeList = m_GraphView.nodes.ToList();
        //���Ԃ����������Ȃ�̂ŕύX
        /*
        foreach (var node in NodeList) {
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                castScriptNode.NodeID = Number;
                
            }
            Number++;
        }
        */
        //(�X�e�[�g�}�V������)(����̃m�[�h��r�����Ă���ȊO�����ԂɊǗ��ԍ���t�^����)
        //�X�^�[�g�m�[�h�ɔԍ���U��(0��)
        var StartNode = NodeList.Find(x => x.title == "StartNode") as StartNode;
        NodeList.Remove(StartNode);
        foreach(Node node in NodeList)
        {
            AddNumbar(node);
        }
        //(�X�e�[�g�}�V������)(���ԂɐU��͉̂������邩��)
        /*
        //�X�^�[�g�m�[�h�ɔԍ���U��(0��)
        var StartNode = NodeList.Find(x => x.title == "StartNode") as StartNode;
        //�Ȃ����Ă���m�[�h���擾(0��)
        var NextNode = StartNode.OutputPort.connections.FirstOrDefault().input.node;
        AddNumbar(NextNode);
        var CastNextNode = NextNode as ScriptNode;
        ChacksNode(CastNextNode);
        /*
        //�q�����Ă���m�[�h�̐��𐔂���
        var NextNodeCount = CastNextNode.OutputPort.connections.Count();
        var NexrNodeList = CastNextNode.OutputPort.connections.ToList();
        //var NextNodeCount = StartNode.OutputPort.connections.Count();
        //var NexrNodeList = StartNode.OutputPort.connections.ToList();

        Debug.Log("aa"+NexrNodeList.Count);
        //�����ɑΉ����Ă���m�[�h�ԍ��t�^�����
        //��2�̐ڑ��m�[�h
        
        for (int actionCount = 0; actionCount < NextNodeCount; actionCount++) {
            //�Ȃ����Ă���m�[�h�ɔԍ���t�^����
            AddNumbar(NexrNodeList[actionCount].input.node,Number);
            Number++;
        }
        */
        //�e�m�[�h�Ɍq�����Ă���m�[�h��T��
        //�O�ɖ߂��Č��������m�[�h�ɔԍ��t�^���s��
        //�J��Ԃ��s��(�ԍ��t�^�����m�[�h�̐��𐔂��Ă���Ŕ��肵�Ă���������)

        //(�r�w�C�r�A�c���[����)
        //�X�^�[�g�m�[�h���珇�Ԃ�T�����ĊǗ��ԍ���t�^����

        //�X�^�[�g�m�[�h���擾
        //var StartNode = NodeList.Find(x=>x.title == "StartNode") as StartNode;
        //�Ȃ����Ă���m�[�h���擾(0��)
        //var NextNode = StartNode.OutputPort.connections.FirstOrDefault().input.node;

        /*
        //�m�[�h���q�����Ă��邩�H
        if (NextNode != null) {
            AddNumbar(NextNode,Number);
            Debug.Log("���݂̊Ǘ��ԍ���" + Number + "�ł�");
            //���Ɍq�����Ă���m�F����
            NextNode=ChackNode(NextNode);
            while(NextNode!=null)
            {
                AddNumbar(NextNode, Number);
                Debug.Log("���݂̊Ǘ��ԍ���" + Number + "�ł�");

                //���Ɍq�����Ă���m�F����
                NextNode = ChackNode(NextNode);
            }
            //TODO ���݂͕���m�[�h������Ă��Ȃ��̂ŕ���ɂ͖��Ή�
        }
        */
        
    }
    //�Ǘ��ԍ���t�^����
    private static void AddNumbar(Node node) {
        //�Ǘ��ԍ���t�^����
        //�X�N���v�g�m�[�h�����ԍ���U��Ȃ�
        //TODO �Z�[�u������ɒǉ�����Ƃ��܂��������Ȃ�
        if (node is ScriptNode){
            ScriptNode castScriptNode = node as ScriptNode;

            if (castScriptNode.NodeID== 0) {
                Debug.Log("ssss"+castScriptNode.NodeID);
                castScriptNode.NodeID = Number;
                Number++;

            }
        }
        else
            Debug.LogError("�ԍ���U��̂ɑΉ����Ă��܂���");
    }
    //���̃m�[�h����������
    private static Node ChackNode(Node node) {
        //���łɔԍ����t�^����Ă���m�[�h�ɍ~��Ȃ��悤�ɂ��Ȃ��Ƃ����Ȃ�
        //�q�����Ă���m�[�h���擾
        if (node is ScriptNode) {
            ScriptNode castScriptNode = node as ScriptNode;
            if (castScriptNode.OutputPort.connections.FirstOrDefault() != null) {
                Node inputNode = castScriptNode.OutputPort.connections.FirstOrDefault().input.node;
                return inputNode;
            }
        }
        return null;
    }

    /// <summary>
    /// �Ǘ��ԍ����U���Ă��Ȃ��m�[�h���Ȃ��Ȃ�܂ŌJ��Ԃ�
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static Node ChacksNode(Node node) {
        //���[�v���Ă���m�[�h����낤�Ƃ���Ƃ����������Ă��܂�
        var castScriptNode = node as ScriptNode;
        //�q�����Ă���m�[�h�̐��𐔂���
        var NextNodeCount = castScriptNode.OutputPort.connections.Count();
        var NexrNodeList = castScriptNode.OutputPort.connections.ToList();
        //�����ɑΉ����Ă���m�[�h�ԍ��t�^�����
        if (NextNodeCount != 0)
        {
            for (int actionCount = 0; actionCount < NextNodeCount; actionCount++)
            {
                //�Ȃ����Ă���m�[�h�ɔԍ���t�^����
                AddNumbar(NexrNodeList[actionCount].input.node);
                ChacksNode(NexrNodeList[actionCount].input.node);
            }
        }
        return null;
    }
    /// <summary>
    /// �X�^�[�g�m�[�h���̂��������ׂẴm�[�h�ɊǗ��ԍ���t�^����
    /// </summary>
    private static void ListAddNumbar() { 
    
    }
    //�m�[�h�̕ۑ�
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //�E�B���h�E��̃m�[�h�̃��X�g
        var fieldNodeList = m_GraphView.nodes.ToList();
        //���O�Ώۂ�r��
        fieldNodeList.RemoveAll(node => node is StartNode);
       
        //���X�g�̏�����
        m_GraphAsset.nodes = new List<NodeData>();

        foreach (var node in fieldNodeList)
        {
            //�ꏊ�̒ǉ�
            m_GraphAsset.nodes.Add(new NodeData());
            int listNumber = m_GraphAsset.nodes.ToList().Count - 1;

            //�ʒu�̕ۑ�
            m_GraphAsset.nodes[listNumber].position = node.GetPosition().position;
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                //�X�N���v�g�̕ۑ�
                m_GraphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
                //�Ǘ��ԍ��̕ۑ�
                m_GraphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;

                //�Ώۃm�[�h�̃A�E�g�v�b�g�ɂȂ����Ă��邷�ׂẴG�b�W��ۑ�
                var edgeslist=castScriptNode.OutputPort.connections.ToList();
                if (edgeslist.Count >= 0) { 
                    for (int listCount= 0; listCount < edgeslist.Count; listCount++) {
                        //�ۑ��ꏊ�̒ǉ�
                        m_GraphAsset.nodes[listNumber].edgesDatas.Add(new EdgesData());
                        ScriptNode castInputNode= edgeslist[listCount].input.node as ScriptNode;
                        //�Ǘ��ԍ��̕ۑ�
                        m_GraphAsset.nodes[listNumber].edgesDatas[listCount].controlNumber = listCount;
                        //�C���v�b�g�ԍ��̕ۑ�(�A�E�g�v�b�g�ԍ��͂��̃m�[�h�̊Ǘ��ԍ��Ȃ̂ŕۑ����Ȃ��Ă悢)
                        m_GraphAsset.nodes[listNumber].edgesDatas[listCount].inputNodeId = castInputNode.NodeID;
                    }
                }
                //�Ώۃm�[�h��extensionContainer�ɂȂ����Ă���Field��ۑ�
                int fieldCount = castScriptNode.extensionContainer.childCount;
                if (fieldCount >= 0) {
                    for (int i = 0; i < fieldCount; i++) {
                        //�ۑ��ꏊ�̒ǉ�
                        m_GraphAsset.nodes[listNumber].fieldData.Add(new FieldData());
                        //TODO�@���݂͂ǂ����String�ł̕ۑ��ق��̕��@��������΂���ɕύX
                        var fieldElement = castScriptNode.extensionContainer[i];
                        var castFieldElement = fieldElement as DataElement<FloatField,float>;
                        //�^���̕ۑ�
                        m_GraphAsset.nodes[listNumber].fieldData[i].TypeName = "float";
                        //�l�̕ۑ�
                        m_GraphAsset.nodes[listNumber].fieldData[i].ValueData = castFieldElement.Field.value.ToString();
                    }
                }


            }

        }
        //�Ǘ��ԍ��̃\�[�g
        m_GraphAsset.nodes.Sort((node1,node2)=>node1.controlNumber-node2.controlNumber);
    }
    //�G�b�W�̕ۑ�
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //�E�B���h�E��̃G�b�W�̃��X�g
        var fieldEdgslist = m_GraphView.edges.ToList();
        Debug.Log(fieldEdgslist.Count());
        //���X�g�̏�����
        m_GraphAsset.edges = new List<EdgeData>();
        //���O�Ώۂ�r��
        fieldEdgslist.RemoveAll(i => i.output.node is StartNode);
        //�e�X�g�p�Ɋȑf��
        foreach (var edge in fieldEdgslist.Select((v, i) => new { value = v, Index = i }))
        {
            //�ꏊ�̒ǉ�
            m_GraphAsset.edges.Add(new EdgeData());
            if (edge.value.input.node is ScriptNode){
                ScriptNode castScriptNode= edge.value.input.node as ScriptNode;
                m_GraphAsset.edges[edge.Index].inputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("input���ł̐ڑ��悪�ۑ��ł��܂���ł����B�Ǘ��ԍ����U���Ă��Ȃ��\��������܂�");
            if (edge.value.output.node is ScriptNode){
                ScriptNode castScriptNode = edge.value.output.node as ScriptNode;
                m_GraphAsset.edges[edge.Index].outputNodeId = castScriptNode.NodeID;
            }
            else
                Debug.LogError("output���ł̐ڑ��悪�ۑ��ł��܂���ł����B�Ǘ��ԍ����U���Ă��Ȃ��\��������܂�");

        }
    }
    private static Type ListReset<Type>(Type ListData)
    where Type:IComparer{
        return default;
    }
}
