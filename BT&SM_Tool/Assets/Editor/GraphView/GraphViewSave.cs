using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
///�@�G�f�B�^�E�B���h�E�̓��e��GraphAsset�ɕۑ�����
/// </summary>
public class GraphViewSave
{
    public void SaveNodeElement(GraphAsset m_GraphAsset, GraphView m_GraphView) 
    {
        Debug.Log("�Z�[�u�̊J�n");
        //�m�[�h
        Debug.Log("�m�[�h�̐���"+m_GraphView.nodes.ToList().Count+"��");
        //�G�b�W
        Debug.Log("�G�b�W�̐���" + m_GraphView.edges.ToList().Count + "��");
        SaveNode(m_GraphView);
    }
    private void SaveNode(GraphView m_GraphView) {
        //�E�B���h�E��̃m�[�h�̃��X�g
        var fieldNodelist = m_GraphView.nodes.ToList();

    }
    private void SaveEdgs() { 

    }
}
