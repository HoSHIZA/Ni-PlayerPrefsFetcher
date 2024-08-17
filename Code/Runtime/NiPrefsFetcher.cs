using NiGames.PlayerPrefsFetcher.Fetchers;

// ReSharper disable once RedundantUsingDirective
using UnityEngine;

namespace NiGames.PlayerPrefsFetcher
{
    public static class NiPrefsFetcher
    {
        private static bool? _supported;
        
        /// <summary>
        /// Checks for availability on the current platform.
        /// </summary>
        /// <remarks>Value is cached. To update value, use <see cref="CheckSupport"/>.</remarks>
        public static bool IsSupported => _supported ?? CheckSupport();
        
        /// <summary>
        /// Checks for availability on the current platform.
        /// </summary>
        /// <remarks>Updates value of the <see cref="IsSupported"/> property.</remarks>
        public static bool CheckSupport()
        {
            _supported = Retrieve() != null;
            
            return _supported.Value;
        }
        
        /// <summary>
        /// Gets all available PlayerPrefs entries on the current platform.
        /// Returns `null` if not available on the current platform.
        /// </summary>
        public static PlayerPrefsEntry[] Retrieve()
        {
#if UNITY_EDITOR_WIN || (UNITY_STANDALONE_WIN && (!UNITY_EDITOR_OSX && !UNITY_EDITOR_LINUX))
            return default(WindowsFetcher).Retrieve();
#elif UNITY_EDITOR_OSX || (UNITY_STANDALONE_OSX && (!UNITY_EDITOR_WIN && !UNITY_EDITOR_LINUX))
            return default(OsxFetcher).Retrieve();
#elif UNITY_EDITOR_LINUX || (UNITY_STANDALONE_LINUX && (!UNITY_EDITOR_WIN && !UNITY_EDITOR_OSX))
            Debug.LogWarning($"[NiPrefsFetcher] Support for this platform will be coming soon!");
            return null; // TODO: Implement
#elif UNITY_ANDROID
            return default(AndroidFetcher).Retrieve();
#elif UNITY_IOS
            Debug.LogWarning($"[NiPrefsFetcher] Support for this platform will be coming soon!");
            return null; // TODO: Implement
#elif UNITY_WEBGL
            Debug.LogWarning($"[NiPrefsFetcher] Support for this platform will be coming soon!");
            return null; // TODO: Implement
#else
            return null;
#endif
        }
    }
}