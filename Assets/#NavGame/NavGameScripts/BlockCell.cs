using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static GameEnums;

public class BlockCell : MonoBehaviour
{
    public TMPro.TextMeshPro m_Text;
    public GameObject MyTile;
    Renderer MyTileRenderer;
    Renderer MyBoxRenderer;
    public void TurnBoxRenderer(bool argOn)
    {
        if (argOn)
        {
            MyBoxRenderer.enabled = true;
            MyBoxRenderer.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
        }
        else
        {
            MyBoxRenderer.enabled = false;
            MyBoxRenderer.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
    public void UpdateBlockText(string argTxt) { m_Text.text = argTxt; }
    public void UpdateBlockTileMaterial(Material argMat) { MyTile.GetComponent<Renderer>().material = argMat; }
    public SubBlockVisualCtrl MySubBlock;
    float _blockEdgeSize;
    public float BlockEdgeSize { get => _blockEdgeSize; set => _blockEdgeSize = value; }
    float _blockHeight;
    public float BlockHeight { get => _blockHeight; set => _blockHeight = value; }

    NavMeshObstacle _MyObstacle;

    public BlockCell LeftNeighbor;
    public BlockCell FrontNeighbor;
    public BlockCell RightNeighbor;
    public BlockCell BackNeighbor;

    public List<Transform> Adjacents;
    public Vector3 Position;
    public int Weight;
    public Transform here;
    public int AdjacentsOpened = 0;
    public bool IsWall = false;


    // may not be needed , i m just gonna take each grid[,]block and check if it is raised, and if its neighbors are raised 
    BlockType _myBlockPositionType;
    public BlockType MyBlockPositionType { get => _myBlockPositionType; set => _myBlockPositionType = value; }


    private void OnEnable()
    {
        _MyObstacle = this.transform.GetComponentInChildren<NavMeshObstacle>();
        _blockEdgeSize = _MyObstacle.gameObject.transform.localScale.x;
        _blockHeight = _MyObstacle.gameObject.transform.localScale.y;
        MyBoxRenderer = _MyObstacle.gameObject.GetComponent<Renderer>();
    }
    public void Set_ObstacleUpDown(bool argUP)
    {
        if (argUP)
        {
            _MyObstacle.gameObject.transform.Translate(Vector3.up * _blockHeight);
        }
        else
            _MyObstacle.gameObject.transform.Translate(Vector3.down * _blockHeight);


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
