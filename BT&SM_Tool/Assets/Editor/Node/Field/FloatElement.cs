using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class FloatElement : FieldElement
{
    public FloatField floatField =default;
    public FloatElement() {
        FloatAdd();
    }
    public FloatElement(float floatValue) {
        FloatAdd();
        floatField.value = floatValue;
    }
    private void FloatAdd() {
        floatField = new FloatField();
        floatField.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        floatField.style.whiteSpace = WhiteSpace.Normal;
        floatField.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        Add(floatField);
    }
        
}
