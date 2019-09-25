using UXPinTests.Utilities;
using System.Collections.Generic;

namespace UXPinTests.Translations
{
    internal class DashboardTranslations
    {
        public List<KeyValuePair<Lang, string>> ProjectTilesEmptyTooltipText => new List<KeyValuePair<Lang, string>>()
        {
            new KeyValuePair<Lang, string>(Lang.en, "Start by creating your first project"),
            new KeyValuePair<Lang, string>(Lang.es, "Comienza creando tu primer proyecto")
        };
    }
}
