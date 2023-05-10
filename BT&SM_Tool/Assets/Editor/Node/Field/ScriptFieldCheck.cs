using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using Codice.CM.SEIDInfo;
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
            BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.DeclaredOnly
            );
        Debug.Log(fieldInfos.Length);
        //Node内にすでにあるならリセットする
        NodeReset.extensionContainerReset(scriptNode);
        //各種フィールド値を元にNodeに追加する
        //新規
        if (scriptNode.NodeID == 0) {
            
            foreach (FieldInfo f in fieldInfos) {
                AddVisualElement(f.FieldType);
            }
        }
    }
    private void AddVisualElement(Type fieldInfo)
    {
        Debug.Log(fieldInfo);
    }
}
