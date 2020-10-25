using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public float DefaultEnemySpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        IgnoreEnemies();
    }

    public void IgnoreEnemies()
    {
        for(int i = 0; i < Enemies.Count - 1; i++)
        {
            for(int j = i + 1; j < Enemies.Count; j++)
            {
                Physics2D.IgnoreCollision(Enemies[i].GetComponent<PolygonCollider2D>(), Enemies[j].GetComponent<PolygonCollider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
