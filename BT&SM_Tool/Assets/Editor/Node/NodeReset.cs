using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeReset : MonoBehaviour
{
    //extensionContainer�ɂ���v�f�̃��Z�b�g
    public static void extensionContainerReset(ScriptNode scriptNode) {
        Debug.Log(scriptNode.extensionContainer.childCount);
    }

}
