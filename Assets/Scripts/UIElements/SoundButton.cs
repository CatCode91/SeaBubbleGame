using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

//для кнопки управления звуком в игре

public class SoundButton : MonoBehaviour
{
    private Button _btn;

    public Image  Image;
    public Sprite SoundOn;
    public Sprite SoundOff;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() => AudioManager.instance.ChangeVolume());

        AudioManager.instance.VolumeChanged += VolumeChanged;
       
        if (AudioManager.instance.IsSoundOn)
        {
            Image.sprite = SoundOn;
        }

        else 
        {
            Image.sprite = SoundOff;
        }
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
        AudioManager.instance.VolumeChanged -= VolumeChanged;
    }

    private void VolumeChanged(bool arg0)
    {
        if (arg0)
        {
            Image.sprite = SoundOn;
        }

        else
        {
            Image.sprite = SoundOff;
        }
    }
}
