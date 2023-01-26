using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEditor;
using System.Runtime.Remoting.Contexts;
/// <summary>
/// �Z�[�u�⃍�[�h�ɕK�v�ȋ@�\�̊Ǘ���
/// </summary>
public class GraphViewManager : GraphView
{
    public GraphAsset m_GraphAsset;

    public GraphViewManager() : base()
    {
        //TODO �����쐬���ł��Ȃ��Ȃ�
        //setInitial();
    }

    public GraphViewManager(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        m_GraphAsset= graphAsset;
        setInitial(editorWindow); 
    }
    //TODO�@�ۑ��@�\�̕������s���\������
    public void SaveStart() {
        if (m_GraphAsset != null)
        {
            Debug.Log("<color=green>�Z�[�u�����܂���</color>");
            GraphViewSave.SaveNodeElement(m_GraphAsset, this);
        }
        else
            Debug.LogError("�Z�[�u�悪����܂���");
        
    }
    public void SaveLog(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
        Debug.Log("�Z�[�u���"+graphAsset.name+"�ɍX�V���܂���");
    }
    //GraphView��̃��[��
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
        var compatiblePorts = new List<Port>();
        compatiblePorts.AddRange(ports.ToList().Where(Port =>
        {
            //�����m�[�h�ɂ͂Ȃ��Ȃ�  
            if (startAnchor.node == Port.node)
                return false;
            //Int���m�AOut���m�ł͂Ȃ��Ȃ�
            if (Port.direction == startAnchor.direction)
                return false;
            //�|�[�g�̌^����v���Ă��Ȃ��ꍇ�͂Ȃ��Ȃ�
            if (Port.portType != startAnchor.portType)
                return false;

            return true;
        }));
        return compatiblePorts;
    }
    public  void setInitial(EditorWindow editorWindow) {
        //�e�̃T�C�Y�ɍ��킹�ăT�C�Y�ύX
        this.StretchToParentSize();
        //�g��k��
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        //�h���b�O�ŕ`��͈͂��ړ�
        this.AddManipulator(new ContentDragger());
        //�h���b�O�őI�������v�f���ړ�
        this.AddManipulator(new SelectionDragger());
        //�h���b�O�Ŕ͈͑I��
        this.AddManipulator(new RectangleSelector());
        // uss�t�@�C����ǂݍ���ŃX�^�C���ɒǉ�
        this.styleSheets.Add(Resources.Load<StyleSheet>("GraphViewBackGround"));
        //�m�[�h�ǉ��p�̃E�B���h�E�\��
        var nodeSearcWindow = new NodeSearchWindow();
        nodeSearcWindow.Initialize(this, editorWindow);
        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), nodeSearcWindow);
        };

        //�f�[�^����̐���
        GraphViewLoad.CreateGraphView(this);
        // �w�i����Ԍ��ɒǉ�
        this.Insert(0, new GridBackground());
    }
}
