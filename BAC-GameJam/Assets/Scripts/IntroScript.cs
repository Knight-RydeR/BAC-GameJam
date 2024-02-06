using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private Animator MobiusLogoAnimator;
    [SerializeField] private Animator mobileAnimator;
    [SerializeField] private AudioManager audioScript;
    [SerializeField] private TMPro.TMP_Text subtitles;
    [SerializeField] private TMPro.TMP_Text warning;
    [SerializeField] private GameObject mobile;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject mobileLight;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Vector3 initialCameraPos;
    [SerializeField] private Vector3 finalCameraPos;
    [SerializeField] private Vector3 newCameraPos;
    [SerializeField] private GameObject globalVol;

    // Start is called before the first frame update
    void Start()
    {
        mainCam.transform.position = initialCameraPos;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(introductionAndMenu());
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator introductionAndMenu() {
        MobiusLogoAnimator.SetBool("show", true);
        yield return new WaitForSeconds(4f);
        subtitles.text = "I can give you the power to save her!";
        audioScript.powerIntroSound();
        warning.gameObject.SetActive(true);
        subtitles.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        warning.gameObject.SetActive(false);
        subtitles.gameObject.SetActive(false);
        // originalBackgroundColor = canvas.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        // originalBackgroundColor.a = 0f;
        // Color newColorWithoutAlpha = new(21f, 23f, 28f, 0f);
        // canvas.transform.GetChild(0).gameObject.GetComponent<Image>().color = originalBackgroundColor;
        // canvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        // mobile.SetActive(true);
        // mobileAnimator.SetBool("show", true);
        // canvas.transform.GetChild(0).gameObject.GetComponent<Image>().color = newColorWithoutAlpha;
        lights.SetActive(true);
        audioScript.menuLightSound();
        audioScript.menuPianoSound();
        yield return new WaitForSeconds(2f);
        float time = 0;
        Vector3 startPosition = mainCam.transform.position;
        while (time < 3)
        {
            mainCam.transform.position = Vector3.Lerp(startPosition, newCameraPos, time / 3);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.transform.position = newCameraPos;
        yield return new WaitForSeconds(1f);
        globalVol.GetComponent<Animator>().Play("MenuVolumeShow");
        yield return new WaitForSeconds(1f);
        mobileLight.SetActive(false);
        mainMenuCanvas.GetComponent<Animator>().Play("menuShowMobile");

    }

    public void onButtonClicked() {
        audioScript.buttonPressIntroSound();
        StartCoroutine(moveToGame());
    }

    public void onExitClicked() {
        Application.Quit();
    }

    IEnumerator moveToGame() {
        // mobileAnimator.SetBool("show", false);
        mainMenuCanvas.GetComponent<Animator>().Play("menuHideMobile");
        canvas.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("RevealBackground");
        float time = 0;
        Vector3 startPosition = mainCam.transform.position;
        while (time < 2)
        {
            mainCam.transform.position = Vector3.Lerp(startPosition, finalCameraPos, time / 2);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}


