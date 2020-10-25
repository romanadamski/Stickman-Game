using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    Rigidbody2D rigidbody2d;
    Vector2 startPosition;
    int direction = 1;
    public int MaxDistance;
    Quaternion turnRight;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.localPosition;
        SetDirections();
    }
    void SetDirections()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x + 180, rot.y, rot.z);
        turnRight = Quaternion.Euler(rot);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            direction = 1;
        }
        else if (transform.rotation.eulerAngles.y == 180)
            direction = -1;
        if (direction == 1 && startPosition.x + MaxDistance * direction < gameObject.transform.localPosition.x)
            Destroy(gameObject);
        else if (direction == -1 && startPosition.x + MaxDistance * direction > gameObject.transform.localPosition.x)
            Destroy(gameObject);
        rigidbody2d.velocity = new Vector2(moveSpeed * direction, rigidbody2d.velocity.y);
        transform.Rotate(Vector3.forward * -rotateSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
            Destroy(gameObject);
    }
}
