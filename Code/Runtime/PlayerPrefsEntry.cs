using System.Runtime.CompilerServices;

namespace NiGames.PlayerPrefsFetcher
{
    public readonly struct PlayerPrefsEntry
    {
        public readonly PlayerPrefsType Type;
        public readonly string Key;
        public readonly string Value;
        
        public PlayerPrefsEntry(PlayerPrefsType type, string key, string value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        [MethodImpl(256)]
        public static PlayerPrefsEntry Float(string key, string value)
        {
            return new PlayerPrefsEntry(PlayerPrefsType.Float, key, value);
        }

        [MethodImpl(256)]
        public static PlayerPrefsEntry Int(string key, string value)
        {
            return new PlayerPrefsEntry(PlayerPrefsType.Int, key, value);
        }

        [MethodImpl(256)]
        public static PlayerPrefsEntry String(string key, string value)
        {
            return new PlayerPrefsEntry(PlayerPrefsType.String, key, value);
        }
    }
}