using UnityEngine;

public class RepoMaster : MonoBehaviour
{
    public static RepoMaster Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    public Material BlackMat;
    public Material WhiteMat;
    public Material YellowMat;
    public Material RedMat;
    public Material GreenMatLight;
    public Material GreenMatMid;
    public Material GreenMatDark;
    public Material[] LightTiles = new Material[6];
    public Material[] DarkTiles = new Material[6];
    public Material[] QuadTiles = new Material[4];


    int LightIndex = 0;
    int DarkIndex = 0;
    bool flipflop = true;
    public Material GetAlternatingTiles()
    {
        if (flipflop)
        {
            flipflop = !flipflop;
            LightIndex++;
            if (LightIndex >= 6) LightIndex = 0;
            return LightTiles[LightIndex];

        }
        else
        {
            flipflop = !flipflop;
            DarkIndex++;
            if (DarkIndex >= 6) DarkIndex = 0;
            return DarkTiles[DarkIndex];
        }

    }

    public Material GetRandQuadMat()
    {
        int r = Random.Range(0, 4);

        return QuadTiles[r];


    }
}
