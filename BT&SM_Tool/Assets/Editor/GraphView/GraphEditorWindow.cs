using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

/// <summary>
/// �G�f�B�^�[�E�B���h�E�̕\��
/// </summary>
public class GraphEditorWindow : EditorWindow
{
    private GraphAsset m_GraphAsset;
    private GraphViewManager m_GraphViewManager;
    private ObjectField m_SaveField;
    public ObjectField SaveField {
        get { return m_SaveField; }
        set { m_SaveField = value; }
    }
    GraphAsset m_SaveGraphAsset;
    public VisualElement a = default;
    //���j���[�o�[����I�����p
    [MenuItem("Tool/GraphEditorWindow")]
    public static void ShowWindow() {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();
        graphEditorWindow.Intialize();

    }
    //scriptableobject��I�����p
    public static void ShowWindow(GraphAsset graphAsset) {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.m_GraphAsset= graphAsset;
        graphEditorWindow.Show();
        graphEditorWindow.Intialize();
        //graphEditorWindow.CreateGraohView();
        graphEditorWindow.m_SaveField.value = graphAsset;
    }
    private void OnEnable()
    {
        //TODO �v����
        //Intialize();
        //TODO�R���p�C�����ʂŕ\�����s���Ă���̂ŉ����o�Ȃ��Ȃ�
        //�\���Ƃ��̑��ŕ�����ׂ���

    }
    /// <summary>
    /// ��������
    /// </summary>
    public void Intialize(){//GraphAsset graphAsset
        //m_GraphAsset = graphAsset;
        //GraphView�E�B���h�E�̍쐻
        m_GraphViewManager = new GraphViewManager(this, m_GraphAsset);
        m_GraphViewManager.style.flexGrow = 1;
        VisualElement visualElement = this.rootVisualElement;

        //��ʏ㕔�̃c�[���o�[
        var toolbar = new Toolbar();
        visualElement.Add(toolbar);
        var btn1 = new ToolbarButton(m_GraphViewManager.SaveStart) { text = "Save" };
        toolbar.Add(btn1);
        if (m_SaveField == null)
        {
            m_SaveField = new UnityEditor.UIElements.ObjectField("�ۑ���") { };//value = m_SaveGraphAsset
            m_SaveField.objectType = typeof(GraphAsset);
        }
        //�R�[���o�b�N
        m_SaveField.RegisterCallback<ChangeEvent<string>>(events => {
            m_GraphViewManager.SaveLog(m_SaveField.value as GraphAsset);
        });

        toolbar.Add(m_SaveField);

        visualElement.Add(m_GraphViewManager);
        rootVisualElement.Add(toolbar);

    }
}
