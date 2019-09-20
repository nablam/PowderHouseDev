using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BlockCell : MonoBehaviour
{
    public TMPro.TextMeshPro m_Text;
    public GameObject MyTile;
    Renderer MyTileRenderer;
    public void UpdateBlockText(string argTxt) { m_Text.text = argTxt; }
    public void UpdateBlockTileMaterial(Material argMat) { MyTile.GetComponent<Renderer>().material = argMat; }
    public SubBlockVisualCtrl MySubBlock;
    float _blockEdgeLength;
    public float BlockEdgeLength { get => _blockEdgeLength; set => _blockEdgeLength = value; }
    NavMeshObstacle _MyObstacle;



    public List<Transform> Adjacents;
    public Vector3 Position;
    public int Weight;
    public Transform here;
    public int AdjacentsOpened = 0;

    private void OnEnable()
    {
        _MyObstacle = this.transform.GetComponentInChildren<NavMeshObstacle>();
        _blockEdgeLength = _MyObstacle.gameObject.transform.localScale.x;
    }
    public void Set_ObstacleUpDown(bool argUP)
    {
        if (argUP)
        {
            _MyObstacle.gameObject.transform.Translate(Vector3.up * _blockEdgeLength);
        }
        else
            _MyObstacle.gameObject.transform.Translate(Vector3.down * _blockEdgeLength);


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
