using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sfw.Web.Attributes.Extensions;

namespace Sfw.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoCompleteAttribute : Attribute, IMetadataAware
    {
        private readonly string _controller;
        private readonly string _action;
        private readonly string _customId;

        public AutoCompleteAttribute(string controller, string action)
        {
            _controller = controller;
            _action = action;
        }

        public AutoCompleteAttribute(string controller, string action, string customId)
        {
            _controller = controller;
            _action = action;
            _customId = customId;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetAutoComplete(_controller, _action, _customId);
        }
    }
}