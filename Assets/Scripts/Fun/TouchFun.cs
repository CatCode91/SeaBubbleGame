using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//чисто по фану, нажали на зверюшку - она издает звук)
public class TouchFun : MonoBehaviour, IPointerClickHandler
{
    private AudioSource _audio;

    public AudioClip[] Sounds;

    void Start()
    {
        _audio = AudioManager.instance.Audio;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _audio.PlayOneShot(Sounds[Random.Range(0, Sounds.Length)]);
    }

    public void OnMouseDown()
    {
        _audio.PlayOneShot(Sounds[Random.Range(0, Sounds.Length)]);
    }
}
