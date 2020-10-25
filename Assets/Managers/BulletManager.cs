using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Managers
{
    public class BulletManager : MonoBehaviour
    {
        public float moveSpeed;
        Rigidbody2D rigidbody2d;
        Vector2 startPosition;
        int direction = 1;
        public int MaxDistance;
        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            startPosition = gameObject.transform.localPosition;
        }
        private void Update()
        {
            if (transform.rotation.eulerAngles.y == 0)
                direction = 1;
            else if (transform.rotation.eulerAngles.y == 180)
                direction = -1;
            if (direction == 1 && startPosition.x + MaxDistance * direction < gameObject.transform.localPosition.x)
                Destroy(gameObject);
            else if(direction == -1 && startPosition.x + MaxDistance * direction > gameObject.transform.localPosition.x)
                Destroy(gameObject);
            rigidbody2d.velocity = new Vector2(moveSpeed * direction, rigidbody2d.velocity.y);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Equals("Player"))
                Destroy(gameObject);
        }
    }
}
