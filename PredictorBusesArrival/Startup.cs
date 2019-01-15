using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PredictorBusesArrival.Startup))]
namespace PredictorBusesArrival
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
