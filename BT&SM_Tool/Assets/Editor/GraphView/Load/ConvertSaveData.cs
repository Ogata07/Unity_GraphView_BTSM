using System;
using UnityEngine.UIElements;
/// <summary>
/// フィールド をGraphView上で触れるようにするElement生成クラス
/// </summary>
public class ConvertSaveData 
{
    public  void ChackSaveData<T, V>(VisualElement childon, NodeData nodeData)
    where T : BaseField<V>, new()
    {
        //変換
        var castInt = childon as DataElement<T, V>;
        //名前の取得
        string loadFieldName = castInt.fieldNameLabel.text;
        //保存先のデータから同じ名前のフィールドがないか探す
        FieldData nodeData1 = nodeData.fieldData.Find(f => f.fieldName == loadFieldName);
        if (nodeData1 != null)
        {
            V value = ConvertValue<V>(nodeData1.valueData);
            castInt.field.value = value;
        }
    }
    private static T ConvertValue<T>(object value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
    }
}
