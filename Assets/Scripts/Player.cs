using Assets.Scripts.SettingsModel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player instance = null;
        //private SettingsWorker _settings;

        public List<int> Scores { get; private set; }

        public int LastScore => Scores.Last();

        void Awake()
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
            //_settings = SettingsWorker.GetInstance();

            int max_result = PlayerPrefs.GetInt("MaxResult", 0);
           // int max_result = _settings.GetSetting<int>(SettingsType.MaximumRate);
            if (max_result > 0) 
            {
                Scores.Add(max_result);
            }
        }

        public void AddScore(int rate)
        {
            if (rate > GetMaxResult())
            {
                PlayerPrefs.SetInt("MaxResult", rate);
                //  _settings.SaveOrUpdateSettings<int>(SettingsType.MaximumRate, rate);
            }

            Scores.Add(rate);
        }

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
    }
}
