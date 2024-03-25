using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip mainMenu;

    [Header("SFX")]
    public AudioClip confirm;
    public AudioClip deny;

    public void PlayMusic(string musicName) {
        switch(musicName) {
            case "Main Menu":
                // Handle main menu music
                Debug.Log("Main Menu music selected.");
                musicSource.clip = mainMenu;
                StartCoroutine(FadeMusic(musicSource, 2f, 1f));
                break;
            default:
                // Handle default case (when musicName does not match any of the above cases)
                Debug.LogWarning("Unknown music name: " + musicName);
                break;
        }
    }

    public void PlaySFX(string soundBite) {
        switch(soundBite) {
            case "confirm":
                Debug.Log("Confirm SFX play");
                SFXSource.PlayOneShot(confirm);
                break;
            default:
                // Handle default case (when musicName does not match any of the above cases)
                Debug.LogWarning("Unknown music name: " + soundBite);
                break;
        }
    }

    public static IEnumerator FadeMusic(AudioSource music, float duration, float targetVol){
        music.Play();
        float currentTime = 0;
        float start = music.volume;

        while(currentTime < duration){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, targetVol, currentTime/duration);
            yield return null;
        }
        yield break;
    }
}
