
using UnityEngine;
/// <summary>
/// プレイヤーが追跡範囲に入っているかどうか
/// </summary>
public class ChackTracingRange : ConditionBase
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
        if (other.tag == "Player")
            conditionFlag = true;
    }
}
    