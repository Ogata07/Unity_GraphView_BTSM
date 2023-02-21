using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
///�@�G�f�B�^�E�B���h�E�̓��e��GraphAsset�ɕۑ�����
/// </summary>
public static class GraphViewSave
{
    public static void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("�Z�[�u�̊J�n");
        //�m�[�h
        Debug.Log("�m�[�h�̐���"+m_GraphView.nodes.ToList().Count+"��");
        //�G�b�W
        Debug.Log("�G�b�W�̐���" + m_GraphView.edges.ToList().Count + "��");
        ControlNumberAdd(m_GraphView);
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset,m_GraphView);
    }
    //TODO �Z�[�u���ɊǗ��ԍ���t�^���Ă��邪���Ŏg�����Ƃ͂Ȃ��̂��H
    //�e�m�[�h�ɊǗ��ԍ���t�^
    private static void ControlNumberAdd(GraphView m_GraphView) {
        var NodeList=m_GraphView.nodes.ToList();
        int Number = 0;
        foreach (var node in NodeList) {
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                castScriptNode.NodeID = Number;
                
            }
            Number++;
        }
    }
    //�m�[�h�̕ۑ�
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //�E�B���h�E��̃m�[�h�̃��X�g
        var fieldNodeList = m_GraphView.nodes.ToList();
        //���X�g�̏�����
        m_GraphAsset.nodes = new List<NodeData>();
        foreach (var node in fieldNodeList)
        {
            //�ꏊ�̒ǉ�
            m_GraphAsset.nodes.Add(new NodeData());
            int listNumber = m_GraphAsset.nodes.ToList().Count - 1;

            //�ʒu�̕ۑ�
            m_GraphAsset.nodes[listNumber].position = node.GetPosition().position;


            //�X�N���v�g�̕ۑ�
            if (node is ScriptNode) {
                ScriptNode castScriptNode = node as ScriptNode;
                m_GraphAsset.nodes[listNumber].Object = castScriptNode.ObjectField.value;
            }

            //�Ǘ��ԍ��̕ۑ�
            if (node is ScriptNode)
            {
                ScriptNode castScriptNode = node as ScriptNode;
                m_GraphAsset.nodes[listNumber].controlNumber = castScriptNode.NodeID;
            }
        }
    }
    //�G�b�W�̕ۑ�
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //�E�B���h�E��̃G�b�W�̃��X�g
        var fieldEdgslist = m_GraphView.edges.ToList();
        Debug.Log(fieldEdgslist.Count());
        //���X�g�̏�����
        m_GraphAsset.edges = new List<EdgeData>();
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