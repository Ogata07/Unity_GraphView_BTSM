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
        fieldNameLabel.text = "�t�B�[���h��";
        //SetMargin(fieldNameLabel, 0);
        //SetPadding(fieldNameLabel, 0);
        fieldNameLabel.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        Add(fieldNameLabel);
        style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        //style.alignItems = new StyleEnum<Align>(Align.Center);
        //Port
        integerField = new IntegerField();
        //SetMargin(integerField, 0);
        //SetPadding(integerField, 0);
        integerField.style.width = new StyleLength(new Length(50, LengthUnit.Pixel));
        //�������͂ݏo�����̑Ή����@���w��
        integerField.style.whiteSpace=WhiteSpace.Normal;
        integerField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        //TODO ���͂����ɉ����Ă���̂ŉE�����ł̓��͂ɂ�����
        Add(integerField);
    }
    private static void SetMargin(VisualElement element, float px)
    {
        element.style.marginLeft = px;
        element.style.marginTop = px;
        element.style.marginRight = px;
        element.style.marginBottom = px;
    }

    private static void SetPadding(VisualElement element, float px)
    {
        element.style.paddingLeft = px;
        element.style.paddingTop = px;
        element.style.paddingRight = px;
        element.style.paddingBottom = px;
    }
}
