using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace Zombs_R_Cute_SimpleExchange
{
    public class ExchangePluginConfiguration : IRocketPluginConfiguration
    {
        [XmlArray("Exchanges")] public List<Exchange> Exchanges;

        public void LoadDefaults()
        {
            Exchanges = new List<Exchange>()
                { new Exchange { NewItemId = 52309, Quantity = 1, TradedItemId = 51919 } };
        }
    }
}