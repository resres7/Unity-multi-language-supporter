using TMPro;
using UnityEngine;

namespace MultiLanguageSupporter.Localizers
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizerTMP : LocalizerBase
    {
        private TMP_Text textComponent;
        public TMP_Text TextComponent => textComponent ?? (textComponent = GetComponent<TMP_Text>());

        public override void SetText(string text)
        {
            TextComponent.SetText(text);
        }
    }
}