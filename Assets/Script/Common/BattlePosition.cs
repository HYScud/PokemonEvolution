using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattlePosition
{
    private static readonly Dictionary<BattleNumTypeEnum, List<Vector2>> BattleEnemyPosition = new Dictionary<BattleNumTypeEnum, List<Vector2>>()
    {
        {BattleNumTypeEnum.Multiple,new List<Vector2>(){ new Vector2(-185,91), new Vector2(0, 91), new Vector2(185, 91), new Vector2(-91, 0), new Vector2(91, 0), } },
        {BattleNumTypeEnum.Three,new List<Vector2>(){ new Vector2(-185, 58), new Vector2(0, 58), new Vector2(185, 58) } },
        {BattleNumTypeEnum.Double,new List<Vector2>(){ new Vector2(-200,58), new Vector2(200, 58) } },
        {BattleNumTypeEnum.Single,new List<Vector2>(){ new Vector2(0, 58) } }
    };

    private static readonly Dictionary<BattleNumTypeEnum, List<Vector2>> BattleOwnPosition = new Dictionary<BattleNumTypeEnum, List<Vector2>>()
    {
        {BattleNumTypeEnum.Three,new List<Vector2>(){ new Vector2(-190, 46), new Vector2(0, 84), new Vector2(190, 46) } },
        {BattleNumTypeEnum.Double,new List<Vector2>(){ new Vector2(-200,58), new Vector2(200, 58) } },
        {BattleNumTypeEnum.Single,new List<Vector2>(){ new Vector2(0, 84) } }
    };

    private static Vector2 GetBattleEnemyPosition(BattleNumTypeEnum battleNumTypeEnum, int index)
    {
        var posList = BattleEnemyPosition[battleNumTypeEnum];
        if (posList != null)
        {
            if (0 < index && index < posList.Count)
            {
                return posList[index];
            }
            else
            {
                Debug.LogError("GetBattleOwnPosition Error index is out of List");
                return new Vector2(0, 0);
            }
        }
        else
        {
            Debug.LogError("OwnPosition is null");
            return new Vector2(0, 0);
        }
    }

    private static Vector2 GetBattleOwnPosition(BattleNumTypeEnum battleNumTypeEnum, int index)
    {
        var posList = BattleOwnPosition[battleNumTypeEnum];
        if (posList != null)
        {
            if (0 < index && index < posList.Count)
            {
                return posList[index];
            }
            else
            {
                Debug.LogError("GetBattleOwnPosition Error index is out of List");
                return new Vector2(0, 0);
            }
        }
        else
        {
            Debug.LogError("OwnPosition is null");
            return new Vector2(0, 0);
        }
    }
}
