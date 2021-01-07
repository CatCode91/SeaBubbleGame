using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private TimeUI _time;
    private ScoreUI _score;
    private Bubble _bubble;
    private AudioSource _audio;
    
    //ширина экрана для рождения шариков, надо бы на что-то опереться, на ширину какого-нибудь game object...но не знаю на что
    [SerializeField]
    private float _screenSize = 10.5f;

    public AudioClip[] bursts;

    // Start is called before the first frame update
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

    public void AddScore(Bubble bubble) 
    {
        _score.AddScore(bubble.Score);
    }

    public IEnumerator CreateBubbles()
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
        _bubble.Burst += bubleBurst;

        if (_time.LevelTime < 23) 
        {
            _bubble.MakeFaster(420 / _time.LevelTime);
        }
    }

    private void bubleBurst(Bubble arg0)
    {
        if (_time.LevelTime >= 1f) 
        {
            _audio.PlayOneShot(bursts[Random.Range(0, bursts.Length)]);
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

