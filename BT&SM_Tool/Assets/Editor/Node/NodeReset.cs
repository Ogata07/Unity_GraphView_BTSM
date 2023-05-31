using UnityEngine;
/// <summary>
/// Node�ɒǉ�����Ƃ��ɈˑR���Ă����̂��O���N���X
/// </summary>
public class NodeReset : MonoBehaviour
{
    //extensionContainer�ɂ���v�f�̃��Z�b�g
    public static void extensionContainerReset(ScriptNode scriptNode) {
        if (scriptNode.extensionContainer.childCount > 0)
        {
            int extensionContainerchildCount = scriptNode.extensionContainer.childCount;
            //�v�f���Ȃ��Ȃ�܂�RemoceAt���܂���
            for (int i = extensionContainerchildCount - 1; i >= 0; i--) 
                scriptNode.extensionContainer.RemoveAt(i);
        }
    }

}
