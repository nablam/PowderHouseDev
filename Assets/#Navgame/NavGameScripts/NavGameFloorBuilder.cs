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
    int _lightmatIndex = 0;
    int _darkmatIndex = 0;
    float tileEdgeSize;
    public int Width;
    public int Height;
    public Material[] LightTiles = new Material[6];
    public Material[] DarkTiles = new Material[6];

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
                //  CycleTempTileColor(TempTile);
                CycleTempTileMat(TempTile);

            }
            else
            {
                float posX = tileEdgeSize * (float)x;
                TempTile = Instantiate(FloorTileObj, new Vector3(posX, 0, rowPosZ), Quaternion.Euler(TileOrientation));
                //  CycleTempTileColor(TempTile);
                CycleTempTileMat(TempTile);

                TempTile = Instantiate(FloorTileObj, new Vector3(-posX, 0, rowPosZ), Quaternion.Euler(TileOrientation));
                //  CycleTempTileColor(TempTile);
                CycleTempTileMat(TempTile);

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


    //real bad ! must check later . this is asumnig light and dark tiles are the same. 
    //separate te two
    bool flip = false;
    void CycleTempTileMat(GameObject argTempTile)
    {
        _lightmatIndex++;
        if (_lightmatIndex >= LightTiles.Length)
        {

            _lightmatIndex = 0;
        }
        if (argTempTile != null)
        {
            int rnd = Random.Range(0, 128);
            if (rnd < 100)
                // hereit is ... thus curious 
                flip = !flip;

            if (flip)
                argTempTile.GetComponent<Renderer>().material = LightTiles[_lightmatIndex];
            else
                argTempTile.GetComponent<Renderer>().material = DarkTiles[_lightmatIndex];

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