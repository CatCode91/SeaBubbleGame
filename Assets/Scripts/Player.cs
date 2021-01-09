using Assets.Scripts.SettingsModel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player instance = null;

        //для хранения настроек с помощью сериализации (не работает на Android)
        private SettingsWorker _settings;

        public List<int> Scores { get; private set; }

        public int LastScore => Scores.Last();

        public int GetMaxResult()
        {
            if (Scores.Count > 0)
            {
                return Scores.Max();
            }

            else
            {
                return 0;
            }
        }

        private void Awake()
        { 
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            else
            {
                Destroy(this.gameObject);
            }

            Initializator();
        }

        private void Initializator()
        {
            Scores = new List<int>();
            _settings = SettingsWorker.GetInstance();
            int max_result = _settings.GetSetting<int>(SettingsType.MaximumRate);
            if (max_result > 0) 
            {
                Scores.Add(max_result);
            }
        }

        public void AddScore(int rate)
        {
            if (rate > GetMaxResult())
            {
                 _settings.SaveOrUpdateSettings<int>(SettingsType.MaximumRate, rate);
            }

            Scores.Add(rate);
        }
    }
}
