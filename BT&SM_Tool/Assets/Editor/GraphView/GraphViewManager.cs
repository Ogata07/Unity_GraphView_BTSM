using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
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
    }
}
