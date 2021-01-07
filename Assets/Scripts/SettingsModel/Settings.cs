using System;

namespace Assets.Scripts.SettingsModel
{
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
