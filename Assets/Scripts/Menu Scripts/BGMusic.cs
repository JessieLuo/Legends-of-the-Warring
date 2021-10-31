using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private AudioSource bgMusic;
    private float musicVolume = 0.1f;

    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
    }

    // Creates a list of objects with the "Music" tag and detroys them if theres more than 1.
    // Allows one Music object to persist through scenes.
    void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");
        if (objects.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Updates the volume based on the slider value.
    void Update()
    {
        bgMusic.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
