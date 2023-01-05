namespace PCPPlugins.BusinessLayer.Extensions
{
    using System;

    /// <summary>
    /// Defines the <see cref="GuidExtensions" />.
    /// </summary>
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value.Equals(Guid.Empty);
        }

    }
}
