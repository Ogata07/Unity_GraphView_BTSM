using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using ScriptFlow;
using Codice.Client.BaseCommands;
/// <summary>
/// GraohView���node��I�����ɉE�N���b�N�������ɏo��E�B���h�E�̊Ǘ�
/// </summary>

public class SelectionDraggerWindow : GraphView
{
    //TODO �m�[�h�E�N���b�N���ɂł�E�B���h�E�ɍ��ڂ�ǉ�
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        if (evt.target is UnityEditor.Experimental.GraphView.GraphView || evt.target is Node || evt.target is Group) {
            ClickEvent();
        }
    }
    public void ClickEvent() {
        Debug.Log("aa");
    }
}
