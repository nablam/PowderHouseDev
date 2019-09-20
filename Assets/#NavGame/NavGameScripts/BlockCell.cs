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
    float _blockEdgeSize;
    public float BlockEdgeSize { get => _blockEdgeSize; set => _blockEdgeSize = value; }
    float _blockHeight;
    public float BlockHeight { get => _blockHeight; set => _blockHeight = value; }
    NavMeshObstacle _MyObstacle;



    public List<Transform> Adjacents;
    public Vector3 Position;
    public int Weight;
    public Transform here;
    public int AdjacentsOpened = 0;

    private void OnEnable()
    {
        _MyObstacle = this.transform.GetComponentInChildren<NavMeshObstacle>();
        _blockEdgeSize = _MyObstacle.gameObject.transform.localScale.x;
        _blockHeight = _MyObstacle.gameObject.transform.localScale.y;
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
