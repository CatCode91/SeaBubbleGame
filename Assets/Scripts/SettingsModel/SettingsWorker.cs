using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.SettingsModel
{
    //Синглтон для хранения/загрузки настроек с помощью сериализации

    public class SettingsWorker
    {
        private static SettingsWorker _instance;

        private BinaryFormatter _formatter = new BinaryFormatter();
        private string _filename;

        private Dictionary<SettingsType, object> _settings;

        public static SettingsWorker GetInstance()
        {
            if (_instance == null)
                _instance = new SettingsWorker();
            return _instance;
        }

        private SettingsWorker()
        {
            _settings = GetAllSettings();
            _filename = Environment.CurrentDirectory + @"\settings.dat";
        }

        public void SaveOrUpdateSettings<T>(SettingsType name, T value) 
        {
           //если платформа андроид, то сохраняем значения в PlayerPrefs
            if (Application.platform == RuntimePlatform.Android)
            {
                if(typeof(T) == typeof(int) || typeof(T) == typeof(bool))
                {
                    PlayerPrefs.SetInt(name.ToString(), Convert.ToInt32(value));
                }

                if (typeof(T) == typeof(float))
                {
                    PlayerPrefs.SetFloat(name.ToString(), Convert.ToSingle(value));
                }

                if (typeof(T) == typeof(string))
                {
                    PlayerPrefs.SetString(name.ToString(),value.ToString());
                }

                return;
            }


            //десериализируем из файла
            _settings = GetAllSettings();

            try 
            {
                //если с таким ключом настройка нашлась, сохраняем новое значение
                _settings[name] = value;
            }

            catch 
            {
                //если не нашлась, то сохраняем новую настройку
                _settings.Add(name, value);
            }

            //записываем изменения в файл
            using (Stream fStream = new FileStream(_filename,FileMode.Create, FileAccess.Write, FileShare.None))
            {
                _formatter.Serialize(fStream, _settings);
            }
        }

        public T GetSetting<T>(SettingsType name)
        {
            T value = default(T);

            //если платформа андроид, то значения вытаскиваем из PlayerPrefs.
            if (Application.platform == RuntimePlatform.Android)
            {
                object temp = name switch
                {
                    SettingsType.MaximumRate => PlayerPrefs.GetInt(SettingsType.MaximumRate.ToString()),
                    SettingsType.SoundEnable => (PlayerPrefs.GetInt(SettingsType.SoundEnable.ToString()) == 0) ? false : true,
                    _ => throw new ArgumentException("Ошибка получения настройки!")
                };

                value = (T)temp;
                return value;
            }

            _settings = GetAllSettings();

            try
            {
                value = (T)_settings[name];
            }

            catch 
            {
                SaveOrUpdateSettings(name, default(T));
            }

            return value;
        }

        private Dictionary<SettingsType, object> GetAllSettings()
        {
            if (!File.Exists(_filename))
            {
                return new Dictionary<SettingsType, object>();
            }

            using (FileStream fs = new FileStream(_filename, FileMode.OpenOrCreate))
            {
                _settings = (Dictionary<SettingsType, object>)_formatter.Deserialize(fs);
            }

            return _settings;
        }
    }
}
