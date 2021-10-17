using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool firstGrab = false;
    public bool set = false;

    // Update is called once per frame si estoy aqui comentado algo
    void Update()
    {
        /*if (firstGrab && !set)
        {
            transform.SetParent(null);
            transform.localPosition = GameObject.FindWithTag("Player").transform.localPosition;
            Collider coll = GetComponent<Collider>();
            coll.isTrigger = false;
            Rigidbody r = GetComponent<Rigidbody>();
            r.useGravity = true;
            set = true;

        }*/

    }

    public void ChangeSpin()
    {

    }
}
