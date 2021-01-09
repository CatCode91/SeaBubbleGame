using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private TimeUI _time;
    private ScoreUI _score;
    private Bubble _bubble;
    private AudioSource _audio;
    
    //������ ������ ��� �������� �������, ���� �� �� ���-�� ���������, �� ������ ������-������ game object...�� �� ���� �� ���
    [SerializeField]
    private float _screenSize = 10.5f;

    //����� �� ���� �� �������
    public AudioClip[] BurstSounds;

    void Start()
    {
        _audio = AudioManager.instance.Audio;
        _time = GetComponentInChildren<TimeUI>();
        _score = GetComponentInChildren<ScoreUI>();
        _time.TimeIsOver += TimeIsOvered;

        CreateBubble();
        StartCoroutine(CreateBubbles());

        _audio.clip = AudioManager.instance.Game;
        _audio.Play();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _audio.Stop();
                _audio.clip = AudioManager.instance.Menu;
                _audio.Play();
                SceneSwitcher.instance.SwitchScene("Menu");
                return;
            }
        }
    }

    public void AddScore(Bubble bubble) 
    {
        _score.AddScore(bubble.Score);
    }

    private IEnumerator CreateBubbles()
    {
         while (_time.LevelTime.ToString("##") != "1")
        {
            yield return new WaitForSeconds(2f);
            CreateBubble();
        }
    }

    private void CreateBubble()
    {
        _bubble = Instantiate((GameObject)Resources.Load("Bubble")).GetComponent<Bubble>();
        float widthLimit = _screenSize - _bubble.Size;
        _bubble.transform.position = new Vector3(Random.Range(widthLimit, -widthLimit), 0.75f, -21f);
        _bubble.Burst += BubleBurst;

        //����������� �������� ������, ���� �� ����� ������ ������ � ������
        if (_time.LevelTime < 23) 
        {
            _bubble.MakeFaster(420 / _time.LevelTime);
        }
    }

    private void BubleBurst(Bubble arg0)
    {
        if (_time.LevelTime >= 1f) 
        {
            _audio.PlayOneShot(BurstSounds[Random.Range(0, BurstSounds.Length)]);
            _score.AddScore(arg0.Score);
            CreateBubble();
        }

        Destroy(arg0.gameObject);
    }

    private void TimeIsOvered()
    {
        SceneSwitcher.instance.SwitchScene("Rates");
        Player.instance.AddScore(_score.Score);
    }
}

