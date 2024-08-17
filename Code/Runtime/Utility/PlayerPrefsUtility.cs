using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NiGames.PlayerPrefsFetcher.Utility
{
    internal static class PlayerPrefsUtility
    {
        #region Add - Float
        
        /// <summary>
        /// Checks the type of PlayerPrefs entry, if true, gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static bool TryAddFloat(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            if (!KeyIsFloat(key, false)) return false;
            
            AddFloat(ref entries, index, key, UnityEngine.PlayerPrefs.GetFloat(key));
            
            return true;
        }
        
        /// <summary>
        /// Gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddFloat(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            var value = UnityEngine.PlayerPrefs.GetFloat(key).ToString(CultureInfo.InvariantCulture);
            
            entries[index] = PlayerPrefsEntry.Float(key, value);
        }
        
        /// <summary>
        /// Adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddFloat(ref PlayerPrefsEntry[] entries, int index, string key, float value)
        {
            var str = value.ToString(CultureInfo.InvariantCulture);
            
            entries[index] = PlayerPrefsEntry.Float(key, str);
        }
        
        #endregion
        
        #region Add - Int
        
        /// <summary>
        /// Checks the type of PlayerPrefs entry, if true, gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static bool TryAddInt(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            if (!KeyIsInt(key, false)) return false;
            
            AddInt(ref entries, index, key, UnityEngine.PlayerPrefs.GetInt(key));
            
            return true;
        }
        
        /// <summary>
        /// Gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddInt(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            var value = UnityEngine.PlayerPrefs.GetInt(key).ToString();
            
            entries[index] = PlayerPrefsEntry.Int(key, value);
        }
        
        /// <summary>
        /// Adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddInt(ref PlayerPrefsEntry[] entries, int index, string key, int value)
        {
            var str = value.ToString();
            
            entries[index] = PlayerPrefsEntry.Int(key, str);
        }
        
        #endregion
        
        #region Add - String
        
        /// <summary>
        /// Checks the type of PlayerPrefs entry, if true, gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static bool TryAddString(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            if (!KeyIsString(key, false)) return false;
            
            AddString(ref entries, index, key, UnityEngine.PlayerPrefs.GetString(key));
            
            return true;
        }
        
        /// <summary>
        /// Gets and adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddString(ref PlayerPrefsEntry[] entries, int index, string key)
        {
            var value = UnityEngine.PlayerPrefs.GetString(key);
            
            entries[index] = PlayerPrefsEntry.String(key, value);
        }
        
        /// <summary>
        /// Adds the value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddString(ref PlayerPrefsEntry[] entries, int index, string key, string value)
        {
            entries[index] = PlayerPrefsEntry.String(key, value);
        }
        
        #endregion

        #region Add - Invalid

        /// <summary>
        /// Adds invalid value to the <c>entries</c> array.
        /// </summary>
        [MethodImpl(256)]
        public static void AddInvalid(ref PlayerPrefsEntry[] entries, int index)
        {
            entries[index] = new PlayerPrefsEntry(PlayerPrefsType.Invalid, null, null);
        }

        #endregion
        
        #region Type Checking
        
        /// <summary>
        /// Parsing the PlayerPrefs entry type.
        /// </summary>
        public static PlayerPrefsType ParseKeyType(string key)
        {
            if (UnityEngine.PlayerPrefs.HasKey(key)) return PlayerPrefsType.Invalid;
            
            if (KeyIsString(key, false))  return PlayerPrefsType.String;
            if (KeyIsFloat(key, false))   return PlayerPrefsType.Float;
            if (KeyIsInt(key, false))     return PlayerPrefsType.Int;
            
            return PlayerPrefsType.Invalid;
        }

        /// <summary>
        /// Checks the type of key.
        /// </summary>
        public static bool KeyIsString(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                UnityEngine.PlayerPrefs.GetString(key, defaultValue: null) == null &&
                UnityEngine.PlayerPrefs.GetString(key, defaultValue: string.Empty) == string.Empty
            );
        }

        /// <summary>
        /// Checks the type of key.
        /// </summary>
        public static bool KeyIsFloat(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                Mathf.Approximately(UnityEngine.PlayerPrefs.GetFloat(key, defaultValue: -1f), -1f) &&
                Mathf.Approximately(UnityEngine.PlayerPrefs.GetFloat(key, defaultValue: 1f), 1f)
            );
        }

        /// <summary>
        /// Checks the type of key.
        /// </summary>
        public static bool KeyIsInt(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                UnityEngine.PlayerPrefs.GetInt(key, defaultValue: -1) == -1 &&
                UnityEngine.PlayerPrefs.GetInt(key, defaultValue: 1) == 1
            );
        }
        
        #endregion
    }
}