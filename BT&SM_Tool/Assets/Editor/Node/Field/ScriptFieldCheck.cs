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

                AddVisualElement(f,scriptNode, getType);
            }
        }
    }
    /// <summary>
    /// 新規追加時に使うFieldの追加を行う部分
    ///</summary>
    /// <param name="fieldInfo"></param>
    /// <param name="scriptNode"></param>
    /// <param name="getType"></param>
    private void AddVisualElement(FieldInfo fieldInfo,ScriptNode scriptNode,Type getType)
    {
        switch (fieldInfo.FieldType.ToString()) {
            case "Vector2":
                print("WW");
                break;
            case "System.Single"://Float型
                //Fieldの名前を取得
                String FieldName=fieldInfo.Name;
                //インスタンス生成
                var activeScript = Activator.CreateInstance(getType);
                Debug.Log("名前＝"+fieldInfo.ToString() + "値" + fieldInfo.GetValue(activeScript));
                float Value = (float)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<FloatField, float>(Value));
                scriptNode.RefreshExpandedState();
                break;
            case "double":
                break;
            default: 
                break;

        }
    }
}
