using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {

    
    private static Dictionary<int, AudioSource> lookup = new Dictionary<int, AudioSource>();
    
    public static int PlaySound(AudioClip sound) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
        lookup.Add(audioSource.GetInstanceID(), audioSource);
        return audioSource.GetInstanceID();
    }

    public static void StopSound(int id) {
        AudioSource audioSource;
        if (lookup.TryGetValue(id, out audioSource)) {
            audioSource.Stop();
            lookup.Remove(id);
        }
    }
}
