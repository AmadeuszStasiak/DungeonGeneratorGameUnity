using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV2Projectile : MonoBehaviour
{

    public float speed;

    private Transform target;

    private Vector2 player;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = new Vector2(target.position.x, target.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player, speed * Time.deltaTime);
        if(transform.position.x == player.x && transform.position.y == player.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
