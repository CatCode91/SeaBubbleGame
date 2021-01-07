using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchFun : MonoBehaviour, IPointerClickHandler
{
    public AudioClip[] Sounds;
    private AudioSource _audio;

    public void OnPointerClick(PointerEventData eventData)
    {
        _audio.PlayOneShot(Sounds[Random.Range(0, Sounds.Length)]);
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(70, 0 ,0), 4);
    }

    public void OnMouseDown()
    {
        _audio.PlayOneShot(Sounds[Random.Range(0, Sounds.Length)]);
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(70, 0, 0), 4);
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio = AudioManager.instance.Audio;
    }

}
