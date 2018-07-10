using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Donna.Core.TTS.Client
{
    public class SsmlBuilder : ISsmlBuilder
    {
        /// <summary>
        /// Generates SSML.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="voiceType">The gender.</param>
        /// <param name="voiceName">The voice name.</param>
        /// <param name="text">The text input.</param>
        public string GenerateSsml(string locale, Gender voiceType, string voiceName, string text)
        {
            var genderValue = "";
            switch (voiceType)
            {
                case Gender.Male:
                    genderValue = "Male";
                    break;

                case Gender.Female:
                default:
                    genderValue = "Female";
                    break;
            }

            var ssmlDoc = new XDocument(
                              new XElement("speak",
                                  new XAttribute("version", "1.0"),
                                  new XAttribute(XNamespace.Xml + "lang", "en-US"),
                                  new XElement("voice",
                                      new XAttribute(XNamespace.Xml + "lang", locale),
                                      new XAttribute(XNamespace.Xml + "gender", genderValue),
                                      new XAttribute("name", voiceName),
                                      text)));
            return ssmlDoc.ToString();
        }

    }
}
