using UnityEngine;
/// <summary>
/// プレイヤーが攻撃範囲に入っているかどうか
/// </summary>
public class ChackAttackRange : ConditionBase
{
    private BTManager bTManager = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
            conditionFlag = true;
    }
}
    