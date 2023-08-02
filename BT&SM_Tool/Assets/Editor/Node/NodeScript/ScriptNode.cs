using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// スクリプトを格納してGraphViewに表示するクラス
/// </summary>
public class ScriptNode : BaseNode
{
    private ObjectField objectField = default;
    private readonly ScriptFieldCheck scriptFieldCheck= new ScriptFieldCheck();
    public NodeType nodeType = NodeType.BT_Action;
    //public Port OutputPort { get; set; }
    //public int NodeID { get; set; } = default;
    public ObjectField ObjectField
    {
        get
        {
            return objectField;
        }
        set
        {
            //if (m_ObjectField.value==value.value)
            //m_ObjectField.objectType = typeof(UnityEngine.Object);
            objectField.value = value.value;
        }
    }

    public ScriptNode():base(){
        title = "ScriptNode";
        //接続Port追加
        PortAdd();
        //ObjectFieldの追加
        objectField = new ObjectField();
        //TODO MonoScriptから変更
        objectField.objectType = typeof(UnityEngine.Object);
        mainContainer.Add(objectField);
        //m_ObjectFieldの値が変更されたときに行う処理
        objectField.RegisterCallback<ChangeEvent<String>>(events =>{
            AddStart();
        });
    }
    /*
    private void PortAdd() {
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
        Port.Capacity.Multi, typeof(Port));
        inputContainer.Add(inputPort);
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Multi, typeof(Port));
        outputContainer.Add(OutputPort);
    }
    */
    /// <summary>
    /// NodeSearchWindowから生成されたときに値が変更されたときと同じ処理をさせる
    /// </summary>
    public void AddStart() {
        TitleChange();
        scriptFieldCheck.Check(ObjectField.value, this);
    }
    private void TitleChange() {
        Debug.Log("値が変更されました");
        if(objectField.value!=null)
            title = objectField.value.name;
    }
    /// <summary>
    /// スタートノードのみ色を変更してわかりやすくする
    /// </summary>
    public void StartNodeColorChange(String colorCode) {
        //TODO 設定として別のところにまとめておく
        Color setColor=ColorConversion.GetColor(colorCode);
        titleContainer.style.backgroundColor = setColor; //new Color(255, 165, 0);
    }
}
