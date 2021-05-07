using System;

namespace MultyLanguageSupporter.Localizers
{
    public interface ILocalizer : IDisposable
    {
        string LocalizerKey { get; }
        void SetText(string text);
    }
}