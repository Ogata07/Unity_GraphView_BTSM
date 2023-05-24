using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
/// <summary>
/// Field��GameObject��GraphView��ŕ\��������X�N���v�g
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
        this.name = "ObjectField";
        Add(objectField);


    }
}

