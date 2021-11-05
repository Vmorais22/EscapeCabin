using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //MOVIMIENTO
    public float speed = 3.5f;
    private float gravity = 10f;
    private CharacterController controller;
    public static bool menuON = false;
    public bool daltonismo = false;

    //SELECCIÓN
    public float distanceOfRaycast = 10f;
    public GameObject myHand;
    private Transform padrecito;
    private RaycastHit _hit;
    public bool selected = false;
    private RaycastHit lastHit;
    private bool rotating = false;

    //DISTINGUIR INTERACCIÓ
    public Material cogido;
    public Material agarrado;
    private Material initMaterial;


    public LayerMask ignore;
    //TECLADO
    private TecladoMorse tecladomorse;
    public GameObject puerta1, puerta2;

    //COLORES
    private Color colorI = Color.red;
    private Color colorC = Color.green;

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
            if (Physics.Raycast(ray, out _hit, distanceOfRaycast, ~ignore))
            {
                dist = Vector3.Distance(_hit.transform.position, transform.position);
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.white;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 20;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                
                if (dist < 3f) //si el objeto esta al alcance
                {
                    if (_hit.transform.CompareTag("Cogido")) //si el objeto es cogible
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorC;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 4;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if ((Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) && !selected) //si apreto y no está seleccionado
                        {

                            if (_hit.transform.name.Contains("Cajon"))
                            {
                                _hit.transform.GetComponent<ObjectInDrawer>().alreadyGrabbed = true;
                            }
                            padrecito = _hit.transform.parent.gameObject.transform;
                            _hit.transform.SetParent(myHand.transform);
                            if(_hit.transform.name != "Cajonkey") _hit.collider.isTrigger = true;
                            _hit.rigidbody.useGravity = false;
                            _hit.transform.localPosition = new Vector3(0f, 0f, 0f);
                            lastHit = _hit;
                            selected = true;
                        }
                    }
                    else if (_hit.transform.CompareTag("LetraTeclado"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if ((Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")))
                        {
                            if (_hit.transform.name == "A" && !tecladomorse.tecla1)
                            {
                                tecladomorse.Primera();
                            }
                            else if (_hit.transform.name == "C" && tecladomorse.tecla1 && !tecladomorse.tecla2)
                            {
                                tecladomorse.Segunda();
                            }
                            else if (_hit.transform.name == "A" && tecladomorse.tecla1 && tecladomorse.tecla2 && !tecladomorse.tecla3)
                            {
                                tecladomorse.Tercera();
                            }
                            else if (_hit.transform.name == "P" && tecladomorse.tecla1 && tecladomorse.tecla2 && tecladomorse.tecla3 && !tecladomorse.tecla4)
                            {
                                tecladomorse.Quarta();
                                puerta1.GetComponent<Animation>().Play();
                                puerta1.GetComponent<AudioSource>().Play();
                                puerta2.GetComponent<Animation>().Play();
                                puerta2.GetComponent<AudioSource>().Play();
                                TimerLogic.stopTimer = true;
                            }
                            else {
                                tecladomorse.Reseteo();
                            }
                        }
                    }
                    else if (_hit.transform.CompareTag("MasLuz"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo")) MenuInGameLogic.ChangeLight(true);
                    }
                    else if (_hit.transform.CompareTag("MenosLuz"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo")) MenuInGameLogic.ChangeLight(false);
                    }
                    else if (_hit.transform.CompareTag("MasVel"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            if (speed < 6.5f)
                            {
                                speed += 1f;
                                MenuInGameLogic.ChangeSpeed(true);
                            }
                        }
                    }
                    else if (_hit.transform.CompareTag("MenosVel"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            if (speed > 3.5f)
                            {
                                speed -= 1f;
                                MenuInGameLogic.ChangeSpeed(false);
                            }
                        }
                    }
                    else if (_hit.transform.CompareTag("Daltonismo"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            daltonismo = !daltonismo;
                            MenuInGameLogic.Daltonismo(daltonismo);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorRojoI"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorI = Color.red;
                            MenuInGameLogic.ColorI(1);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorVerdeI"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorI = Color.green;
                            MenuInGameLogic.ColorI(2);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorAzulI"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorI = Color.blue;
                            MenuInGameLogic.ColorI(3);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorAmarilloI"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorI = Color.yellow;
                            MenuInGameLogic.ColorI(4);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorLilaI"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorI = Color.magenta;
                            MenuInGameLogic.ColorI(5);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorRojoC"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorC = Color.red;
                            MenuInGameLogic.ColorC(1);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorVerdeC"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorC = Color.green;
                            MenuInGameLogic.ColorC(2);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorAzulC"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorC = Color.blue;
                            MenuInGameLogic.ColorC(3);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorAmarilloC"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorC = Color.yellow;
                            MenuInGameLogic.ColorC(4);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorLilaC"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if (Input.GetButtonDown("TriggerAbajo"))
                        {
                            colorC = Color.magenta;
                            MenuInGameLogic.ColorC(5);
                        }
                    }
                    else if (_hit.transform.CompareTag("ColorSofa"))
                    {
                        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
                        if (daltonismo)
                        {
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
                        }
                        if ((Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))) _hit.transform.GetComponent<SofaBehaviour>().changeSofaColor();

                    }
                    else //tratamiento animaciones puertas y cajones
                    {
                        AnimationTreatment(_hit);
                    }
                }
            }

        }

        if(!menuON)PlayerMovement();
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
            if ((velocity.x != 0 || velocity.z!= 0) && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            else if ((velocity.x == 0 && velocity.z == 0) && GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Pause();
            controller.Move(velocity * Time.deltaTime);
        }

    }

    void AnimationTreatment(RaycastHit _hit)
    {
        if (_hit.transform.CompareTag("ArmAbIzDer"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenLeftRight");
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiDerIz";
            }
        }
        else if (_hit.transform.CompareTag("ArmAbDerIz"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("OpenRightLeft");
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmCiIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiDerIz"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseRightLeft");
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbIzDer";
            }
        }
        else if (_hit.transform.CompareTag("ArmCiIzDer"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1")) //si apreto y...
            {
                _hit.transform.GetComponent<Animation>().Play("CloseLeftRight");
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.collider.isTrigger = true;
                _hit.transform.gameObject.tag = "ArmAbDerIz";
            }
        }
        else if (_hit.transform.CompareTag("OpenKitchen"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("OpenKitchenDrawer" + drawNum.ToString());
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.transform.gameObject.tag = "CloseKitchen";
            }
        }
        else if (_hit.transform.CompareTag("CloseKitchen"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber - 5;
                _hit.transform.GetComponent<Animation>().Play("CloseKitchenDrawer" + drawNum.ToString());
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.transform.gameObject.tag = "OpenKitchen";
            }
        }
        else if (_hit.transform.CompareTag("OpenBedroom"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("OpenBedroomDrawer" + drawNum.ToString());
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.transform.gameObject.tag = "CloseBedroom";
            }
        }
        else if (_hit.transform.CompareTag("CloseBedroom"))
        {
            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = colorI;
            if (daltonismo)
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().reticleSegments = 3;
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().CreateReticleVertices();
            }
            if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
            {
                int drawNum = _hit.transform.GetComponent<MoveableObject>().objectNumber;
                _hit.transform.GetComponent<Animation>().Play("CloseBedroomDrawer" + drawNum.ToString());
                _hit.transform.GetComponent<AudioSource>().Play();
                _hit.transform.gameObject.tag = "OpenBedroom";
            }
        }
    }

}
