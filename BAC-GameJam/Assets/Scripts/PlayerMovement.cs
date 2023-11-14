using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float  speed;
    private Vector3 initialTransform;

    void Start()
    {
        initialTransform = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) {
            gameObject.transform.position = gameObject.transform.position + mainCamera.transform.forward * (speed * Time.deltaTime);
            // gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, mainCamera.transform.rotation, speed * Time.deltaTime);
        } 
        if (Input.GetKey(KeyCode.S)) {
            gameObject.transform.position = gameObject.transform.position - (mainCamera.transform.forward * (speed/5 * Time.deltaTime));
        } 
        if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.position = gameObject.transform.position - mainCamera.transform.right * (speed/2 * Time.deltaTime);
        } 
        if (Input.GetKey(KeyCode.D)) {
            gameObject.transform.position = gameObject.transform.position + mainCamera.transform.right * (speed/2 * Time.deltaTime);
        }
    }

    public void resetAfterReversal() {
        gameObject.transform.position = initialTransform;
    }
}
