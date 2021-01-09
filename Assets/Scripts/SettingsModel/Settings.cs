using System;

namespace Assets.Scripts.SettingsModel
{
    //модель для сериализации настроек в файл

    [Serializable]
    public class Settings<T>
    {
        public Settings(SettingsType name, T parameter)
        {
            Name = name;
            Parameter = parameter;
        }

        public SettingsType Name { get; set; }

        public T Parameter { get; set; }
    
    
    }
}
