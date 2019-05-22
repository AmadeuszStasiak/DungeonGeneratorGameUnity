using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsPlayer : MonoBehaviour
{
    #region Singleton

    public static WhatIsPlayer instance;
    public GameObject player;
    private void Awake()
    {
        instance = this;
        
    }

    

    #endregion
}
