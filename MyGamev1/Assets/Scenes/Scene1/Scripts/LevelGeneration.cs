using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;  // index 0 --> LR, index 1 =LRB. index 2 = LRT, index 3 = LRBT, index 4 = StartRoom
    public GameObject player;
    public GameObject mainCamera;
    public GameObject[] Pose;

    public static int pose;

    bool destroyOnce=false;

    private int direction;
    public float moveAmount;
    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool stopGeneration;

    public LayerMask room;

    private int downCounter;



    // Start is called before the first frame update
    void Start()
    {
        pose = Pose.Length;
        stopGeneration = false;

        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[4], transform.position, Quaternion.identity);

        Instantiate(player, transform.position, Quaternion.identity);
        Instantiate(mainCamera, transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
        WhatIsPlayer.instance.player = player = GameObject.FindGameObjectWithTag("Player");

    }
    

    private void Move()
    {
        if(direction==1||direction==2)
        {
           

            if(transform.position.x<maxX)
            {
				downCounter = 0;
				Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);


                direction = Random.Range(1, 6);
                if(direction==3)
                {
                    direction = 2;
                }
                else if(direction==4)
                {
                    direction = 5;
                }

            }
            else
            {
                direction = 5;
            }
                                  
        }
        else if (direction == 3 || direction == 4)
        {
            

            if (transform.position.x>minX)
            {
				downCounter = 0;
				Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);


            }
            else
            {
                direction = 5;
            }
           
        }else if (direction == 5)
        {

            downCounter++;
            if(transform.position.y>minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBoottomRoom = Random.Range(1, 4);
                        if (randBoottomRoom == 2)
                        {
                            randBoottomRoom = 1;
                        }
                        Instantiate(rooms[randBoottomRoom], transform.position, Quaternion.identity);
                    }

                   
                }
                



                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);


                direction = Random.Range(1, 6);

            }
            else
            {
                stopGeneration = true;
                

            }
            
        }


      
       


    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeBtwRoom<=0 && stopGeneration== false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }else
        {
            timeBtwRoom -= Time.deltaTime;
        }
        if(pose <= 0 && destroyOnce==false)
        {
            Destroying();
            destroyOnce = true;
        }
    }
    void Destroying()
    {
       
        StartCoroutine(Example2());
        

    }
    
    IEnumerator Example2()
    {
		
		yield return new WaitForSeconds(0.5f);
		AstarPath.active.Scan();
		Destroy(gameObject);


    }

}
