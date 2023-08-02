using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 巡回経路が入っているのか確認用クラス
/// </summary>
public class ChackPatrolRoute : ConditionBase
{
    private BTManager bTManager = default;
    private List<GameObject> patrolRoute = default;
    public override void BTStart(BTManager manager)
    {
        bTManager = manager;
        patrolRoute = bTManager.SerchExternalVariable<List<GameObject>>("patrolRoute");
        if (patrolRoute != null) { 
            conditionFlag = true;
        }
    }
    public override void BTUpdate()
    {
        if (patrolRoute == null)
        {
            conditionFlag = false;
        }
    }
}
    