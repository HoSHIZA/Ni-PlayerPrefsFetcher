using JetBrains.Annotations;

#if UNITY_STANDALONE_OSX
using System;
using System.Collections.Generic;
using System.IO;
using NiGames.PlayerPrefsFetcher.Utility;
using UnityEngine;
#endif

namespace NiGames.PlayerPrefsFetcher.Fetchers
{
    [UsedImplicitly]
    public readonly struct OsxFetcher : IPlayerPrefsFetcher
    {
        public PlayerPrefsEntry[] Retrieve()
        {
#if UNITY_STANDALONE_OSX
            var path = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library/Preferences"), 
                $"unity.{Application.companyName}.{Application.productName}.plist");

            if (Plist.readPlist(path) is not Dictionary<string, object> parsed) return null;
            
            var result = new PlayerPrefsEntry[parsed.Count];
            
            var i = 0;
            foreach (var (key, value) in parsed)
            {
                switch (value)
                {
                    case double:
                        if (PlayerPrefsUtility.TryAddFloat(ref result, i, key)) break;
                        if (PlayerPrefsUtility.TryAddInt(ref result, i, key)) break;
                        PlayerPrefsUtility.AddInvalid(ref result, i);
                        break;
                    default:
                        PlayerPrefsUtility.AddString(ref result, i, key);
                        break;
                }
                
                i++;
            }

            return result;
#else
            return null;
#endif
        }
    }
}