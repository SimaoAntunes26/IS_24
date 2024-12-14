using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SOMIOD.Common
{
    public abstract class ApiRoutes : ApiController
    {
        protected static readonly string specialHeader = "somiod-locate";

        public static bool UrlNameValidation(string name)
        {
            if (name == null)
                return true;

            if (Regex.Match(name, "^(a-zA-Z0-9_-.~)+$").Success)
                return true;

            return false;
        }
    }
}
