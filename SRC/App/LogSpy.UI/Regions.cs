using Microsoft.Practices.Composite.Regions;
namespace LogSpy.UI
{
    /// <summary>
    /// Defines the names of the available regions
    /// </summary>
    public static class Regions
    {
        /// <summary>
        /// Represents the region that contains a view displaying the all log source entries
        /// </summary>
        public const string LogSourceView = "LogSourceView";

        public static IRegion GetLogSourceViewRegion(this IRegionManager regionManager)
        {
            return regionManager.Regions[LogSourceView];
        }
    }
}