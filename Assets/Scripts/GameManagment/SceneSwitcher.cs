﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    //синглтон для переключения сцен с анимацией затухания

    public class SceneSwitcher : MonoBehaviour
    {
        public static SceneSwitcher instance = null;

        private Animation _animation;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
            }

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