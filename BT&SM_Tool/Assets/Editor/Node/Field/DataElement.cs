using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// �t�B�[���h�l���m�[�h�O�ł������悤�ɂ���N���X
/// </summary>
/// <typeparam name="T">�\�����������^�́`Field</typeparam>
/// <typeparam name="V">�\�����������^</typeparam>
public class DataElement<T,V>:FieldElement
    where T : BaseField<V>,new()
{
    public T Field=default;
    public string FieldName=default;
    public DataElement()
    {
        FloatAdd();
    }
    public DataElement(V Value)
    {
        FloatAdd();
        Field.value = Value;
    }
    public DataElement(string Name,V Value)
    {
        FloatAdd();
        Field.value = Value;
        fieldNameLabel.text=Name;
    }
    private void  FloatAdd()
    {
        //�^����
        Field=new T();
        Field.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //�������͂ݏo�����̑Ή����@���w��
        Field.style.whiteSpace = WhiteSpace.Normal;
        Field.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        Add(Field);
    }
}
