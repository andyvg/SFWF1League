using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;

namespace Sfw.Web.Attributes
{
    public class MatchParametersOnRouteKeys : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var methodParams = methodInfo.GetParameters();
            List<string> passedValues = GetReleventRouteDataValues(controllerContext.RouteData.Values);

            return CheckIfPassedValuesMatchMethodParameters(passedValues, methodParams);
        }

        public bool CheckIfPassedValuesMatchMethodParameters(List<string> passedValues, ParameterInfo[] methodParams)
        {
            if (methodParams.Length != passedValues.Count)
                return false;

            return methodParams.All(t => passedValues.Contains(t.Name.ToLower()));
        }

        public List<string> GetReleventRouteDataValues(RouteValueDictionary routeVals)
        {
            return routeVals.Where(v => IsNotTheActionOrController(v) && IsASimpleType(v)).Select(rv => rv.Key.ToLower()).ToList();
        }

        private static bool IsASimpleType(KeyValuePair<string, object> v)
        {
            return (v.Value.GetType().IsPrimitive || v.Value is String);
        }

        private static bool IsNotTheActionOrController(KeyValuePair<string, object> v)
        {
            return v.Key != "action" && v.Key != "controller" && v.Key != "area";
        }
    }
}