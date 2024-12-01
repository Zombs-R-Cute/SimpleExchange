using Rocket.Core.Plugins;

namespace Zombs_R_Cute_SimpleExchange
{
    public class ExchangePlugin : RocketPlugin<ExchangePluginConfiguration>
    {
        public static ExchangePlugin Instance;
        
        protected override void Load()
        {
            Instance = this;
        }
    }
}