using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float MoveSpeed;
    Quaternion turnRight;
    Quaternion turnLeft;
    public GameObject StartEnemyPosition;
    public GameObject EndEnemyPosition;
    public Vector2 DefaultEnemyPosition;
    int direction = 1;
    void SetDirections()
    {
        turnRight = transform.rotation;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        turnLeft = Quaternion.Euler(rot);
    }
    public void ResetRotationAndPosition()
    {
        transform.localPosition = DefaultEnemyPosition;
        transform.rotation = turnRight;
        direction = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        DefaultEnemyPosition = transform.localPosition;
        SetDirections();
    }
    //todo zatrzymuje sie
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x <= StartEnemyPosition.transform.position.x)
        {
            transform.rotation = turnRight;
            direction = 1;
        }
        else if (transform.localPosition.x >= EndEnemyPosition.transform.position.x)
        {
            transform.rotation = turnLeft;
            direction = -1;
        }
        rigidbody2d.velocity = new Vector2(MoveSpeed * direction, rigidbody2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
