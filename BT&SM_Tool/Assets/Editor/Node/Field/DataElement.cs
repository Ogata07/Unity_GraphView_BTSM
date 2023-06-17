using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// IntやFloatなどのFieldElementを生成するクラス
/// </summary>
/// <typeparam name="T">表示させたい型の～Field</typeparam>
/// <typeparam name="V">表示させたい型</typeparam>
public class DataElement<T,V>:FieldElement
    where T : BaseField<V>,new()
{
    public T field=default;
    public string fieldName=default;
    public DataElement()
    {
        FloatAdd();
    }
    public DataElement(V value)
    {
        FloatAdd();
        field.value = value;
    }
    public DataElement(string name,V value)
    {
        FloatAdd();
        field.value = value;
        fieldNameLabel.text=name;
    }
    private void  FloatAdd()
    {
        //型生成
        field=new T();
        field.style.width = new StyleLength(new Length(90, LengthUnit.Percent));
        //文字がはみ出た時の対応方法を指定
        field.style.whiteSpace = WhiteSpace.Normal;
        field.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
        NameSet();
        Add(field);
    }
    /// <summary>
    /// 外部から読み取り用に名前欄を使用
    /// </summary>
    private void NameSet() { 
        this.name= field.GetType().Name;
    }
}
