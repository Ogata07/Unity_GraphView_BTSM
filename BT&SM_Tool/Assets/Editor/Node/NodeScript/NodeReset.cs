using UnityEngine;
/// <summary>
/// Nodeに追加するときに依然ついていたFieldElementを外すクラス
/// </summary>
public class NodeReset : MonoBehaviour
{
    public static void ExtensionContainerReset(ScriptNode scriptNode) {
        if (scriptNode.extensionContainer.childCount > 0)
        {
            int extensionContainerchildCount = scriptNode.extensionContainer.childCount;
            //要素がなくなるまでRemoceAtしまくる
            for (int i = extensionContainerchildCount - 1; i >= 0; i--) 
                scriptNode.extensionContainer.RemoveAt(i);
        }
    }

}
