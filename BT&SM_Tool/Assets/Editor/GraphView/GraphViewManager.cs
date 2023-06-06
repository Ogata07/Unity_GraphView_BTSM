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
    //SM�p
    public Node sm_StartNode { get; set; } = default;
    //Node�̐F�֌W
    public string defaultColorCode { get;} = "#3F3F3F";
    public string startColorCode { get;} = "#FFA500";
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
            EditorUtility.SetDirty(m_GraphAsset);
            AssetDatabase.SaveAssets();
        }
        else
            Debug.LogError("�Z�[�u�悪����܂���");
        
    }
    public void SaveLog(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
        Debug.Log("�Z�[�u���"+graphAsset.name+"�ɍX�V���܂���");
    }
    /// <summary>
    /// GraohView��ł̓����ɔ�������
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private GraphViewChange OnCallbackGraphView(GraphViewChange callback)
    {
        if (callback.edgesToCreate != null)
        {
            foreach (UnityEditor.Experimental.GraphView.Edge e in callback.edgesToCreate)
            {
                Debug.Log("Edge���쐻����܂���");
            }

        }
        return callback;

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
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        //if (evt.target is GraphView || evt.target is Node || evt.target is Group)
        //{
        //    evt.menu.AppendAction("StateMachine", delegate
        //    {
                
        //        ClickEvent();
        //        //CopySelectionCallback();
        //    }, (DropdownMenuAction a) => canCopySelection ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
        //}
        if (evt.target is Node) {
            Node SelectNode = (Node)evt.target;
            evt.menu.AppendAction(
                "SelectStartNode" ,
                paste=>{ ClickEvent(SelectNode); },
                (paste => (this.canCopySelection ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled)),
                (object)null);
        }
    }
    public void ClickEvent(Node handler)
    {
        Debug.Log(handler);
        if (handler is ScriptNode) {
            if (sm_StartNode != default) {
                sm_StartNode.name = default;
                NodeTitleColorChange(sm_StartNode, defaultColorCode);
            }
            //�X�V
            sm_StartNode = handler;
            handler.name = "Start";
            NodeTitleColorChange(handler, startColorCode);
        }
        //TODO �X�e�[�g�}�V���݂̂��ꂪ�I�����ꂽ�m�[�h����X�^�[�g�����悤�ɂ�����(���Ԃ�������̂Ō�񂵂�������Ȃ�)

    }
    //Node�̃^�C�g�������̐F��ύX���郁�\�b�h
    public void NodeTitleColorChange(Node tagetNode,string changeColorCode) {
        ScriptNode castNode = (ScriptNode)tagetNode;
        castNode.startNodeColorChange(changeColorCode);
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
        //GraphView��̕ω��Ď��R�[���o�b�N
        graphViewChanged = OnCallbackGraphView;
        //�f�[�^����̐���
        GraphViewLoad.CreateGraphView(this);
        //TODO �X�^�[�g�m�[�h�̒ǉ�
        this.AddElement(new StartNode());
        // �w�i����Ԍ��ɒǉ�
        this.Insert(0, new GridBackground());
    }
}
