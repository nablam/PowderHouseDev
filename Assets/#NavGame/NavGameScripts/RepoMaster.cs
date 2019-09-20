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

}
