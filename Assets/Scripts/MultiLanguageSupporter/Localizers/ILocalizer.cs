using System;

namespace MultiLanguageSupporter.Localizers
{
    public interface ILocalizer : IDisposable
    {
        string LocalizerKey { get; }
        void SetText(string text);
    }
}