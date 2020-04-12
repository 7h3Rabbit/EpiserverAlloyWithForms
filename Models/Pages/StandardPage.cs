using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using StaticWebEpiserverPlugin.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpiserverAlloyWithForms.Models.Pages
{
    /// <summary>
    /// Used for the pages mainly consisting of manually created content such as text, images, and blocks
    /// </summary>
    [SiteContentType(GUID = "9CCC8A41-5C8C-4BE0-8E73-520FF3DE8267")]
    [SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
    public class StandardPage : SitePageData, IStaticWebIgnoreGenerateDynamically
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea MainContentArea { get; set; }

        public bool ShouldDeleteGenerated()
        {
            // This method is ONLY called if "ShouldGenerate" method returns false (And when this happens we ALWAYS want to remove generated page)
            return true;
        }

        public bool ShouldGenerate()
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            if (MainContentArea != null && MainContentArea.Items.Any())
            {
                foreach (var contentItem in MainContentArea.Items)
                {
                    FormContainerBlock formContainerBlock;
                    if (contentLoader.TryGet<FormContainerBlock>(contentItem.ContentLink, out formContainerBlock))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
