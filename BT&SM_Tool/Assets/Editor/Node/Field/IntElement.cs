using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using System.Windows.Forms;
using Codice.CM.Common.Serialization;

public class IntElement:VisualElement
{
    public IntegerField integerField = default;
    private VisualElement visualElement = default;
    public UnityEngine.UIElements.Label fieldNameLabel = default;
    // Start is called before the first frame update
    public IntElement() {
        ValueAdd();
    }
    public IntElement(int intValue){
        ValueAdd();
        integerField.value = intValue; 
    }
    private void ValueAdd() {

        ElementStyleSetting.Setting(this);
        style.flexWrap = new StyleEnum<Wrap>(Wrap.Wrap);
        fieldNameLabel = new UnityEngine.UIElements.Label();
        fieldNameLabel.text = "フィールド名";
        fieldNameLabel.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        //文字がはみ出た時の対応方法を指定
        Add(fieldNameLabel);
        style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        //style.alignItems = new StyleEnum<Align>(Align.Center);
        integerField = new IntegerField();
        integerField.style.width = new StyleLength(new Length(50, LengthUnit.Pixel));
        //文字がはみ出た時の対応方法を指定
        integerField.style.whiteSpace=WhiteSpace.Normal;
        integerField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        //TODO 入力が左に沿っているので右沿いでの入力にしたい
        Add(integerField);
    }
}
