using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame() 
    {
        SceneSwitcher.instance.SwitchScene("Game");
    }
}
