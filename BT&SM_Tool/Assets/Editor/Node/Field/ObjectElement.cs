using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using System.Windows.Forms;
using Codice.CM.Common.Serialization;
using System;

public class ObjectElement : VisualElement
{
    public UnityEngine.UIElements.Label fieldNameLabel = default;
    public ObjectField objectField = default;

    public ObjectElement(GameObject objectValue)
    {
        ValueAdd();
        objectField.value = objectValue;
    }
    private void ValueAdd()
    {

        ElementStyleSetting.Setting(this);
        style.flexWrap = new StyleEnum<Wrap>(Wrap.Wrap);
        //���O
        fieldNameLabel = new UnityEngine.UIElements.Label();
        fieldNameLabel.text = "�t�B�[���h��";
        fieldNameLabel.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        fieldNameLabel.style.whiteSpace = WhiteSpace.Normal;
        Add(fieldNameLabel);

        style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);

        objectField = new ObjectField();
        objectField.objectType = typeof(GameObject);
        objectField.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        objectField.style.whiteSpace = WhiteSpace.Normal;
        objectField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        Add(objectField);


    }
}

