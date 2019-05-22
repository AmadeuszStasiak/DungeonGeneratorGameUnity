using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Doors_Dangeon1 : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    void Update()
    {
        
    }
}
