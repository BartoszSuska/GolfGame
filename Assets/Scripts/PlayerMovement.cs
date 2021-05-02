using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Com.BoarShroom.Golf
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float maxPower;
        [SerializeField] float actualPower;
        [SerializeField] float powerModifier;
        [SerializeField] float aimRotationSpeed;
        [SerializeField] Vector2 fieldOfViewBorders;
        public int numberOfHits;
        float fieldOfView;
        bool outOfBonds;
        bool canHit;
        bool hit;
        Vector3 lastPosition;
        [SerializeField] bool test;

        [SerializeField] Transform aimTransform;
        [SerializeField] Slider sliderPower;
        [SerializeField] GameObject aimArrow;
        [SerializeField] TMP_Text numberOfHitsText;
        Rigidbody rb;
        Transform cam;
        CinemachineFreeLook cinemachine;
        CinemachineCollider cinemachineCollider;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            sliderPower.maxValue = maxPower;
            aimArrow.SetActive(false);
            cinemachine = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineFreeLook>();
            cinemachineCollider = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineCollider>();
            fieldOfView = 60f;
            cinemachine.m_Lens.FieldOfView = 60f;
            lastPosition = transform.position;
        }

        void Update()
        {
            //aimTransform.rotation = Quaternion.RotateTowards(transform.rotation, aimPos, Time.deltaTime * aimRotationSpeed);
            aimTransform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
            //aimArrow.transform.rotation = Quaternion.RotateTowards(aimArrow.transform.rotation, aimTransform.rotation, Time.deltaTime * aimRotationSpeed);
            canHit = false;

            if(rb.velocity.magnitude < 0.05f)
            {
                canHit = true;
            }

            if(!outOfBonds && canHit)
            {
                Zoom();

                if (Input.GetButton("Shoot"))
                {
                    actualPower += powerModifier * Time.fixedDeltaTime;
                    actualPower = Mathf.Min(actualPower, maxPower);
                    aimArrow.SetActive(true);
                }
                else if (Input.GetButtonUp("Shoot"))
                {
                    Launch();
                }
            }

            sliderPower.value = actualPower;

            if(Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                cinemachineCollider.enabled = !cinemachineCollider.enabled;
            }


            if(Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }

            numberOfHitsText.text = numberOfHits.ToString();
        }

        void Launch()
        {

            rb.velocity = Vector3.zero;
            rb.AddForce(aimTransform.forward * actualPower, ForceMode.Impulse);
            actualPower = 0;
            aimArrow.SetActive(false);
            numberOfHits++;
        }

        void Zoom()
        {
            fieldOfView -= Input.mouseScrollDelta.y;

            fieldOfView = Mathf.Clamp(fieldOfView, fieldOfViewBorders.x, fieldOfViewBorders.y);

            cinemachine.m_Lens.FieldOfView = fieldOfView;
        }

        void Respawn()
        {
            transform.position = lastPosition;
            rb.velocity = Vector3.zero;
            outOfBonds = false;
        }

        void OnCollisionEnter(Collision col)
        {
            if(col.transform.tag == "OutOfBonds" && !outOfBonds)
            {
                StartCoroutine(RespawnCoroutine());
            }
        }

        void OnCollisionExit(Collision col)
        {
            if(col.transform.tag == "OutOfBonds")
            {
                outOfBonds = false;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.tag == "End")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        IEnumerator RespawnCoroutine()
        {
            outOfBonds = true;

            yield return new WaitForSeconds(5);

            if(outOfBonds)
            {
                Respawn();
            }
        }
    }
}