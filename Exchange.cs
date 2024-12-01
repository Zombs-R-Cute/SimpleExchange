using System.Xml.Serialization;

namespace Zombs_R_Cute_SimpleExchange
{
    public class Exchange
    {
        [XmlAttribute("NewItem")] public ushort NewItemId;
        [XmlAttribute("TradedQuantity")] public ushort Quantity;
        [XmlAttribute("TradedItem")] public ushort TradedItemId;
    }
}