using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {

    public static AudioSource PlaySound(AudioClip sound) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
        return audioSource;
    }
}
