
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

    }
}
    