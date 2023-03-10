using System;
using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// スクリプト参照ノード
/// </summary>
public class ScriptNode : Node
{
    private ObjectField m_ObjectField = default;
    public Port OutputPort { get; set; }
    public ObjectField ObjectField
    {
        get
        {
            return m_ObjectField;
        }
        set
        {
            //if (m_ObjectField.value==value.value)
            //m_ObjectField.objectType = typeof(UnityEngine.Object);
            m_ObjectField.value = value.value;
        }
    }
    public int NodeID { get; set; } = 0;

    public ScriptNode():base(){
        title = "ScriptNode";
        //接続Port追加
        PortAdd();
        //ObjectFieldの追加
        m_ObjectField = new ObjectField();
        //TODO MonoScriptから変更
        m_ObjectField.objectType = typeof(UnityEngine.Object);
        mainContainer.Add(m_ObjectField);
        m_ObjectField.RegisterCallback<ChangeEvent<String>>(events =>{
            TitleChange();
        });
    }
    private void PortAdd() {
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
        Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Single, typeof(Port));
        outputContainer.Add(OutputPort);
    }
    private void TitleChange() {
        Debug.Log("値が変更されました");
        if(m_ObjectField.value!=null)
            title = m_ObjectField.value.name;
    }
}
