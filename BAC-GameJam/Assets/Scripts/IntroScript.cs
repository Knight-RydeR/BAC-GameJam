using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private Animator MobiusLogoAnimator;
    [SerializeField] private Animator mobileAnimator;
    [SerializeField] private AudioManager audioScript;
    [SerializeField] private TMPro.TMP_Text subtitles;
    [SerializeField] private TMPro.TMP_Text warning;
    [SerializeField] private GameObject mobile;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(0.5f);
        mobile.SetActive(true);
        mobileAnimator.SetBool("show", true);
    }

    public void onButtonClicked() {
        StartCoroutine(moveToGame());
    }

    public void onExitClicked() {
        Application.Quit();
    }

    IEnumerator moveToGame() {
        mobileAnimator.SetBool("show", false);
        audioScript.buttonPressIntroSound();
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(1);
    }
}


