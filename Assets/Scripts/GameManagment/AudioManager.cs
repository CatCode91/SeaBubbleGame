using Assets.Scripts.SettingsModel;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    //Синглтон для аудио в игре

    public class AudioManager : MonoBehaviour
    {
        //Сохранение настроек с помощью сериализации
        private SettingsWorker _sets = SettingsWorker.GetInstance();

        public static AudioManager instance = null;
        public AudioSource Audio { get; private set; }
        
        public AudioClip Menu;

        public AudioClip Game;

        public bool IsSoundOn { get; private set; } = true;
        public UnityAction<bool> VolumeChanged;
        
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

        public void ChangeVolume()
        {
            if (IsSoundOn)
            {
                IsSoundOn = false;
                _sets.SaveOrUpdateSettings<bool>(SettingsType.SoundEnable, false);
                Audio.volume = 0;
            }

            else
            {
                IsSoundOn = true;
                _sets.SaveOrUpdateSettings<bool>(SettingsType.SoundEnable, true);
                Audio.volume = 1;
            }

            VolumeChanged?.Invoke(IsSoundOn);
        }

        private void Start()
        {
            IsSoundOn = _sets.GetSetting<bool>(SettingsType.SoundEnable);
            VolumeChanged?.Invoke(IsSoundOn);
            Audio.clip = Menu;
            Audio.volume = IsSoundOn ? 1 : 0;
            Audio.Play();
        }

        private void Initializator()
        {
            Audio = GetComponent<AudioSource>();
        }
    }
}
