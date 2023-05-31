using UnityEngine.UIElements;
public class FieldElement : VisualElement
{
    public UnityEngine.UIElements.Label fieldNameLabel = default;
    public FieldElement() {
        ElementStyleSetting.Setting(this);
        style.flexWrap = new StyleEnum<Wrap>(Wrap.Wrap);
        //���O
        fieldNameLabel = new UnityEngine.UIElements.Label();
        fieldNameLabel.text = "�t�B�[���h��";
        fieldNameLabel.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        fieldNameLabel.style.whiteSpace = WhiteSpace.Normal;
        Add(fieldNameLabel);
    }
}
