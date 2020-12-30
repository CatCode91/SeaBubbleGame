using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneSwitcher : MonoBehaviour
    {
        private Animation _animation;
        private ArrayList _states;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _animation = GetComponentInChildren<Animation>(true);
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            _animation.Play("FadeOut");
            StartCoroutine("ActiveDelay");
        }

        internal void SwitchScene(string scene)
        {
            _animation.gameObject.SetActive(true);
            _animation.Play("FadeIn");
            StartCoroutine(LoadWithDelay(scene));
        }

        private IEnumerator LoadWithDelay(string scene)
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(scene);
        }

        private IEnumerator ActiveDelay()
        {
            yield return new WaitForSeconds(1);
            _animation.gameObject.SetActive(false);
        }

    }
}
