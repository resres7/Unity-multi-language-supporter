using UnityEngine;

namespace MultiLanguageSupporter.Localizers
{
    public abstract class LocalizerBase : MonoBehaviour, ILocalizer
    {
        [SerializeField]
        private string localizerKey;
        public string LocalizerKey => localizerKey;


        private void Awake()
        {
            LanguageSupporter.RegisterLocalizer(this);
            OnAwake();
        }

        public virtual void OnAwake() { }

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