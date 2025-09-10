using UnityEngine;

// ���ɺ͹����ͼ��Դ
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

    // ���ε���߸߶�
    [SerializeField]
    private int terrainHeight;

    // ���ε����ű�����Խ�������Խƽ��
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
    
    // ������ɵ�ͼ����
    private void RandomSeed()
    {
        if (mapSeed == 0)
        {
            mapSeed = Random.Range(0, 1000);
        }
    }

    // ���ɵ����е���������
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

    // ʵ�������β���
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
