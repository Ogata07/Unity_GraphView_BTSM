using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveExternalVariable 
{
    public void ChackVariable(VisualElement externalVariableElement,GraphAsset graphAsset) {
        if (externalVariableElement is BackBordElement<IntegerField, int> castIntElement) {
            AddExternalVariableData(graphAsset,castIntElement.typeText.value,castIntElement.nameText.value,castIntElement.value.value.ToString());
        }
    }
    void AddExternalVariableData(GraphAsset graphAsset, string variableType, string variableName, string variableValue) {
        graphAsset.keyValues.Add(new ExternalVariable(){ 
         variableType = variableType,
         variableName=variableName, 
         variableValue=variableValue
        });
    }
}
