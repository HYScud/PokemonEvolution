using UnityEngine;

public class PokemonTable
{

    public static float[][] TypeChart;

    public static string[][] Type_Color;

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
    public static string GetTypeColorEffect(int index, UITypeColorEnum TypeColorEnum)
    {
        if (Type_Color == null || Type_Color[index] == null || Type_Color[index].Length == 0)
        {
            Debug.LogError("获取颜色失败");
            return "";
        }
        return Type_Color[index][(int)TypeColorEnum];
    }
}
