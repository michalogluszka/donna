namespace Donna.Core.TTS.Client
{
    public interface ISsmlBuilder
    {
        string GenerateSsml(string locale, Gender gender, string name, string text);
    }
}