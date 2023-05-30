using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;
/// <summary>
/// GraphAsset�̓��e���G�f�B�^�E�B���h�E�ɕ\������
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
    private static ScriptFieldCheck scriptFieldCheck = new ScriptFieldCheck();
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
        //for (int fieldNumber = 0; fieldNumber < fieldCount; fieldNumber++) { 
        //    //�^���擾
        //    string TypeName = nodeData.fieldData[fieldNumber].typeName;
        //    //���O�̎擾
        //    string fieldName= nodeData.fieldData[fieldNumber].fieldName;
        //    //�l�̎擾
        //    string Value = nodeData.fieldData[fieldNumber].valueData;
        //    switch (TypeName)
        //    {
        //        case "System.Single":
        //            float floatvalue =Convert.ToSingle(Value);
        //            node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, floatvalue));
        //            break;
        //        case "System.Int32":
        //            int intvalue = Convert.ToInt32(Value);
        //            node.extensionContainer.Add(new DataElement<IntegerField, int>(fieldName, intvalue));
        //            break;
        //        case "System.Boolean":
        //            //bool boolvalue = Convert.ToBoolean(Value);
        //            //node.extensionContainer.Add(new DataElement<Toggle, bool>(fieldName, boolvalue));
        //            break;
        //        case "UnityEngine.GameObject":
        //            //GameObject obhectvalue = Convert.(Value);
        //            //node.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, obhectvalue));
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //�X�N���v�g����field�̍쐬�@
        scriptFieldCheck.Check(nodeData.Object, node);
        //
        int fieldElementCount= node.extensionContainer.childCount;
        Debug.Log(fieldElementCount);
        
        //GraphView���field�ƕۑ���̃f�[�^���ׂĈقȂ��Ă�����ۑ���̃f�[�^��}��
        for (int chackCount = 0; chackCount < fieldElementCount; chackCount++) {
            VisualElement child = node.extensionContainer[chackCount];
            //Float�^
            if (child is DataElement<FloatField, float>)
            {
                ////�ϊ�
                //var castfloat = childon as DataElement<FloatField, float>;
                ////���O�̎擾
                //string loadFieldName = castfloat.fieldNameLabel.text;
                ////�ۑ���̃f�[�^���瓯�����O�̃t�B�[���h���Ȃ����T��
                //FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
                //if (nodeData1 != null)
                //{
                //    float floatvalue = Convert.ToSingle(nodeData1.valueData);
                //    castfloat.Field.value = floatvalue;
                //}
                chackSaveData<FloatField, float>(child, nodeData);
            }
            //Int�^
            if (child is DataElement<IntegerField, int>)
            {
                chackSaveData<IntegerField, int>(child, nodeData);
            }
            //Bool�^
            if (child is DataElement<Toggle, bool>)
            {
                chackSaveData<Toggle, bool>(child, nodeData);
            }
            //GameObject�^
            if (child is ObjectElement) {
                ////�ϊ�
                var castfloat = child as DataElement<FloatField, float>;
                ////���O�̎擾
                string loadFieldName = castfloat.fieldNameLabel.text;
                ////�ۑ���̃f�[�^���瓯�����O�̃t�B�[���h���Ȃ����T��
                //FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
            }
        }

        //extensionContainer�ɒǉ�������Y�ꂸ���s���Ȃ��ƉB����Ă��܂�
        node.RefreshExpandedState();
        //��ʂɒǉ�
        graphViewManager.AddElement(node);
    }
    static void chackSaveData<T,V>( VisualElement childon,  NodeData nodeData)
    where T : BaseField<V>, new()
    {

        //�ϊ�
        var castInt = childon as DataElement<T, V>;
        //���O�̎擾
        string loadFieldName = castInt.fieldNameLabel.text;
        //�ۑ���̃f�[�^���瓯�����O�̃t�B�[���h���Ȃ����T��
        FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
        if (nodeData1 != null)
        {
            //dynamic
            if (typeof(V) == typeof(float)) 
                castInt.Field.value = (V)(object)Convert.ToSingle(nodeData1.valueData);
            if (typeof(V) == typeof(int))
                castInt.Field.value = (V)(object)Convert.ToInt32(nodeData1.valueData);
            if (typeof(V) == typeof(bool))
                castInt.Field.value = (V)(object)Convert.ToBoolean(nodeData1.valueData);
            //if(typeof(V) == typeof(GameObject))
        }
    }
    static TResult ConvertViaDynamic<T, TResult>(T number)
    {
        return (TResult)(dynamic)number;
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
