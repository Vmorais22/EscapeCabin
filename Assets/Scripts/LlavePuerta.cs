using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavePuerta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject reactiveObject;
    private bool sofa1 = false;
    private bool sofa2 = false;
    public Material matSofa1;
    public Material matSofa2;
    private bool abierto = false;
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
        if (transform.tag == "door2")
        {
            if (GameObject.Find("PFB_Sofa").GetComponent<Renderer>().material.name.Substring(0, 5) == matSofa1.name)
            {
                sofa1 = true;
            }
            else sofa1 = false;

            if (GameObject.Find("PFB_SofaSmall").GetComponent<Renderer>().material.name.Substring(0, 5) == matSofa2.name)
            {
                sofa2 = true;
            }
            else sofa2 = false;
            if (sofa1 && sofa2 && !abierto)
            {
                GetComponent<Animation>().Play();
                abierto = true;
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == reactiveObject && transform.tag == "door1")
        {
            GetComponent<Animation>().Play();
            Destroy(collision.collider.gameObject);
            GameObject.Find("XR Rig").GetComponent<PlayerMove>().selected = false;

        }
    }
}
