using UnityEngine;
//[RequireComponent(typeof(Rigidbody))]
public class GenericDraggableRigidBody : MonoBehaviour
{


    float _minY = -5f;
    float _maxY = 5f;
    float _minX = -7.5f;
    float _maxX = 7.5f;

    float _margin = 0.2f; //if object hits wall, place it off the wall by this margin
    private Vector3 offset;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //GetComponent<BoxCollider2D>().sharedMaterial = null;
    }
    void OnMouseDown()
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
        }
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }

    void OnMouseUp()
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
        }

        rb.freezeRotation = false;
        //  rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 2;
    }
    private void FixedUpdate()
    {
        CheckOutOfBOundLow();
        CheckOutOfBOundLeft();
        CheckOutOfBOundRight();
        CheckOutOfBOundTop();
    }

    void CheckOutOfBOundLow()
    {
        if (transform.position.y <= _minY)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            rb.isKinematic = true;
            transform.position = new Vector3(this.transform.position.x, _minY, this.transform.position.z);
        }

    }

    void CheckOutOfBOundLeft()
    {
        if (transform.position.x <= _minX)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = new Vector2(3, 0);
            rb.gravityScale = 0;
            //rb.isKinematic = true;
            transform.position = new Vector3(_minX + _margin, this.transform.position.y, this.transform.position.z);
        }
    }
    void CheckOutOfBOundRight()
    {
        if (transform.position.x >= _maxX)
        {
            rb.freezeRotation = true;
            transform.position = new Vector3(_maxX - _margin, this.transform.position.y, this.transform.position.z);
            // rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            //rb.velocity = new Vector2(-3, 0);
            rb.AddForce(Vector2.right * 2);
            rb.gravityScale = 2;
            //rb.isKinematic = true;
        }
    }


    void CheckOutOfBOundTop()
    {
        if (transform.position.y >= _maxY)
        {

            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            rb.isKinematic = true;
            transform.position = new Vector3(this.transform.position.x, _maxY - _margin, this.transform.position.z);
            rb.gravityScale = 2;

        }
    }
}
