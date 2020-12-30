using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private SceneSwitcher _fader;

    // Start is called before the first frame update
    void Start()
    {
        _fader = FindObjectOfType<SceneSwitcher>();
    }

    public void StartGame() 
    {
        _fader.SwitchScene("Game");
    }
}
