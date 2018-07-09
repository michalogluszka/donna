namespace Donna.Core.TTS.Client
{
    public interface ISsmlBuilder
    {
        string GenerateSsml(string locale, string gender, string name, string text);
    }
}