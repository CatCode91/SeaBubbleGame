using Assets.Scripts;
using Assets.Scripts.SettingsModel;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame() 
    {
        SceneSwitcher.instance.SwitchScene("Game");
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape) || (Input.GetKey(KeyCode.Menu)))
            {
                Application.Quit();
                return;
            }
        }
    }
}
