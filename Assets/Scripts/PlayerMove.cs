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
    public float distanceOfRaycast = 10f;
    public GameObject myHand;
    private Transform padrecito;
    private RaycastHit _hit;
    private bool selected = false;
    private RaycastHit lastHit;

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



        if (Input.GetButtonDown("Fire1") && selected)
        {
            Debug.Log("lo suelto");
            lastHit.transform.SetParent(padrecito);
            lastHit.collider.isTrigger = false;
            lastHit.rigidbody.useGravity = true;
            selected = false;
        }

        else
        {
            if (Physics.Raycast(ray, out _hit, distanceOfRaycast))
            {
                dist = Vector3.Distance(_hit.transform.position, transform.position);
                if (dist < 3f) //si el objeto esta al alcance
                {
                    if (_hit.transform.CompareTag("Cogido")) //si el objeto es cogible
                    {
                        /*initMaterial = _hit.transform.GetComponent<Renderer>().material; //guarda material inicial
                        _hit.transform.GetComponent<Renderer>().material = cogido; //pintalo amarillo*/
                        Debug.Log("cogible");
                        if (Input.GetButtonDown("Fire1") && !selected) //si apreto y no está seleccionado
                        {
                            Debug.Log("lo cojo");
                            padrecito = _hit.transform.parent.gameObject.transform;
                            _hit.transform.SetParent(myHand.transform);
                            _hit.collider.isTrigger = true;
                            _hit.rigidbody.useGravity = false;
                            _hit.transform.localPosition = new Vector3(0f, 0f, 0f);
                            lastHit = _hit;
                            selected = true;
                        }
                    }
                    else //tratamiento animaciones puertas y cajones
                    {
                        AnimationTreatment(_hit);
                    }
                }
                else //si no estoy a distancia
                {
                    if (!_hit.transform.CompareTag("Cogido") && dist < 2f) Debug.Log("no es Cogible");
                    if (dist > 2) Debug.Log("muy lejos");
                }
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

    void AnimationTreatment( RaycastHit _hit)
    {
        if (_hit.transform.CompareTag("ArmAbIzDer"))
        {
            if (Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenLeftRight");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiDerIz";
            }
        }
        else if (_hit.transform.CompareTag("ArmAbDerIz"))
        {
            if (Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenRightLeft");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiDerIz"))
        {
            if (Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseRightLeft");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiIzDer"))
        {
            if (Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseLeftRight");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbDerIz";
            }
        }
        else if (_hit.transform.CompareTag("OpenKitchen"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("OpenKitchenDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "CloseKitchen";
            }
        }
        else if (_hit.transform.CompareTag("CloseKitchen"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("CloseKitchenDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "OpenKitchen";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroom"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("OpenBedroomDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "CloseBedroom";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroom"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("CloseBedroomDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "OpenBedroom";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroomLeft"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("OpenBedSideDrawerLeft");
                _hit.transform.gameObject.tag = "CloseBedroomLeft";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroomRight"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("OpenBedSideDrawerRight");
                _hit.transform.gameObject.tag = "CloseBedroomRight";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroomLeft"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("CloseBedSideDrawerLeft");
                _hit.transform.gameObject.tag = "OpenBedroomLeft";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroomRight"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("CloseBedSideDrawerRight");
                _hit.transform.gameObject.tag = "OpenBedroomRight";
            }
        }
    }

}


//_hit.transform.gameObject.GetComponent<Rotate>().ChangeSpin();
//_hit.transform.localPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z+1f);
//_hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
