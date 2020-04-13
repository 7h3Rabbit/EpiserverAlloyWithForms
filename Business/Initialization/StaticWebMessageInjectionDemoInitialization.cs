using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using StaticWebEpiserverPlugin.Services;

namespace EpiserverAlloyWithForms.Business.Initialization
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class StaticWebMessageInjectionDemoInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var service = ServiceLocator.Current.GetInstance<IStaticWebService>();
            service.AfterGetPageContent += OnAfterGetPageContent;
        }

        private void OnAfterGetPageContent(object sender, StaticWebEpiserverPlugin.Events.StaticWebGeneratePageEventArgs e)
        {
            if (e.Content == null)
                return;

            /***
             * NOTE: This is not a good example in any way...
             * Not only because this can be done with displaychannel specific views instead...
             * But it was the only example I came up with at this time so here you go.... An example of how to use events when generating pages
             **/

            var warningsMessage = "<div class=\"container navbar-fixed-bottom\"><div class=\"row\"><div class=\"alert alert-warning\" role=\"alert\">You are currently viewing a limited version of our website. One of the reasons for this could be because we currently have an abnormal amout of traffic.</div></div></div><br><br><br><br></body>";
            e.Content = e.Content.Replace("</body>", warningsMessage);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}