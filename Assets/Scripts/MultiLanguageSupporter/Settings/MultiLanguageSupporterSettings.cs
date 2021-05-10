using UnityEngine;

namespace MultiLanguageSupporter.Settings
{
    [CreateAssetMenu(fileName = "MLSSettings", menuName = "MultiLanguageSupporterSettings/Settings")]
    public class MultiLanguageSupporterSettings : ScriptableObject
    {
        public PathSettings pathSettings;
        public string defaultLanguageISOThreeLetter = "eng";
    }
}