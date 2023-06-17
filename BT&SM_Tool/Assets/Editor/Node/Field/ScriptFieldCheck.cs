using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
/// <summary>
/// ScriptNodeに置かれたスクリプトのFieldを呼んで対応するFieldElementを生成するクラス
/// </summary>
public class ScriptFieldCheck : MonoBehaviour
{
    public void Check(UnityEngine.Object @object, ScriptNode scriptNode) {
    //パブリックフィールドを取得
    MonoScript value=@object as MonoScript;
    Type getType = value.GetClass();
    FieldInfo[] fieldInfos = getType.GetFields(
            //BindingFlags.NonPublic
            BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.DeclaredOnly
            );

        //Node内にすでにあるならリセットする
        NodeReset.ExtensionContainerReset(scriptNode);
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
        String fieldName = fieldInfo.Name;
        //インスタンス生成
        var activeScript = Activator.CreateInstance(getType);
        switch (fieldInfo.FieldType.ToString()) {
            case "System.Int32"://int型
                int intValue = (int)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<IntegerField, int>(fieldName, intValue));
                break;
            case "System.Single"://Float型
                float floatValue = (float)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<FloatField, float>(fieldName, floatValue));
                break;
            case "System.Boolean"://Bool型
                //int intValue = (int)fieldInfo.GetValue(activeScript);
                bool boolValue = (bool)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new DataElement<Toggle, bool>(fieldName, boolValue));
                break;
            case "UnityEngine.GameObject"://GameObject型
                GameObject gameObjectValue = (GameObject)fieldInfo.GetValue(activeScript);
                scriptNode.extensionContainer.Add(new ObjectElement(fieldName, gameObjectValue));
                break;
            default: 
                break;

        }
        scriptNode.RefreshExpandedState();
    }
}
