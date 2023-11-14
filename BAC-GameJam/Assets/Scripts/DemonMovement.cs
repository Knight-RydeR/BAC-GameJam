using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonMovement : MonoBehaviour
{
    private Animator demonAnim;
    private NavMeshAgent demonAgent;
    [SerializeField] private GameObject wife;
    [SerializeField] private GameObject eye;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject tendrils;
    [SerializeField] private GameObject particles;


    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        demonAnim = GetComponent<Animator>();
        demonAgent = GetComponent<NavMeshAgent>();
        // demonAgent.destination = wife.transform.position;
        // target = wife.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!demonAgent.hasPath) {
            Debug.Log("here i am");
            demonAgent.SetDestination(wife.transform.position);
            target = wife.transform.position;
        }

        // if (Input.GetKeyDown(KeyCode.Z)) {
        //     splitRevealDemon();
        // }
    }

    public void startDemonPath() {
        // gameObject.SetActive(true);
        eye.GetComponent<SkinnedMeshRenderer>().enabled = false;
        body.GetComponent<SkinnedMeshRenderer>().enabled = false;
        tendrils.GetComponent<SkinnedMeshRenderer>().enabled = false;
        particles.SetActive(false);
        demonAgent.destination = wife.transform.position;
        target = wife.transform.position;
    }

    public void splitRevealDemon() {
        StartCoroutine(RevealDemonEvent());
    }

    IEnumerator RevealDemonEvent() {
        eye.GetComponent<SkinnedMeshRenderer>().enabled = true;
        body.GetComponent<SkinnedMeshRenderer>().enabled = true;
        tendrils.GetComponent<SkinnedMeshRenderer>().enabled = true;
        particles.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        eye.GetComponent<SkinnedMeshRenderer>().enabled = false;
        body.GetComponent<SkinnedMeshRenderer>().enabled = false;
        tendrils.GetComponent<SkinnedMeshRenderer>().enabled = false;
        particles.SetActive(false);
    }
}
