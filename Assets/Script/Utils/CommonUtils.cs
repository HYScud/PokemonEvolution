using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils
{
    public static Dictionary<long, Vector3> AllUnreachablePos = new Dictionary<long, Vector3>();

    public static void CorrectPosition(Vector3 pos)
    {
        pos.x = Mathf.Round(pos.x - 0.5f);
        pos.y = Mathf.Round(pos.y - 0.5f);
        pos.z = Mathf.Round(pos.z);
    }
    public static bool CheckIsReachable(Vector3 targetPos)
    {
        foreach (KeyValuePair<long, Vector3> kv in AllUnreachablePos)
        {

            if (kv.Value.Equals(targetPos))
            {
                return false;
            }
        }
        return true;
    }

    public static void SaveCharacterPos(long id, Vector3 targetPos)
    {
        if (!AllUnreachablePos.ContainsKey(id))
        {
            AllUnreachablePos.Add(id, targetPos);
        }
        else
        {
            AllUnreachablePos[id] = targetPos;
        }
    }
    /*获取等级的经验和(2-100->2代表升到2级累积经验)*/
    public static int GetExpByLevel(int level, ExpTypeEnum expTypeEnum)
    {
        int exp = 0;
        if (level > 100)
        {
            level = 100;
        }
        level = level < 1 ? 1 : level;
        if (level <= 1)
        {
            return exp;
        }
        switch (expTypeEnum)
        {
            case ExpTypeEnum.Best_Fast:
                if (level >= 99)
                {
                    exp = Mathf.FloorToInt(-0.01f * Mathf.Pow(level, 4) + 1.6f * Mathf.Pow(level, 3));
                }
                else if (level >= 69)
                {
                    exp = Mathf.FloorToInt(0.002f * Mathf.Pow(level, 3) * Mathf.FloorToInt((1911 - 10 * level) / 3));
                }
                else if (level >= 51)
                {
                    exp = Mathf.FloorToInt(-0.01f * Mathf.Pow(level, 4) + 1.5f * Mathf.Pow(level, 3));
                }
                else
                {
                    exp = Mathf.FloorToInt(-0.02f * Mathf.Pow(level, 4) + 2 * Mathf.Pow(level, 3));
                }
                break;
            case ExpTypeEnum.Faster:
                exp = Mathf.FloorToInt(0.8f * Mathf.Pow(level, 3));
                break;
            case ExpTypeEnum.Fast:
                exp = Mathf.FloorToInt(Mathf.Pow(level, 3));
                break;
            case ExpTypeEnum.Slow:
                exp = Mathf.FloorToInt(1.2f * Mathf.Pow(level, 3) - 15 * Mathf.Pow(level, 2) + 100 * level - 140);
                break;
            case ExpTypeEnum.Slower:
                exp = Mathf.FloorToInt(1.25f * Mathf.Pow(level, 3));
                break;
            case ExpTypeEnum.Best_Slow:
                if (level >= 37)
                {
                    exp = Mathf.FloorToInt(0.02f * Mathf.Pow(level, 3) * Mathf.FloorToInt(level + 64 / 2));
                }
                else if (level >= 16)
                {
                    exp = Mathf.FloorToInt(0.02f * Mathf.Pow(level, 4) + 0.28f * Mathf.Pow(level, 3));
                }
                else
                {
                    exp = Mathf.FloorToInt(-0.02f * Mathf.Pow(level, 3) * Mathf.FloorToInt(level + 73) / 3);
                }
                break;
        }
        return exp;
    }
    /*获取当前等级的经验*/
    public static int GetExpByCurLevel(int level, ExpTypeEnum expTypeEnum)
    {
        return CommonUtils.GetExpByLevel(level + 1, expTypeEnum) - CommonUtils.GetExpByLevel(level, expTypeEnum);
    }
}
