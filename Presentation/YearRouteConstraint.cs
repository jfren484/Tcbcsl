using System;
using System.Globalization;
using System.Web;
using System.Web.Routing;

namespace Tcbcsl.Presentation
{
    public class YearRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException("parameterName");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            object value;
            if (!values.TryGetValue(parameterName, out value) || value == null) return false;

            int year;
            if (value is int)
            {
                year = (int)value;
            }
            else
            {
                string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (!int.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out year)) return false;
            }

            return year >= Consts.FirstYear && year <= Consts.CurrentYear;
        }
    }
}