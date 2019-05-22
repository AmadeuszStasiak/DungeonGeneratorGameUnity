using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowV2 : MonoBehaviour
{

    public float speed;
    public float stopingDistance;
    public float retreatDistance;

    private Transform target;

    public float SeeingRange;


    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = WhatIsPlayer.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < SeeingRange)
        {
            if (Vector2.Distance(transform.position, target.position) > stopingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, target.position) < stopingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance && stopingDistance != 0) 
            {
                transform.position = this.transform.position;
            }
            else if(Vector2.Distance(transform.position, target.position) < retreatDistance && retreatDistance !=0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }

           
        }

        

    }
}