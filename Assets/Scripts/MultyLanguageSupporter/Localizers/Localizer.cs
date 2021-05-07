using UnityEngine;
using UnityEngine.UI;

namespace MultyLanguageSupporter.Localizers
{
    [RequireComponent(typeof(Text))]
    public class Localizer : LocalizerBase
    {
        private Text textComponent;

        protected override void GetTextComponent()
        {
            textComponent = GetComponent<Text>();
        }

        public override void SetText(string text)
        {
            textComponent.text = text;
        }
    }
}