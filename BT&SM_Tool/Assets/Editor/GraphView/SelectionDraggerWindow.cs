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
/// GraohView上でnodeを選択時に右クリックした時に出るウィンドウの管理
/// </summary>

public class SelectionDraggerWindow : GraphView
{
    //TODO ノード右クリック時にでるウィンドウに項目を追加
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
