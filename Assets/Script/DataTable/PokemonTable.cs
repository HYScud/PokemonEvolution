using UnityEngine;

public class PokemonTable
{

    public static float[][] TypeChart;

    public static float[,] NatureChart;

    public static float GetNatureEffect(int index, NatrueTypeEnum natrueTypeEnum)
    {
        if (NatureChart == null)
        {
            Debug.LogError("获取性格修正失败");
            return 0;
        }
        return NatureChart[index, (int)natrueTypeEnum];
    }
    public static float GetTypeEffect(int index, NatrueTypeEnum natrueTypeEnum)
    {
        if (TypeChart == null || TypeChart[index] == null || TypeChart[index].Length == 0)
        {
            Debug.LogError("获取性格修正失败");
            return 0;
        }
        return TypeChart[index][(int)natrueTypeEnum];
    }
}
