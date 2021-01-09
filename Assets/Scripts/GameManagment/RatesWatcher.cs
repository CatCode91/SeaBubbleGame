using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatesWatcher : MonoBehaviour
{
    private bool _isWork = true;
    private int _rate;
    private int _tempRate = 0;
    private TextMeshProUGUI _textRate;
    private Animation _animation;
    private AudioSource _audio;

    public AudioClip count;
    public AudioClip end;


    // Start is called before the first frame update
    void Start()
    {
        _audio = AudioManager.instance.Audio;
        _audio.clip = count;
        _audio.Play();
        _rate = Player.instance.LastScore;
        _textRate = GetComponentInChildren<TextMeshProUGUI>();
        _animation = GetComponentInChildren<Button>().GetComponent<Animation>();
    }

    void Update()
    {
        //if running on Android, check for Menu/Home and exit
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GoToMainMenu();
                return;
            }
        }


        if (_isWork) 
        {
            if (_tempRate != _rate)
            {
                if (_rate - _tempRate > 1000)
                {
                    _tempRate += 100;
                }

                if (_rate - _tempRate > 100)
                {
                    _tempRate += 10;
                }

                _tempRate += 1;
                _textRate.text = _tempRate.ToString();
            }

            if (_tempRate == _rate) 
            {
                _isWork = false;
                var s = CongratulationsShower(_rate);
                Text[] texts = GetComponentsInChildren<Text>();
                texts[0].text = s.Item1;
                texts[1].text = s.Item2;
                _audio.Stop();
                _audio.PlayOneShot(end);
                _animation.Play();
          } 
        }
      }

    private Tuple<string, string> CongratulationsShower(int rate) 
    {
        if (rate > 600)
        {
            return new Tuple<string, string>("Хммм...", "А вы точно не читер? :)");
        }

        if (rate > 500)
        {
            return new Tuple<string, string>("Капец!", "Ваши руки не для скуки :)");
        }

        if (rate > 400)
        {
            return new Tuple<string, string>("Невероятно!", "У вас потрясающая реакция!");
        }

        if (rate > 350)
        {
            return new Tuple<string, string>("Уау! Круто!", "У вас очень быстрые пальцы!");
        }

        if (rate > 300)
        {
            return new Tuple<string, string>("Молодец!", "Ты супермегабыстрый!");
        }

        if (rate > 200)
        {
            return new Tuple<string, string>("Неплохо!", "...но можно и лучше)");
        }

        else 
        {
            return new Tuple<string, string>("Слабовато...", "Попробуй еще разок!");
        }
    }

    public void RestartGame() 
    {
        SceneSwitcher.instance.SwitchScene("Game");
    }

    public void GoToMainMenu() 
    {
        _audio.Stop();
        _audio.clip = AudioManager.instance.Menu;
        _audio.Play();
        SceneSwitcher.instance.SwitchScene("Menu");
    }
}
