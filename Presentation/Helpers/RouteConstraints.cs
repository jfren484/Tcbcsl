using System;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Helpers
{
    internal abstract class RouteConstraintBase : IRouteConstraint
    {
        protected abstract bool MatchValue(object value);

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            object value;
            if (!values.TryGetValue(parameterName, out value) || value == null) return false;

            return MatchValue(value);
        }
    }

    internal class NumericYearRouteConstraint : RouteConstraintBase
    {
        protected override bool MatchValue(object value)
        {
            int year;
            if (value is int)
            {
                year = (int)value;
            }
            else
            {
                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (!int.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out year)) return false;
            }

            return year >= Consts.FirstYear && year <= Consts.CurrentYear;
        }
    }

    internal class FlexYearRouteConstraint : RouteConstraintBase
    {
        protected override bool MatchValue(object value)
        {
            YearEnum year;
            if (value is int)
            {
                year = (YearEnum)(int)value;
            }
            else
            {
                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (string.Equals(valueString, YearEnum.All.ToString(), StringComparison.CurrentCultureIgnoreCase)) return true;

                int yearInt;
                if (!int.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out yearInt)) return false;
                year = (YearEnum)yearInt;
            }

            return (int)year >= Consts.FirstYear && (int)year <= Consts.CurrentYear;
        }
    }
}