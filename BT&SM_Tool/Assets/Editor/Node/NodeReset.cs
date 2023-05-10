using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeReset : MonoBehaviour
{
    //extensionContainerにある要素のリセット
    public static void extensionContainerReset(ScriptNode scriptNode) {
        Debug.Log(scriptNode.extensionContainer.childCount);
    }

}
