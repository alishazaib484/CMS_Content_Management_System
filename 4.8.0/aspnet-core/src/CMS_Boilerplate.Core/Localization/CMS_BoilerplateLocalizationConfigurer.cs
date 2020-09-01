using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CMS_Boilerplate.Localization
{
    public static class CMS_BoilerplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CMS_BoilerplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CMS_BoilerplateLocalizationConfigurer).GetAssembly(),
                        "CMS_Boilerplate.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
