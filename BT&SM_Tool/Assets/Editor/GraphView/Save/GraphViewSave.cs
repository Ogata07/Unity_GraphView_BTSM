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
        SaveNode(m_GraphAsset,m_GraphView);
        SaveEdgs(m_GraphAsset, m_GraphView);
    }
    //�m�[�h�̕ۑ�
    private static void SaveNode(GraphAsset m_GraphAsset,GraphView m_GraphView) {
        //�E�B���h�E��̃m�[�h�̃��X�g
        var fieldNodelist = m_GraphView.nodes.ToList();
        //���X�g�̏�����
        m_GraphAsset.nodes = new List<NodeData>();
        foreach (var node in fieldNodelist)
        {
            //�ꏊ�̒ǉ�
            m_GraphAsset.nodes.Add(new NodeData());
            int listNumber = m_GraphAsset.nodes.ToList().Count - 1;

            //�ʒu�̕ۑ�
            m_GraphAsset.nodes[listNumber].position = node.GetPosition().position;

            //�X�N���v�g�̕ۑ�
            if (node is DebugNode)
            {
               // m_GraphAsset.nodes[listNumber].scriptObject = new DebugNode();
                //m_GraphAsset.nodes[listNumber].GetObject
            }
            if (node is RealTimeNode) {
                //m_GraphAsset.nodes[listNumber].scriptObject = new RealTimeNode();
            }

            //�Ǘ��ԍ��̕ۑ�
            //m_GraphAsset.nodes[listNumber].controlNumber = node.GetPosition().;


        }
    }
    //�G�b�W�̕ۑ�
    private static void SaveEdgs(GraphAsset m_GraphAsset, GraphView m_GraphView) {
        //�E�B���h�E��̃G�b�W�̃��X�g
        var fieldEdgslist = m_GraphView.edges.ToList();
        //���X�g�̏�����
        m_GraphAsset.edges = new List<EdgeData>();
        //�e�X�g�p�Ɋȑf��
        foreach (var edge in fieldEdgslist.Select((v, i) => new { value = v, Index = i }))
        {
            //�ꏊ�̒ǉ�
            m_GraphAsset.edges.Add(new EdgeData());
            //�m�[�h�̐����ԍ����擾
            var a = edge.value.output.node as TestNode;
            //�G�b�W�̃C���m�[�h�������
            m_GraphAsset.edges[edge.Index].inputNodeId = a.NodeID;
            //�G�b�W�̃A�E�g�m�[�h�������
            m_GraphAsset.edges[edge.Index].outputNodeId = a.NodeID;

        }
    }
    private static Type ListReset<Type>(Type ListData)
    where Type:IComparer{
        return default;
    }
}
