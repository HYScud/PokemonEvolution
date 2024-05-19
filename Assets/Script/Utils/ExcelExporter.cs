using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using UnityEngine;

public class ReadExcel : MonoBehaviour
{

    private string FilePath;


    private void Start()
    {
        ReadNatureExcelStream();
        ReadTypeExcelStream();
        ReadTypeColorExcelStream();
    }

    void ReadNatureExcelStream()
    {
        string FilePath = Application.dataPath + "/Resources/DataTables/NatrueCharts.xlsx";
        FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
        if (stream != null)
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();
            if (result != null)
            {
                int columns = result.Tables[0].Columns.Count;
                int rows = result.Tables[0].Rows.Count;
                PokemonTable.NatureChart = new float[rows - 1, columns - 1];
                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < columns; j++)
                    {
                        PokemonTable.NatureChart[i - 1, j - 1] = (float)Convert.ToSingle(result.Tables[0].Rows[i][j]);
                    }
                }
            }
            else
            {
                Debug.Log("读取性格表失败");
            }
            excelDataReader.Close();
        }
    }
    void ReadTypeExcelStream()
    {
        string FilePath = Application.dataPath + "/Resources/DataTables/TypeCharts.xlsx";
        FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
        if (stream != null)
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();
            if (result != null)
            {
                int columns = result.Tables[0].Columns.Count;
                int rows = result.Tables[0].Rows.Count;
                PokemonTable.TypeChart = new float[rows - 1][];
                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < columns; j++)
                    {
                        var tempList = PokemonTable.TypeChart[i - 1];
                        if (tempList == null)
                        {
                            tempList = new float[columns - 1];
                            PokemonTable.TypeChart[i - 1] = tempList;
                        }
                        tempList[j - 1] = (float)Convert.ToSingle(result.Tables[0].Rows[i][j]);
                    }
                }
            }
            else
            {
                Debug.Log("读取属性表失败");
            }
            excelDataReader.Close();
        }
    }
    void ReadTypeColorExcelStream()
    {
        string FilePath = Application.dataPath + "/Resources/DataTables/Type_Color.xlsx";
        FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
        if (stream != null)
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();
            if (result != null)
            {
                int columns = result.Tables[0].Columns.Count;
                int rows = result.Tables[0].Rows.Count;
                PokemonTable.Type_Color = new string[rows - 1][];
                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < columns; j++)
                    {
                        var tempList = PokemonTable.Type_Color[i - 1];
                        if (tempList == null)
                        {
                            tempList = new string[columns - 1];
                            PokemonTable.Type_Color[i - 1] = tempList;
                        }
                        tempList[j - 1] = (string)(result.Tables[0].Rows[i][j]);
                    }
                }
            }
            else
            {
                Debug.Log("读取属性表失败");
            }
            excelDataReader.Close();
        }
    }
}