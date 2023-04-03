using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
        var node = new ScriptNode();
        //�m�[�h�̈ʒu
        node.SetPosition(new Rect(nodeData.position, new UnityEngine.Vector2(100,100)));
        //���O�i�\��j
        //�X�N���v�g
        if (nodeData.Object != null)
            node.ObjectField.value = nodeData.Object;
        //�Ǘ��ԍ�
        node.NodeID = nodeData.controlNumber;
        //��ʂɒǉ�
        graphViewManager.AddElement(node);
    } 
    private static void CreateEdge(EdgeData edgeData,GraphViewManager graphViewManager) {
        var node = graphViewManager.nodes.ToList();
        Debug.Log("���ݐ�������Ă���m�[�h��"+node.Count+"�ł�");
        //TODO�@��������Port��������������������H
        //Port�쐻
        Port inputPort = node[edgeData.inputNodeId].inputContainer.contentContainer.Q<Port>();
        Port outputPort = node[edgeData.outputNodeId].outputContainer.contentContainer.Q<Port>();
        //Edge�쐻
        var edge = ConnectPorts(inputPort,outputPort);
        //���x���ǉ�
        UnityEngine.UIElements.Label edgeLabel = new UnityEngine.UIElements.Label();
        edgeLabel.text = "0";
        edgeLabel.style.fontSize= 64;
        edgeLabel.style.marginTop = -32;
        edge.edgeControl.Add(edgeLabel);
        //Label
        //edge.Add(btn1);

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
