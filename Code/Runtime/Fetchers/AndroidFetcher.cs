using JetBrains.Annotations;

#if UNITY_ANDROID
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using NiGames.PlayerPrefsFetcher.Utility;
using UnityEngine;
#endif

namespace NiGames.PlayerPrefsFetcher.Fetchers
{
    [UsedImplicitly]
    public readonly struct AndroidFetcher : IPlayerPrefsFetcher
    {
        public PlayerPrefsEntry[] Retrieve()
        {
#if UNITY_ANDROID
            var path = $"/data/data/{Application.identifier}/shared_prefs/{Application.identifier}.v2.playerprefs.xml";
            
            var xmlDoc = new XmlDocument();
            var xmlTextSb = new StringBuilder();

            using (var file = File.Open(path, FileMode.Open))
            {
                using (var reader = new StreamReader(file))
                {
                    string line;
                    
                    do
                    {
                        line = reader.ReadLine();
                        xmlTextSb.AppendLine(line);
                    } while (line != null);
                }
            }
            
            xmlDoc.LoadXml(xmlTextSb.ToString());
            var baseNode = xmlDoc.DocumentElement;

            if (baseNode == null) return null;

            var result = new PlayerPrefsEntry[baseNode.ChildNodes.Count];

            for (var i = 0; i < baseNode.ChildNodes.Count; i++)
            {
                var node = baseNode.ChildNodes[i];
                
                var key = WebUtility.UrlDecode(node.Attributes?["name"].Value);
                
                switch (node.Name)
                {
                    case "string":
                        PlayerPrefsUtility.AddString(ref result, i, key);
                        break;
                    case "float":
                        PlayerPrefsUtility.AddFloat(ref result, i, key);
                        break;
                    case "int":
                        PlayerPrefsUtility.AddInt(ref result, i, key);
                        break;
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