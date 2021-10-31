using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Drawer;
    public GameObject Object;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Drawer.transform.position.z);
    }
}
