using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using LTA.Data;

public class SoundController : MonoBehaviour
{
    //Dictionary<string, AudioClip> dic_Sound = new Dictionary<string, AudioClip>();

    private AudioSource audioSource;
    
    //private AudioSource audioSourceEffect;

    public bool IsOn
    {
        get {
            return PlayerPrefs.GetInt("IsOnSFX", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("IsOnSFX", value ? 1 : 0);
        }
    }

    public bool IsOnMusic
    {
        get
        {
            return PlayerPrefs.GetInt("IsOnMusic", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("IsOnMusic", value ? 1 : 0);
            if (!value)
            {
                audioSource.Stop();
                return;
            }
            audioSource.Play();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        //audioSourceEffect = transform.GetChild(0).GetComponent<AudioSource>();
        //AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sound");
        //foreach (AudioClip audioClip in audioClips)
        //{
        //    dic_Sound.Add(audioClip.name, audioClip);
        //}
    }



    public void PlayMusic(string key, bool isloop = true)
    {
        audioSource.Stop();
        AudioClip audioClip = AssetHelper.AssetManager.GetAsset<AudioClip>("Sound/" + key);
        audioSource.loop = isloop;
        audioSource.clip = audioClip;
        if (IsOnMusic)
            audioSource.Play();

    }

    public void StopMusic()
    {
        audioSource.Stop();
    }


    public void PlaySound(string key , float volum = 1)
    {
        
        if (IsOn)
        {
            AudioClip audioClip = AssetHelper.AssetManager.GetAsset<AudioClip>("Sound/" + key);
            if (audioClip != null)
            {
                audioSource.volume = volum;
                audioSource.PlayOneShot(audioClip);
            }
            else
                Debug.LogError("error key sound " + key);

        }
    }

}

public class MySound : SingletonMonoBehaviour<SoundController>
{

}


