using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3.5f;
    public float distanceOfRaycast = 0.000000000005f;


    private float gravity = 10f;
    private RaycastHit _hit;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Camera.main.transform.position = new Vector3(-0.4854f, 0.7f, 4.26811f);

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out _hit, distanceOfRaycast))
        {
            if(Input.GetButtonDown("Fire1") && _hit.transform.CompareTag("rotate"))
            {
                _hit.transform.gameObject.GetComponent<Rotate>().ChangeSpin();
            }
        }
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
    }
}
