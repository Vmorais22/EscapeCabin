using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfTouched : MonoBehaviour
{

    public bool touched;
    // Start is called before the first frame update
    void Start()
    {
        touched = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touched)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
