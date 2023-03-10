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
        public virtual void BTStart()
        {

        }
        public virtual void BTUpdate()
        {

        }
        public virtual void BTEnd()
        {

        }
        /// <summary>
        /// 次のノードへ移行
        /// </summary>
        public void BTNext(SMManager sMManager)
        {
            sMManager.Next();
        }
    }
}
