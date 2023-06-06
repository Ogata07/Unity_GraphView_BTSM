using System;
using System.Reflection.Emit;
using System.Web;
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
    private readonly ScriptFieldCheck scriptFieldCheck= new ScriptFieldCheck();
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
    public int NodeID { get; set; } = default;

    public ScriptNode():base(){
        title = "ScriptNode";
        //接続Port追加
        PortAdd();
        //ObjectFieldの追加
        m_ObjectField = new ObjectField();
        //TODO MonoScriptから変更
        m_ObjectField.objectType = typeof(UnityEngine.Object);
        mainContainer.Add(m_ObjectField);
        //m_ObjectFieldの値が変更されたときに行う処理
        m_ObjectField.RegisterCallback<ChangeEvent<String>>(events =>{
            AddStart();
        });
    }
    private void PortAdd() {
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
        Port.Capacity.Multi, typeof(Port));
        inputContainer.Add(inputPort);
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Multi, typeof(Port));
        outputContainer.Add(OutputPort);
    }

    /// <summary>
    /// NodeSearchWindowから生成されたときに値が変更されたときと同じ処理をさせる
    /// </summary>
    public void AddStart() {
        TitleChange();
        scriptFieldCheck.Check(ObjectField.value, this);
    }
    private void TitleChange() {
        Debug.Log("値が変更されました");
        if(m_ObjectField.value!=null)
            title = m_ObjectField.value.name;
    }
    /// <summary>
    /// スタートノードのみ色を変更してわかりやすくする
    /// </summary>
    public void startNodeColorChange(String ColorCode) {
        //TODO 設定として別のところにまとめておく
        Color setColor=ColorConversion.GetColor(ColorCode);
        titleContainer.style.backgroundColor = setColor; //new Color(255, 165, 0);
    }
}
