using TMPro;
using UnityEngine;

namespace MultyLanguageSupporter.Localizers
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizerTMP : LocalizerBase
    {
        private TMP_Text textComponent;

        protected override void GetTextComponent()
        {
            textComponent = GetComponent<TMP_Text>();
        }

        public override void SetText(string text)
        {
            textComponent.SetText(text);
        }

    }
}