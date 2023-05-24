using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
/// <summary>
/// FieldのGameObjectをGraphView上で表示させるスクリプト
/// </summary>
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
        //名前
        fieldNameLabel = new UnityEngine.UIElements.Label();
        fieldNameLabel.text = "フィールド名";
        fieldNameLabel.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        //文字がはみ出た時の対応方法を指定
        fieldNameLabel.style.whiteSpace = WhiteSpace.Normal;
        Add(fieldNameLabel);

        style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);

        objectField = new ObjectField();
        objectField.objectType = typeof(GameObject);
        objectField.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //文字がはみ出た時の対応方法を指定
        objectField.style.whiteSpace = WhiteSpace.Normal;
        objectField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        this.name = "ObjectField";
        Add(objectField);


    }
}

