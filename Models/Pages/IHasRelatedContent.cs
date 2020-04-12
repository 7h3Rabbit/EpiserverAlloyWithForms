using EPiServer.Core;

namespace EpiserverAlloyWithForms.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
