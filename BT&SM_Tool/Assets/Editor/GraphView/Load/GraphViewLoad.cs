using System;
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
    /// <summary>
    /// �m�[�h�̐���
    /// </summary>
    /// <param name="nodeData"></param>
    /// <param name="graphViewManager"></param>
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
        //field�G�������g�ǉ�
        //field�G�������g�ǉ�
        //�ǉ����鐔���W�v
        int fieldCount=nodeData.fieldData.Count;
        //�񐔕���������
        for (int fieldNumber = 0; fieldNumber < fieldCount; fieldNumber++) { 
            //�^���擾
            string TypeName = nodeData.fieldData[fieldNumber].typeName;
            //���O�̎擾
            string fieldName= nodeData.fieldData[fieldNumber].fieldName;
            //�l�̎擾
            string Value = nodeData.fieldData[fieldNumber].valueData;
            switch (TypeName)
            {
                case "System.Single":
                    float floatvalue =Convert.ToSingle(Value);
                    node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, floatvalue));
                    break;
                case "System.Int32":
                    int intvalue = Convert.ToInt32(Value);
                    node.extensionContainer.Add(new DataElement<IntegerField, int>(fieldName, intvalue));
                    break;
                case "System.Boolean":
                    //bool boolvalue = Convert.ToBoolean(Value);
                    //node.extensionContainer.Add(new DataElement<Toggle, bool>(fieldName, boolvalue));
                    break;
                case "UnityEngine.GameObject":
                    //GameObject obhectvalue = Convert.(Value);
                    //node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, obhectvalue));
                    break;
                default:
                    break;
            }
        }
        //extensionContainer�ɒǉ�������Y�ꂸ���s���Ȃ��ƉB����Ă��܂�
        node.RefreshExpandedState();
        //��ʂɒǉ�
        graphViewManager.AddElement(node);
    } 
    /// <summary>
    /// �G�b�W�̐���
    /// </summary>
    /// <param name="edgeData"></param>
    /// <param name="graphViewManager"></param>
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
