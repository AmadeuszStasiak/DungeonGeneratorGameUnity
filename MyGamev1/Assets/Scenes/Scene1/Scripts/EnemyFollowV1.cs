using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowV1 : MonoBehaviour
{

    public float speed;

    private Transform target;

    public float SeeingRange;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position)< SeeingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }
    
}
