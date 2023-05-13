using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using Codice.CM.SEIDInfo;
using System.Web.UI;
using UnityEditor.UIElements;
using System.ComponentModel;
using System.Diagnostics.Contracts;
/// <summary>
/// ScriptNodeにはられたスクリプトのフィールドを呼んでGraphView上でいじれる様にするElementを追加するスクリプト
/// </summary>
public class ScriptFieldCheck : MonoBehaviour
{
    public void Check(UnityEngine.Object _object, ScriptNode scriptNode) { 
    //パブリックフィールドを取得
    MonoScript value=_object as MonoScript;
    Type getType = value.GetClass();
    FieldInfo[] fieldInfos = getType.GetFields(
            //BindingFlags.NonPublic
            BindingFlags.Instance
            | BindingFlags.Public
            //| BindingFlags.DeclaredOnly
            );

        //Node内にすでにあるならリセットする
        NodeReset.extensionContainerReset(scriptNode);
        //各種フィールド値を元にNodeに追加する
        //新規
        if (scriptNode.NodeID == 0) {
            //fieldInfos.GetValue()で値の受け渡しができるかも？
            foreach (FieldInfo f in fieldInfos) {
                AddVisualElement(f.FieldType,scriptNode);
            }
        }
    }
    private void AddVisualElement(Type fieldInfo,ScriptNode scriptNode)
    {
        Debug.Log(fieldInfo.ToString());
        switch (fieldInfo.ToString()) {
            case "Vector2":
                print("WW");
                break;
            case "System.Single"://Float型
                scriptNode.extensionContainer.Add(new DataElement<FloatField, float>());
                scriptNode.RefreshExpandedState();
                break;
            case "double":
                break;
            default: 
                break;

        }
    }
}
