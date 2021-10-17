using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //MOVIMIENTO
    public float speed = 3.5f;
    private float gravity = 10f;
    private CharacterController controller;

    //SELECCIÓN
    public float distanceOfRaycast = 0.000000000005f;
    public GameObject myHand;
    private Transform padrecito;
    private RaycastHit _hit;
    private bool selected = false;


    //DISTINGUIR INTERACCIÓ
    public Material cogido;
    public Material agarrado;
    private Material initMaterial;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        var dist = 1000f;

        if(Physics.Raycast(ray, out _hit, distanceOfRaycast))
        {
            dist = Vector3.Distance(_hit.transform.position, transform.position);
            if (dist < 2f) //si el objeto esta al alcance
            {
                if (_hit.transform.CompareTag("Cogido")) //si el objeto es cogible
                {
                    /*initMaterial = _hit.transform.GetComponent<Renderer>().material; //guarda material inicial
                    _hit.transform.GetComponent<Renderer>().material = cogido; //pintalo amarillo*/
                    Debug.Log("cogible");

                    if (Input.GetButtonDown("Fire1")) //si apreto y...
                    {
                        if (!selected) //no esta seleccionado lo cojo
                        {
                            Debug.Log("lo cojo");
                            padrecito = _hit.transform.parent.gameObject.transform;
                            _hit.transform.SetParent(myHand.transform);
                            _hit.collider.isTrigger = true;
                            _hit.rigidbody.useGravity = false;
                            _hit.transform.localPosition = new Vector3(0f, 0f, 0f);
                            selected = true;
                        }
                        else if (selected)//esta seleccionado lo suelto
                        {
                            Debug.Log("lo suelto");
                            _hit.transform.SetParent(padrecito);
                            _hit.collider.isTrigger = false;
                            _hit.rigidbody.useGravity = true;
                            selected = false;
                        }
                    }
                }

                if (_hit.transform.CompareTag("ArmAbIzDer"))
                {
                    if (Input.GetButtonDown("Fire1")) //si apreto y...
                    {
                        _hit.transform.GetComponent<Animation>().Play("OpenLeftRight");
                        _hit.collider.isTrigger = true;
                        _hit.transform.gameObject.tag = "ArmCiDerIz";
                    }
                }
                if (_hit.transform.CompareTag("ArmAbDerIz"))
                {
                    if (Input.GetButtonDown("Fire1")) //si apreto y...
                    {
                        _hit.transform.GetComponent<Animation>().Play("OpenRightLeft");
                        _hit.collider.isTrigger = true;
                        _hit.transform.gameObject.tag = "ArmCiIzDer";
                    }
                }


            }
            
            else //si no estoy a distancia
            {
                
                if (!_hit.transform.CompareTag("Cogido") && dist < 2f) Debug.Log("no es Cogible");
                if (dist > 2) Debug.Log("muy lejos");
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


//_hit.transform.gameObject.GetComponent<Rotate>().ChangeSpin();
//_hit.transform.localPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z+1f);
//_hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
