﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;




[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyFollowV3 : MonoBehaviour
{

    public Transform target;
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

    public float speed;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWaypointDistance;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //target = WhatIsPlayer.instance.player.transform;

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Did it have an error ?: " + p.error);
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWaypoint>=path.vectorPath.Count)
        {
            if(pathIsEnded)
            {
                return;
            }

            pathIsEnded = true;
            return;

        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist<nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    IEnumerator UpdatePath()
    {
        
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
}
