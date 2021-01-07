using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts.SettingsModel
{
    public class SettingsWorker
    {
        private static SettingsWorker _instance;

        private BinaryFormatter _formatter = new BinaryFormatter();
        private string _filename = "settings.dat";
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
        }

        public void SaveOrUpdateSettings<T>(SettingsType name, T value) 
        {
            //десериализируем из файла
            _settings = GetAllSettings();

            try 
            {
                _settings[name] = value;
            }

            catch 
            {
                _settings.Add(name, value);
            }


            using (Stream fStream = new FileStream(_filename,FileMode.Create, FileAccess.Write, FileShare.None))
            {
                _formatter.Serialize(fStream, _settings);
            }
        }

        public T GetSetting<T>(SettingsType name)
        {
            _settings = GetAllSettings();

            T value = default(T);

            try
            {
                value = (T)_settings[name];
            }

            catch 
            {
                SaveOrUpdateSettings<T>(name, default(T));
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
