using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveField 
{
    public void ChackField(VisualElement fieldElement, GraphAsset graphAsset,int listNumber) {
        if (fieldElement is DataElement<FloatField, float> floatElement)
        {
            AddFieldData(graphAsset, listNumber, "System.Single", floatElement.fieldNameLabel.text, floatElement.Field.value.ToString());
            return;
        }
        if (fieldElement is DataElement<IntegerField, int> intElement)
        {
            AddFieldData(graphAsset, listNumber, "System.Int32", intElement.fieldNameLabel.text, intElement.Field.value.ToString());
            return;
        }
        if (fieldElement is DataElement<Toggle, bool> boolElement)
        {
            AddFieldData(graphAsset, listNumber, "System.Boolean", boolElement.fieldNameLabel.text, boolElement.Field.value.ToString());
            return;
        }

        if (fieldElement is ObjectElement castFieldElement)
        {
            graphAsset.nodes[listNumber].fieldDataObject.Add(new FieldDataObject());
            //型の保存
            graphAsset.nodes[listNumber].fieldDataObject[0].typeName = "UnityEngine.GameObject";
            //名前の保存
            graphAsset.nodes[listNumber].fieldDataObject[0].fieldName = castFieldElement.fieldNameLabel.text;
            //値の保存
            var valueob = castFieldElement.objectField.value;
            Object @object = valueob as Object;

            graphAsset.nodes[listNumber].fieldDataObject[0].valueData = @object;

            return;
        }
        Debug.LogError("未分類のFieldElementがありました");
    }
    void AddFieldData(GraphAsset graphAsset, int listNumber, string typeName, string fieldName, string value)
    {
        graphAsset.nodes[listNumber].fieldData.Add(new FieldData()
        {
            typeName = typeName,
            fieldName = fieldName,
            valueData = value
        });
    }
}
