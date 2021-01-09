using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    //синглтон для переключения сцен с анимацией затухания

    public class SceneSwitcher : MonoBehaviour
    {
        private Animation _animation;

        public static SceneSwitcher instance = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


            _animation = GetComponentInChildren<Animation>(true);
          
        }

        public void SwitchScene(string scene)
        {
            _animation.gameObject.SetActive(true);
            _animation.Play("FadeIn");
            StartCoroutine(LoadWithDelay(scene));
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            _animation.Play("FadeOut");
            StartCoroutine("ActiveDelay");
        }

        private IEnumerator LoadWithDelay(string scene)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(scene);
        }

        private IEnumerator ActiveDelay()
        {
            yield return new WaitForSeconds(1);
            _animation.gameObject.SetActive(false);
        }
    }
}
