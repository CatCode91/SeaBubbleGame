using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private TimeUI _time;
    private ScoreUI _score;
    private Bubble _bubble;
    private AudioSource _audio;

    public AudioClip[] bursts;

    // Start is called before the first frame update
    void Start()
    {
        _time = GetComponentInChildren<TimeUI>();
        _audio = GetComponentInChildren<AudioSource>();
        _score = GetComponentInChildren<ScoreUI>();
        _time.TimeIsOver += TimeIsOvered;

        CreateBubble();
        StartCoroutine(CreateBubbles());
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
            CreateBubble();
        }
    }

    private void CreateBubble()
    {
        _bubble = Instantiate((GameObject)Resources.Load("Bubble")).GetComponent<Bubble>();
        float widthLimit = 10.75f - _bubble.Size;
        _bubble.transform.position = new Vector3(Random.Range(widthLimit, -widthLimit), 0.75f, -21f);
        _bubble.Burst += bubleBurst;

        if (_time.LevelTime < 25) 
        {
            _bubble.MakeFaster(450 / _time.LevelTime);
        }
    }

    private void bubleBurst(Bubble arg0)
    {
        _audio.PlayOneShot(bursts[Random.Range(0,bursts.Length-1)]);
        _score.AddScore(arg0.Score);
        Destroy(arg0.gameObject);
        CreateBubble();
    }

    private void TimeIsOvered()
    {
        Player.instance.AddScore(_score.Score);
        SceneSwitcher.instance.SwitchScene("Rates");
    }
}

