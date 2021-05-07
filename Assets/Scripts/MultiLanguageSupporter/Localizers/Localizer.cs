using UnityEngine;
using UnityEngine.UI;

namespace MultiLanguageSupporter.Localizers
{
    [RequireComponent(typeof(Text))]
    public class Localizer : LocalizerBase
    {
        private Text textComponent;
        public Text TextComponent => textComponent ?? (textComponent = GetComponent<Text>());

        public override void SetText(string text)
        {
            TextComponent.text = text;
        }
    }
}