using UnityEngine;

namespace MultyLanguageSupporter.Localizers
{
    public abstract class LocalizerBase : MonoBehaviour, ILocalizer
    {
        [SerializeField]
        private string localizerKey;
        public string LocalizerKey => localizerKey;

        protected abstract void GetTextComponent();

        private void Awake()
        {
            GetTextComponent();
            LanguageSupporter.RegisterLocalizer(this);
        }

        public abstract void SetText(string text);

        public void Dispose()
        {
            LanguageSupporter.DisposeLocalizer(this);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}