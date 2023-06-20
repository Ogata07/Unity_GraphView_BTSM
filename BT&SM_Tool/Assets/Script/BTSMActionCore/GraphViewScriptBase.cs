using UnityEngine;
namespace ScriptFlow
{
    /// <summary>
    /// ビジュアルスクリプティング用のUpdateなどを用意するクラス
    /// </summary>
    public class GraphViewScriptBase : MonoBehaviour
    {
        public int nodeNumbar = 0;
        public enum State
        {
            Normal,
            Decison
        }
        public State state = State.Normal;
        /// <summary>
        /// Start相当です
        /// </summary>
        public virtual void BTStart()
        {

        }
        /// <summary>
        /// ステートマシン用のStartです
        /// </summary>
        /// <param name="sMManager">ステートマシン用の実行スクリプト</param>
        public virtual void SMStart(SMManager sMManager)
        {
        }
        /// <summary>
        /// ビヘイビアツリー用のStartです
        /// </summary>
        /// <param name="bTManager">ビヘイビアツリー用の実行スクリプト</param>
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
        /// 次のノードに移行します(単体用)
        /// </summary>
        /// <param name="sMManager">ステートマシン用の実行スクリプト</param>
        public virtual void SMNext(SMManager sMManager)
        {
            sMManager.Next();
        }
        /// <summary>
        /// 次のノードに移行します(複数用)
        /// </summary>
        /// <param name="sMManager">ステートマシン用の実行スクリプト</param>
        public virtual void SMNext(SMManager sMManager, int count) {
            sMManager.Next(count);
        }
    }
}
