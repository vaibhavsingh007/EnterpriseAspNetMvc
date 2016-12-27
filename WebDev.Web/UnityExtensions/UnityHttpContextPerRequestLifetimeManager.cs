
namespace WebDev.Web.UnityExtensions
{
    /// <summary>
    /// The UnityHttpContextPerRequestLifetimeManager exists solely to make it easier
    /// to configure the per-request lifetime manager in a configuration file.
    /// </summary>
    /// <remarks>
    /// An alternative approach would be to use a type converter to convert the 
    /// configuration string and new up a <see cref="UnityPerRequestLifetimeManager"/>
    /// from this type converter.
    /// </remarks>
    public class UnityHttpContextPerRequestLifetimeManager : UnityPerRequestLifetimeManager
    {
        public UnityHttpContextPerRequestLifetimeManager()
            : base(new HttpContextPerRequestStore())
        {
        }
    }
}