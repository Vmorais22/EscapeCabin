using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Drawer;
    public GameObject Object;

    public bool alreadyGrabbed = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(alreadyGrabbed);

        if(!alreadyGrabbed)
        {
            Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Drawer.transform.position.z);
        }
        
    }
}
