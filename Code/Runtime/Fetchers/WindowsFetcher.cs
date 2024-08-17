using JetBrains.Annotations;

#if !(UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) && !(NET_STANDARD || NET_STANDARD_2_0 || NET_STANDARD_2_1)
#else
using NiGames.PlayerPrefsFetcher.Utility;
using Microsoft.Win32;
using UnityEngine;
#endif

namespace NiGames.PlayerPrefsFetcher.Fetchers
{
    [UsedImplicitly]
    internal readonly struct WindowsFetcher : IPlayerPrefsFetcher
    {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) && !(NET_STANDARD || NET_STANDARD_2_0 || NET_STANDARD_2_1)
        private static RegistryKey GetKey()
        {
#if UNITY_EDITOR_WIN
            const string registryKeyPattern = @"Software\Unity\UnityEditor\{0}\{1}";
#else
            const string registryKeyPattern = @"Software\{0}\{1}";
#endif
            
            var name = string.Format(registryKeyPattern, Application.companyName, Application.productName);
            
            return Registry.CurrentUser.OpenSubKey(name, true);
        }
#endif

        public PlayerPrefsEntry[] Retrieve()
        {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) && !(NET_STANDARD || NET_STANDARD_2_0 || NET_STANDARD_2_1)
            var registryKey = GetKey();
            
            if (registryKey == null) return null;

            var valueNames = registryKey.GetValueNames();

            var result = new PlayerPrefsEntry[valueNames.Length];

            for (var i = 0; i < valueNames.Length; i++)
            {
                var rawKey = valueNames[i];
                
                var index = rawKey.LastIndexOf('_');
                var key = index == -1 ? rawKey : rawKey.Remove(index, rawKey.Length - index);
                
                switch (registryKey.GetValue(rawKey))
                {
                    case int:
                    case long:
                    {
                        if (PlayerPrefsUtility.TryAddFloat(ref result, i, key)) break;
                        if (PlayerPrefsUtility.TryAddInt(ref result, i, key)) break;
                        PlayerPrefsUtility.AddInvalid(ref result, i);
                        break;
                    }
                    case byte[]:
                    {
                        PlayerPrefsUtility.AddString(ref result, i, key);
                        break;
                    }
                    default:
                        PlayerPrefsUtility.AddInvalid(ref result, i);
                        break;
                }
            }

            return result;
#else
            return null;
#endif
        }
    }
}