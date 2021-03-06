using MultiLanguageSupporter.Localizers;
using System.Collections.Generic;
using System.Globalization;

namespace MultiLanguageSupporter
{
    public class LanguageSupporter
    {
        internal static Dictionary<string, string> KeyValueData = new Dictionary<string, string>();
        internal readonly static HashSet<ILocalizer> Localizers = new HashSet<ILocalizer>();

        static LanguageSupporter()
        {
            CSVLoader.Load(ref KeyValueData, CultureInfo.CurrentCulture.ThreeLetterISOLanguageName);
        }

        public static void ChangeLanguage(string threeLetterISOLangName)
        {
            CSVLoader.Load(ref KeyValueData, threeLetterISOLangName);
            UpdateAllLocalizers();
        }

        public static void UpdateAllLocalizers()
        {
            foreach (var localizer in Localizers)
                Localize(localizer);
        }

        internal static void RegisterLocalizer(ILocalizer localizer)
        {
            if (Localizers.Contains(localizer)) return;
            Localizers.Add(localizer);
            Localize(localizer);
        }

        internal static bool DisposeLocalizer(ILocalizer localizer) => Localizers.Remove(localizer);

        private static void Localize(ILocalizer localizer)
        {
            if (!KeyValueData.TryGetValue(localizer.LocalizerKey, out string value))
                value = "KeyNotFound";
            localizer.SetText(value);
        }
    }
}
