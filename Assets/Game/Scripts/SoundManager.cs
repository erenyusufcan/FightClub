using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;
   public AudioSource victorysound;
   public AudioSource throwsound1;
   public AudioSource throwsound2;
   public AudioSource damagesound;
   public AudioSource laughsound;

    public void Awake()
    {
        instance = this;
    }
    public void VictoryEfect()
    {
        victorysound.Play();
    }
    public void ThrowEffect1()
    {
        throwsound1.Play();
    }
    public void ThrowEffect2()
    {
        throwsound2.Play();
    }
    public void DamageEffect()
    {
        damagesound.Play(); 
    }
    public void LaughEffect()
    {
        laughsound.Play();
    }
}
