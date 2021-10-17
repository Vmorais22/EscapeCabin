using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{

    public GameObject target;
    public GameObject myHand;


    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            /*target.transform.SetParent(myHand.transform);
            target.transform.localPosition = new Vector3(0f, -.54f, 0f);*/

        }
        
    }
}
