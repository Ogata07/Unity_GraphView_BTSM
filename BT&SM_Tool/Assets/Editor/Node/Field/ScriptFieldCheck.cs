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
            | BindingFlags.DeclaredOnly
            );

        //Node内にすでにあるならリセットする
        NodeReset.extensionContainerReset(scriptNode);
        //各種フィールド値を元にNodeに追加する
        //新規
        //if (scriptNode.NodeID == 0) {
        //    //fieldInfos.GetValue()で値の受け渡しができるかも？

        //}
        foreach (FieldInfo f in fieldInfos)
        {
            AddVisualElement(f, scriptNode, getType);
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
        Debug.Log(fieldInfo.FieldType.ToString());
        //Fieldの名前を取得
        String FieldName = fieldInfo.Name;
        //インスタンス生成
        var activeScript = Activator.CreateInstance(getType);
        switch (fieldInfo.FieldType.ToString()) {
            case "System.Int32"://int型
                int intValue = (int)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<IntegerField, int>(FieldName, intValue));
                break;
            case "System.Single"://Float型
                ////Fieldの名前を取得
                //String FieldName = fieldInfo.Name;
                ////インスタンス生成
                //var activeScript = Activator.CreateInstance(getType);
                float floatValue = (float)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<FloatField, float>(FieldName, floatValue));
                break;
            case "System.Boolean"://Bool型
                //int intValue = (int)fieldInfo.GetValue(activeScript);
                bool boolValue = (bool)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<Toggle, bool>(FieldName, boolValue));
                break;
            case "UnityEngine.GameObject"://GameObject型
                GameObject gameObjectValue = (GameObject)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new ObjectElement(FieldName, gameObjectValue));
                break;
            default: 
                break;

        }
        scriptNode.RefreshExpandedState();
    }
}
