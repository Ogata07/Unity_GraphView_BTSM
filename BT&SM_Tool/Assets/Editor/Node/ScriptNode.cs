using System;
using System.Reflection.Emit;
using System.Web;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// �X�N���v�g�Q�ƃm�[�h
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
        //�ڑ�Port�ǉ�
        PortAdd();
        //ObjectField�̒ǉ�
        m_ObjectField = new ObjectField();
        //TODO MonoScript����ύX
        m_ObjectField.objectType = typeof(UnityEngine.Object);
        mainContainer.Add(m_ObjectField);
        //m_ObjectField�̒l���ύX���ꂽ�Ƃ��ɍs������
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
    /// NodeSearchWindow���琶�����ꂽ�Ƃ��ɒl���ύX���ꂽ�Ƃ��Ɠ���������������
    /// </summary>
    public void AddStart() {
        TitleChange();
        scriptFieldCheck.Check(ObjectField.value, this);
    }
    private void TitleChange() {
        Debug.Log("�l���ύX����܂���");
        if(m_ObjectField.value!=null)
            title = m_ObjectField.value.name;
    }
    /// <summary>
    /// �X�^�[�g�m�[�h�̂ݐF��ύX���Ă킩��₷������
    /// </summary>
    public void startNodeColorChange(String ColorCode) {
        //TODO �ݒ�Ƃ��ĕʂ̂Ƃ���ɂ܂Ƃ߂Ă���
        Color setColor=ColorConversion.GetColor(ColorCode);
        titleContainer.style.backgroundColor = setColor; //new Color(255, 165, 0);
    }
}
