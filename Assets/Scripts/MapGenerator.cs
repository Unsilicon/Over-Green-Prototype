using UnityEngine;

// 生成和管理地图资源
public class MapGenerator : MonoBehaviour
{
    public enum Tile
    {
        None,
        Soil
    }

    [SerializeField]
    private int mapSeed;

    [SerializeField]
    private int mapWidth;

    [SerializeField]
    private int mapHeight;

    // 地形的最高高度
    [SerializeField]
    private int terrainHeight;

    // 地形的缩放比例，越大则地形越平滑
    [SerializeField]
    private float terrainScale;

    [SerializeField]
    private GameObject tileSoil;

    private Tile[,] terrainMap;

    private void Awake()
    {
        terrainMap = new Tile[mapWidth, mapHeight];
    }

    private void Start()
    {
        RandomSeed();
        GenerateSoil();
        InstantiateTerrain();
    }
    
    // 随机生成地图种子
    private void RandomSeed()
    {
        if (mapSeed == 0)
        {
            mapSeed = Random.Range(0, 1000);
        }
    }

    // 生成地形中的土壤部分
    private void GenerateSoil()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            float sample = Mathf.PerlinNoise1D(x * terrainScale + mapSeed * 0.01f);
            for (int y = 0; y < terrainHeight; y++)
            {
                if (y < sample * terrainHeight)
                {
                    terrainMap[x, y] = Tile.Soil;
                }
            }
        }
    }

    // 实例化地形部分
    private void InstantiateTerrain()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (terrainMap[x, y] != Tile.None)
                {
                    GameObject tile = null;
                    switch (terrainMap[x, y])
                    {
                        case Tile.Soil:
                            tile = tileSoil;
                            break;
                    }
                    Instantiate(tile, new(x - mapWidth / 2f, y - mapHeight / 2f), Quaternion.identity);
                }
            }
        }
    }
}
