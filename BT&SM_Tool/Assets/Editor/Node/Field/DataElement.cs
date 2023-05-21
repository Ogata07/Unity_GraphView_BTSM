using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// フィールド値をノード外でいじれるようにするクラス
/// </summary>
/// <typeparam name="T">表示させたい型の～Field</typeparam>
/// <typeparam name="V">表示させたい型</typeparam>
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
        //型生成
        Field=new T();
        Field.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //文字がはみ出た時の対応方法を指定
        Field.style.whiteSpace = WhiteSpace.Normal;
        Field.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        Add(Field);
    }
}
