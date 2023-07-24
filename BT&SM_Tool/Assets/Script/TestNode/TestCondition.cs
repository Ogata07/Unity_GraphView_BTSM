using UnityEngine;
/// <summary>
/// 一定時間後に実行可能になるスクリプトです
/// </summary>
public class TestCondition : ConditionBase
{
    private BTManager bTManager = default;
    public float time = 2;
    private float countValue = 0;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
    }
    public override void BTUpdate()
    {
        countValue += Time.deltaTime;

        if (countValue > time) { 
            conditionFlag = true;
           
        }
    }
}
