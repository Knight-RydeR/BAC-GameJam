using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource powerIntro;
    [SerializeField] private AudioSource buttonPressIntro;
    [SerializeField] private AudioSource stateOneAudio;
    [SerializeField] private AudioSource stateTwoAudio;
    [SerializeField] private AudioSource stateThreeAudio;
    [SerializeField] private AudioSource scaryLaugh;
    [SerializeField] private AudioSource tickTockClock;
    [SerializeField] private AudioSource diavoloReversal;
    [SerializeField] private AudioSource heartbeating;
    [SerializeField] private AudioSource wifeScream;
    [SerializeField] private AudioSource waterSplashSound;
    [SerializeField] private AudioSource[] wifeSound;


    public void playDoorOpenSound() {
        doorOpen.time = 0.45f;
        doorOpen.Play();
    }

    public void playDoorCloseSound() {
        doorClose.time = 0.10f;
        doorClose.Play();
    }

    public void powerIntroSound() {
        powerIntro.Play();
    }

    public void buttonPressIntroSound() {
        buttonPressIntro.Play();
    }

    public void stateOneStatement() {
        stateOneAudio.Play();
    }

    public void stateTwoStatement() {
        stateTwoAudio.Play();
    }

    public void stateThreeStatement() {
        stateThreeAudio.Play();
    }

    public void getWifeSound() {
        AudioSource temp = wifeSound[Random.Range(0, 2)];
        temp.time = 0.6f;
        temp.Play();
    }

    public void getLaugh() {
        scaryLaugh.Play();
    }

    public void clockSound() {
        tickTockClock.Play();
    }

    public void timeReversalSound() {
        diavoloReversal.time = 1.8f;
        diavoloReversal.Play();
    }

    public void beatingHeart() {
        heartbeating.Play();
    }

    public void stopHeartBeat() {
        heartbeating.Stop();
    }

    public void wifeScreamSound() {
        wifeScream.time = 0.3f;
        wifeScream.Play();
    }

    public void waterSplash() {
        waterSplashSound.Play();
    }
}
