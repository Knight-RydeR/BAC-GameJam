using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class StateManagement : MonoBehaviour
{
    [SerializeField] private Animator gameplayCanvas;
    [SerializeField] private Transform initialTransformPlayer;
    [SerializeField] private AudioManager audioScript;
    [SerializeField] private GameObject reversePrompt;
    [SerializeField] private GameObject subtitleOne;
    [SerializeField] private GameObject subtitleTwo;
    [SerializeField] private GameObject subtitleThree;
    [SerializeField] private GameObject stateTwoTexts;
    [SerializeField] private GameObject stateThreeBlood;
    [SerializeField] private GameObject pictureOne;
    [SerializeField] private GameObject pictureTwo;
    [SerializeField] private GameObject pictureThree;
    [SerializeField] private GameObject pictureFour;
    [SerializeField] private GameObject pictureFive;
    [SerializeField] private Volume postProcess;
    [SerializeField] private Material[] horrorMaterials;
    [SerializeField] private Light[] houseLights;
    [SerializeField] private DemonMovement demonScript;
    [SerializeField] private GameObject[] waterPuddles;
    [SerializeField] private GameObject[] knifePositions;
    [SerializeField] private GameObject[] knives;

    private float time = 0;
    private int state = 0;
    private PlayerNavAgent wifey;
    private PlayerMovement player;
    private bool allowTimeToTick;
    private bool reverseButtonPressed;
    private float timeBeforeWin;
    private float elapsedTime;
    private bool ghostEvent;
    bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        ghostEvent = false;
        reverseButtonPressed = false;
        timeBeforeWin = 200f;
        elapsedTime = 0;
        player = FindObjectOfType<PlayerMovement>();
        wifey = FindObjectOfType<PlayerNavAgent>();
        // demonScript = FindObjectOfType<DemonMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        state++;
        StartCoroutine(InitiateStateOne());
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverseButtonPressed && Input.GetKeyDown(KeyCode.R)) {
            // Time.timeScale = 1;
            reverseButtonPressed = true;
            StartCoroutine(reverseTime(5f));
        }

        if (allowTimeToTick) {
            if (timeBeforeWin > 0) {
                timeBeforeWin -= Time.deltaTime;
                elapsedTime += Time.deltaTime;
                if (hasStarted && !ghostEvent && elapsedTime%20 > 0 && elapsedTime%20 < 0.25) {
                    ghostEvent = true;
                    waterPuddles[0].SetActive(true);
                    waterPuddles[1].SetActive(true);
                    waterPuddles[2].SetActive(true);

                    GameObject knifeTemp = knives[Random.Range(0, knives.Length-1)];
                    knifeTemp.transform.position = knifePositions[Random.Range(0, knifePositions.Length-1)].transform.position;
                }
                ghostEvent = false;
            }
            if (timeBeforeWin <= 0 && hasStarted) {
                StartCoroutine(Winning());
            }
        }
    }

    public void showRPrompt() {
        reversePrompt.SetActive(true);
        // Time.timeScale = 0.2f;
        allowTimeToTick = false;
    }

    IEnumerator Winning() {
        gameplayCanvas.SetBool("show", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator reverseTime(float duration) {
        reversePrompt.SetActive(false);
        allowTimeToTick = false;
        gameplayCanvas.SetBool("show", false);
        if (state != 3) {
            gameplayCanvas.SetBool("showClock", true);
        }
        // gameplayCanvas.SetBool("showClock", true);
        audioScript.timeReversalSound();
        // yield return new WaitForSeconds(1f);
        audioScript.clockSound();
        if (postProcess.profile.TryGet(out LensDistortion distortion)) {
            while (time < duration)
            {
                distortion.intensity.value = Mathf.Lerp(distortion.intensity.value, -1, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            distortion.intensity.value = -1;
        }
        yield return new WaitForSeconds(3.5f);
        waterPuddles[0].SetActive(true);
        waterPuddles[1].SetActive(true);
        waterPuddles[2].SetActive(true);
        // player.resetAfterReversal();
        // wifey.resetWifey();
        distortion.intensity.value = 0f;
        time = 0;
        if (state == 1) {
            player.resetAfterReversal();
            wifey.resetWifey();
            state++;
            StartCoroutine(InitiateStateTwo());
        } else if (state == 2) {
            player.resetAfterReversal();
            wifey.resetWifey();
            state++;
            StartCoroutine(InitiateStateThree());
        } else {
            wifey.resetWifey();
            StartCoroutine(TheEnd());
        }
        
    }

    IEnumerator InitiateStateOne() {
        audioScript.stateOneStatement();
        subtitleOne.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameplayCanvas.SetBool("show", true);
        // gameplayCanvas.SetBool("showClock", true);
        allowTimeToTick = true;
        yield return new WaitForSeconds(4.5f);
        hasStarted = true;
        subtitleOne.SetActive(false);
        reverseButtonPressed = false;
    }

    IEnumerator InitiateStateTwo() {
        stateTwoTexts.SetActive(true);
        audioScript.stateTwoStatement();
        subtitleTwo.SetActive(true);
        yield return new WaitForSeconds(3f);
        gameplayCanvas.SetBool("show", true);
        gameplayCanvas.SetBool("showClock", false);
        allowTimeToTick = true;
        yield return new WaitForSeconds(4f);
        wifey.startAgainAfterReverse();
        subtitleTwo.SetActive(false);
        reverseButtonPressed = false;
    }

    IEnumerator InitiateStateThree() {
        audioScript.getLaugh();
        Material[] temp;

        temp = pictureOne.GetComponent<Renderer>().materials;
        temp[0] = horrorMaterials[0];
        pictureOne.GetComponent<Renderer>().materials = temp;

        temp = pictureTwo.GetComponent<Renderer>().materials;
        temp[0] = horrorMaterials[1];
        pictureTwo.GetComponent<Renderer>().materials = temp;

        temp = pictureThree.GetComponent<Renderer>().materials;
        temp[0] = horrorMaterials[2];
        pictureThree.GetComponent<Renderer>().materials = temp;

        temp = pictureFour.GetComponent<Renderer>().materials;
        temp[0] = horrorMaterials[3];
        pictureFour.GetComponent<Renderer>().materials = temp;

        temp = pictureFive.GetComponent<Renderer>().materials;
        temp[0] = horrorMaterials[4];
        pictureFive.GetComponent<Renderer>().materials = temp;

        houseLights[0].color = Color.red;
        houseLights[1].color = Color.red;
        houseLights[2].color = Color.red;
        houseLights[3].color = Color.red;
        houseLights[4].color = Color.red;
        houseLights[5].color = Color.red;
        houseLights[6].color = Color.red;
        houseLights[7].color = Color.red;
        stateThreeBlood.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        demonScript.gameObject.SetActive(true);
        demonScript.startDemonPath();
        audioScript.beatingHeart();
        gameplayCanvas.SetBool("show", true);
        gameplayCanvas.SetBool("showClock", false);
        allowTimeToTick = true;
        wifey.startAgainAfterReverse();
        reverseButtonPressed = false;
        // yield return new WaitForSeconds(3.5f);
    }

    IEnumerator TheEnd() {
        allowTimeToTick = false;
        audioScript.stopHeartBeat();
        audioScript.stateThreeStatement();
        subtitleThree.SetActive(true);
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Menu");
    }
}
