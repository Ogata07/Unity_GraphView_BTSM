using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptFlow
{
    public class GraphViewScriptBase : MonoBehaviour
    {
        public int NodeNumbar = 0;
        public enum State
        {
            Normal,
            Decison
        }
        public State state = State.Normal;
        /// <summary>
        /// Start�����ł�
        /// </summary>
        public virtual void BTStart()
        {

        }
        /// <summary>
        /// �X�e�[�g�}�V���p��Start�ł�
        /// </summary>
        /// <param name="sMManager">�X�e�[�g�}�V���p�̎��s�X�N���v�g</param>
        public virtual void BTStart(SMManager sMManager)
        {
        }
        /// <summary>
        /// �r�w�C�r�A�c���[�p��Start�ł�
        /// </summary>
        /// <param name="bTManager">�r�w�C�r�A�c���[�p�̎��s�X�N���v�g</param>
        public virtual void BTStart(BTManager bTManager)
        {
        }
        public virtual void BTUpdate()
        {

        }
        public virtual void BTEnd()
        {

        }
        /// <summary>
        /// ���̃m�[�h�Ɉڍs���܂�
        /// </summary>
        /// <param name="sMManager">�X�e�[�g�}�V���p�̎��s�X�N���v�g</param>
        public virtual void BTNext(SMManager sMManager)
        {
            //TODO�@���󂾂�1�̃m�[�h�����ڍs���Ȃ��̂ō�Ƃ��K�v
            sMManager.Next();
        }
    }
}
