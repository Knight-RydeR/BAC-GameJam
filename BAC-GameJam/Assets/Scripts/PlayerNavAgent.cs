using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavAgent : MonoBehaviour
{
    [SerializeField] private List<Transform> movePositionTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private bool allowMovement;
    [SerializeField] private string[] locations;
    [SerializeField] private AudioManager audioScript;

    private Animator wifeAnimator;
    GameObject currentTarget;
    bool hasDied;
    Vector3 initialPosition;
    private StateManagement stateManageScript;

    // Start is called before the first frame update
    void Start()
    {
        hasDied = false;
        stateManageScript = FindObjectOfType<StateManagement>();
        wifeAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        // allowMovement = false;
        initialPosition = gameObject.transform.position;
        StartCoroutine(choosePlaceToWalkToInitial());
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement) {
            wifeAnimator.SetBool("walking", true);
        } else {
            wifeAnimator.SetBool("walking", false);
        }
    }

    public void resetWifey() {
        hasDied = false;
        wifeAnimator.SetBool("died", false);
        allowMovement = false;
        // navMeshAgent.destination = gameObject.transform.position;
        navMeshAgent.SetDestination(initialPosition);
        gameObject.transform.position = initialPosition;
    }

    public void startAgainAfterReverse() {
        StartCoroutine(choosePlaceToWalkToInitial());
    }

    IEnumerator choosePlaceToWalkTo() {
        yield return new WaitForSeconds(2f);
        audioScript.getWifeSound();
        yield return new WaitForSeconds(3f);
        GameObject temp = currentTarget;
        currentTarget = movePositionTransform[Random.Range(0, movePositionTransform.Count-1)].gameObject;
        movePositionTransform.Add(temp.transform);
        movePositionTransform.Remove(currentTarget.transform);
        navMeshAgent.destination = currentTarget.transform.position;
        currentTarget.tag = "Target";
        allowMovement = true;
    }

    IEnumerator choosePlaceToWalkToInitial() {
        yield return new WaitForSeconds(5f);
        currentTarget = movePositionTransform[Random.Range(0, movePositionTransform.Count-1)].gameObject;
        currentTarget.gameObject.tag = "Target";
        movePositionTransform.Remove(currentTarget.transform);
        navMeshAgent.destination = currentTarget.transform.position;
        allowMovement = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Target")) {
            other.gameObject.tag = "Untagged";
            allowMovement = false;
            StartCoroutine(choosePlaceToWalkTo());
            // allowMovement = false;
        } else if (other.gameObject.CompareTag("Door")) {
            if (other.gameObject.GetComponent<Animator>().GetBool("isDoorOpen")) {
                other.gameObject.GetComponent<Animator>().SetBool("isDoorOpen", false);
                audioScript.playDoorOpenSound();
            }
        } 
        // else if (
        //     other.gameObject.CompareTag("Knife") || 
        //     other.gameObject.CompareTag("Stove") || 
        //     other.gameObject.CompareTag("Puddle") || 
        //     other.gameObject.CompareTag("Electric") || 
        //     other.gameObject.CompareTag("Poison")
        //     ) {
        //         allowMovement = false;
        //         navMeshAgent.SetDestination(gameObject.transform.position);
        //         wifeAnimator.SetBool("died", true);
        //         audioScript.wifeScreamSound();
        //         stateManageScript.showRPrompt();
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.CompareTag("Knife") || 
            collision.gameObject.CompareTag("Stove") || 
            collision.gameObject.CompareTag("Puddle") || 
            collision.gameObject.CompareTag("Electric") || 
            collision.gameObject.CompareTag("Poison")
            ) {
                if (!hasDied) {
                    hasDied = true;
                    allowMovement = false;
                    navMeshAgent.SetDestination(gameObject.transform.position);
                    wifeAnimator.SetBool("died", true);
                    audioScript.wifeScreamSound();
                    stateManageScript.showRPrompt();
                }     
        }
    }

    void OnTriggerExit(Collider other)
    {

    }

    public void enableMovementForPlayer() {
        allowMovement = true;
    }
}
