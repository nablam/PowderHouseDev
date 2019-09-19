using UnityEngine;

public class NavGameFloorBuilder : MonoBehaviour
{

    public GameObject FloorTileObj;
    public GameObject BlockWallObj;
    public Material DarkGreen;
    public Material MidGreen;
    public Material LightGreen;
    const int MaxTileMaterials = 3;

    int _matIndex = 0;
    float tileEdgeSize;
    public int Width;
    public int Height;
    Material[] TileMats;
    Vector3 TileOrientation = new Vector3(90f, 0, 0);
    private void Awake()
    {
        TileMats = new Material[MaxTileMaterials];
        TileMats[0] = DarkGreen;
        TileMats[1] = MidGreen;
        TileMats[2] = LightGreen;
        tileEdgeSize = FloorTileObj.transform.localScale.x;
        Debug.Log(tileEdgeSize);
    }
    // Start is called before the first frame update
    void Start()
    {
        BuildTiles();
    }

    void BuildRow(float rowPosZ)
    {
        GameObject TempTile;
        for (int x = 0; x < Width / 2; x++)
        {
            if (x == 0)
            {
                TempTile = Instantiate(FloorTileObj, new Vector3(0, 0, rowPosZ), Quaternion.Euler(TileOrientation));
                CycleTempTileColor(TempTile);

            }
            else
            {
                float posX = tileEdgeSize * (float)x;
                TempTile = Instantiate(FloorTileObj, new Vector3(posX, 0, rowPosZ), Quaternion.Euler(TileOrientation));
                CycleTempTileColor(TempTile);

                TempTile = Instantiate(FloorTileObj, new Vector3(-posX, 0, rowPosZ), Quaternion.Euler(TileOrientation));
                CycleTempTileColor(TempTile);

            }
        }
    }

    void CycleTempTileColor(GameObject argTempTile)
    {
        _matIndex++;
        if (_matIndex >= MaxTileMaterials)
        {
            _matIndex = 0;
        }
        if (argTempTile != null)
        {
            argTempTile.GetComponent<Renderer>().material = TileMats[_matIndex];
            //
        }


    }
    void BuildTiles()
    {
        for (int y = 0; y < Height / 2; y++)
        {

            if (y == 0)
            {
                BuildRow(0f);
            }
            else
            {
                float posY = tileEdgeSize * (float)y;
                BuildRow(posY);

                BuildRow(-posY);
                _matIndex++;

            }

        }
    }

}