namespace Donna.Core.TTS.Client
{
    public interface ISsmlBuilder
    {
        string GenerateSsml(string locale, string voiceName, string text);
    }
}