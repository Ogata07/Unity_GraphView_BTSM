using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// GraphAsset�̓��e���G�f�B�^�E�B���h�E�ɕ\������
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
    //�f�[�^����̍쐬
    public static void CreateGraphView(GraphViewManager graphViewManager) {
        GraphAsset loadGraphAsset = graphViewManager.m_GraphAsset;

        foreach (var node in loadGraphAsset.nodes) {
            CreateNode(node,graphViewManager);
        }
        foreach (var edge in loadGraphAsset.edges) {
            CreateEdge(edge, graphViewManager);
        }
    }
    private static void CreateNode(NodeData nodeData,GraphViewManager graphViewManager) {
        var node = new TestNode();
        //�m�[�h�̈ʒu
        node.SetPosition(new Rect(nodeData.position, new UnityEngine.Vector2(100,100)));
        //���O�i�\��j
        //�Ǘ��ԍ��i�\��j
        graphViewManager.AddElement(node);
    } 
    private static void CreateEdge(EdgeData edgeData,GraphViewManager graphViewManager) {
        var node = graphViewManager.nodes.ToList();
        Debug.Log("���ݐ�������Ă���m�[�h��"+node.Count+"�ł�");
        //TODO ����͊e�m�[�h�ŊǗ��ԍ��������Ă��Ȃ��̂ł��������Ȃ�܂�
        //��������Port��������������������H
        Port inputPort = node[0].inputContainer.contentContainer.Q<Port>();
        Port outputPort = node[1].outputContainer.contentContainer.Q<Port>();
        var edge = ConnectPorts(inputPort,outputPort);
        //GraphView�ɒǉ�
        graphViewManager.AddElement(edge);
    }
    private static Edge ConnectPorts(Port inputport, Port outputport) {
        //�G�b�W�̍쐬
        var tempEdge = new Edge
        {
            output = outputport,
            input=inputport
        };
        //�m�[�h�ɐڑ�
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        return tempEdge;
    }
}
