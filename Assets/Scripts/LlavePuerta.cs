﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavePuerta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            GetComponent<Animation>().Play();
        }
    }
}