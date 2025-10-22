using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] mapParts;
    [SerializeField] private Transform parent;
    private string[] _readLines = default!;
    private string path = @"Assets\StreamingAssets\map01.csv";

    private int mapWidth = 10;
    private int mapHeight = 10;

    private int Width { get; set; }
    private int Height { get; set; }

    private void Awake()
    {
        ReadData(path);
    }

    private void Start()
    {
        InitMap();
    }

    private void ReadData(string path)
    {
        try
        {
            _readLines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Debug.Log($"{path} 読み込みエラー: {ex.Message}");
        }

        if (_readLines.Last().Trim() == "") _readLines = _readLines.Take(_readLines.Length - 1).ToArray();//最終行が空行なら削除
        Height = _readLines.Length;
        Width = _readLines[0].Split(',').Length;
    }

    private void InitMap()
    {
        for (int x = 0; x < Width; x++)
        {
            var cells = _readLines[x].Split(',');

            for (int y = 0; y < Width; y++)
            {
                if (int.TryParse(cells[y], out int num))
                {
                    var pos = new Vector3(x * mapWidth, 0, y * mapHeight);
                    var parts = Instantiate(mapParts[num], pos, Quaternion.identity, parent);
                }
                else
                {
                    Debug.Log("CSVデータに不備があります");
                }
            }
        }

        //var parentPos = new Vector3();
        //parentPos.x = 
        //parent.transform.position = parentPos;
    }
}
