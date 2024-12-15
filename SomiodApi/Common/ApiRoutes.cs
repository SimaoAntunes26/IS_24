using System.Text.RegularExpressions;
using System.Web.Http;

namespace SOMIOD.Common
{
    public abstract class ApiRoutes : ApiController
    {
        protected static readonly string specialHeader = "somiod-locate";

        public static bool UrlNameValidation(string name)
        {
            if (name == null)
                return true;

            if (Regex.Match(name, "^[A-Za-z0-9._~-]+$").Success)
                return true;

            return false;
        }
    }
}
