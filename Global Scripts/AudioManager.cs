using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
   public AudioSource[] sounds;
   public static AudioManager instance;
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        

        
    }

   public void Play(int id){
        sounds[id].Play();
   }

}
