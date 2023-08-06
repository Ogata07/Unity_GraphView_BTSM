
using UnityEngine;
/// <summary>
/// キャラのHPを見て判定値以下ならTrueにする
/// </summary>
public class ChackHp : ConditionBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        Debug.Log(bTManager.SerchExternalVariable<int>("Hp"));
        //TODO BTManagerに
        if (0 > bTManager.SerchExternalVariable<int>("Hp")) { 
            conditionFlag = true;
        }
    }
}
    