using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingScript : MonoBehaviour
{

    [SerializeField] private float rayDistance;
    // [SerializeField] private GameObject cam;
    [SerializeField] private GameObject equipItemPlaceholder;
    [SerializeField] private AudioManager audioScript;
    [SerializeField] private GameObject interactText;
    bool hitDoor;
    bool isHoldingItem;
    GameObject itemHeld;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            isHoldingItem = false;
            itemHeld.GetComponent<BoxCollider>().enabled = true;
            itemHeld.GetComponent<Rigidbody>().useGravity = true;
        }

        if (isHoldingItem) {
            itemHeld.transform.position = equipItemPlaceholder.transform.position;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject.name == "Cubic" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");

            } else if (hit.collider.gameObject.CompareTag("Door")) {
                hitDoor = true;
                // interactText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    bool doorState = hit.collider.gameObject.GetComponent<Animator>().GetBool("isDoorOpen");
                    Debug.Log("This doorlock is " + doorState);
                    if (doorState) {
                        audioScript.playDoorOpenSound();
                    } else {
                        audioScript.playDoorCloseSound();
                    }
                    hit.collider.gameObject.GetComponent<Animator>().SetBool("isDoorOpen", !doorState);
                }  
            } else if (!isHoldingItem && hit.collider.gameObject.CompareTag("Broom")) {
                hitDoor = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    itemHeld = hit.collider.gameObject;
                    itemHeld.GetComponent<BoxCollider>().enabled = false;
                    itemHeld.GetComponent<Rigidbody>().useGravity = false;
                    Debug.Log("This broom");
                    isHoldingItem = true;
                }  
            } else if (!isHoldingItem && hit.collider.gameObject.CompareTag("Knife")) {
                hitDoor = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    itemHeld = hit.collider.gameObject;
                    itemHeld.GetComponent<BoxCollider>().enabled = false;
                    // itemHeld.GetComponent<CapsuleCollider>().enabled = false;
                    itemHeld.GetComponent<Rigidbody>().useGravity = false;
                    Debug.Log("This knife");
                    isHoldingItem = true;
                }  
            } else if (isHoldingItem && hit.collider.gameObject.CompareTag("Puddle")) {
                if (itemHeld.CompareTag("Broom")) {
                    hitDoor = true;
                    if (Input.GetKeyDown(KeyCode.E)) {
                        hit.collider.gameObject.SetActive(false);
                        audioScript.waterSplash();
                        Debug.Log("Paani Paani");
                    } 
                }
                
                 
            } else {
                hitDoor = false;
                // hitBroom = false;
                // interactText.SetActive(false);
            }
            // else if (hit.collider.gameObject.CompareTag("Knife") && Input.GetKeyDown(KeyCode.E)) {
            //     bool doorState = hit.collider.gameObject.GetComponent<Animator>().GetBool("isDoorOpen");
            //     Debug.Log("This doorlock is " + doorState);
            //     hit.collider.gameObject.GetComponent<Animator>().SetBool("isDoorOpen", !doorState);
            // }
        } else if (hitDoor == true) {
            hitDoor = false;
        }
        interactText.SetActive(hitDoor);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a X unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = Camera.main.transform.forward * rayDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
