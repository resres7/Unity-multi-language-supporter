using MultyLanguageSupporter.Localizers;
using System.Collections.Generic;
using System.Globalization;

namespace MultyLanguageSupporter
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
                SetText(localizer);
        }

        internal static void RegisterLocalizer(ILocalizer localizer)
        {
            if (Localizers.Contains(localizer)) return;
            Localizers.Add(localizer);
            SetText(localizer);
        }

        internal static bool DisposeLocalizer(ILocalizer localizer) => Localizers.Remove(localizer);

        private static void SetText(ILocalizer localizer)
        {
            if (!KeyValueData.TryGetValue(localizer.LocalizerKey, out string value))
                value = "KeyNotFound";
            localizer.SetText(value);
        }
    }
}
