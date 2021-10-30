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
    private bool rotating = false;

    //DISTINGUIR INTERACCIÓ
    public Material cogido;
    public Material agarrado;
    private Material initMaterial;

    //OTRAS
    private bool pintado = false;

    //TECLADO
    private TecladoMorse tecladomorse;
    public GameObject puerta1, puerta2;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        tecladomorse = GameObject.FindGameObjectWithTag("TagTecladoMorse").GetComponent<TecladoMorse>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        var dist = 1000f;



        if (selected)
        {
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                rotating = false;
                Debug.Log("lo suelto");
                lastHit.transform.SetParent(padrecito);
                lastHit.collider.isTrigger = false;
                lastHit.rigidbody.useGravity = true;
                selected = false;
            }
            else if (Input.GetButton("TriggerArriba") || Input.GetButton("Fire2"))
            {
                rotating = true;
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                lastHit.transform.Rotate(0, horizontal * 3, vertical * 3);
            }
            else rotating = false;

        }

        else
        {
            if (Physics.Raycast(ray, out _hit, distanceOfRaycast))
            {
                dist = Vector3.Distance(_hit.transform.position, transform.position);
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.white;
                if (dist < 3f) //si el objeto esta al alcance
                {
                    if (_hit.transform.CompareTag("Cogido")) //si el objeto es cogible
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.green;
                        Debug.Log("cogible");
                        if ((Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) && !selected) //si apreto y no está seleccionado
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
                    else if (_hit.transform.CompareTag("LetraTeclado"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                        if ((Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")))
                        {
                            if (_hit.transform.name == "A" && !tecladomorse.tecla1)
                            {
                                Debug.Log("Correcto");
                                tecladomorse.Primera();
                                Debug.Log(tecladomorse.tecla1);
                            }
                            else if (_hit.transform.name == "C" && tecladomorse.tecla1 && !tecladomorse.tecla2)
                            {
                                Debug.Log("Correcto");
                                tecladomorse.Segunda();
                            }
                            else if (_hit.transform.name == "A" && tecladomorse.tecla1 && tecladomorse.tecla2 && !tecladomorse.tecla3)
                            {
                                Debug.Log("Correcto");
                                tecladomorse.Tercera();
                            }
                            else if (_hit.transform.name == "P" && tecladomorse.tecla1 && tecladomorse.tecla2 && tecladomorse.tecla3 && !tecladomorse.tecla4)
                            {
                                Debug.Log("Correcto");
                                tecladomorse.Quarta();
                                Debug.Log("CodigoCorrecto");
                                puerta1.GetComponent<Animation>().Play();
                                puerta2.GetComponent<Animation>().Play();
                            }
                            else {
                                Debug.Log("Reseteo");
                                tecladomorse.Reseteo();
                            }
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
        if (!rotating)
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

    void AnimationTreatment(RaycastHit _hit)
    {
        if (_hit.transform.CompareTag("ArmAbIzDer"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenLeftRight");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiDerIz";
            }
        }
        else if (_hit.transform.CompareTag("ArmAbDerIz"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenRightLeft");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiDerIz"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseRightLeft");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiIzDer"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseLeftRight");
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbDerIz";
            }
        }
        else if (_hit.transform.CompareTag("OpenKitchen"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("OpenKitchenDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "CloseKitchen";
            }
        }
        else if (_hit.transform.CompareTag("CloseKitchen"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("CloseKitchenDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "OpenKitchen";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroom"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("OpenBedroomDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "CloseBedroom";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroom"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("CloseBedroomDrawer" + drawNum.ToString());
                _hit.transform.gameObject.tag = "OpenBedroom";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroomLeft"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("OpenBedSideDrawerLeft");
                _hit.transform.gameObject.tag = "CloseBedroomLeft";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroomRight"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("OpenBedSideDrawerRight");
                _hit.transform.gameObject.tag = "CloseBedroomRight";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroomLeft"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo"))
            {
                _hit.transform.GetComponent<Animation>().Play("CloseBedSideDrawerLeft");
                _hit.transform.gameObject.tag = "OpenBedroomLeft";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroomRight"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                _hit.transform.GetComponent<Animation>().Play("CloseBedSideDrawerRight");
                _hit.transform.gameObject.tag = "OpenBedroomRight";
            }
        }
    }

}
