using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavePuerta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject reactiveObject;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == reactiveObject)
        {
            GetComponent<Animation>().Play();
            Destroy(collision.collider.gameObject);
            GameObject.Find("XR Rig").GetComponent<PlayerMove>().selected = false;
            

        }
    }
}
