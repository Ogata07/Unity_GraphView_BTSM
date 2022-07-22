using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;

public class GraphViewManager : GraphView
{
    public GraphViewManager() : base()
    {
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

        // �w�i����Ԍ��ɒǉ�
        this.Insert(0, new GridBackground());
        this.AddElement(new TestNode());
        this.AddElement(new TestNode());
    }

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
}
