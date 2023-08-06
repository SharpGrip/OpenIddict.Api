using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SharpGrip.OpenIddict.Api.Options;

namespace SharpGrip.OpenIddict.Api.Routing
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel attributeRoutePrefixModel;
        private readonly IDictionary<string, Type> controllerTypeData;

        public RoutePrefixConvention(OpenIddictApiConfiguration openIddictApiOptions, IDictionary<string, Type> controllerTypeData)
        {
            this.controllerTypeData = controllerTypeData;
            attributeRoutePrefixModel = new AttributeRouteModel(new RouteAttribute(openIddictApiOptions.ApiRoutePrefix));
        }

        public void Apply(ApplicationModel applicationModel)
        {
            foreach (var controllerTypeDataValue in controllerTypeData)
            {
                var attributeRouteModel = new AttributeRouteModel(new RouteAttribute(controllerTypeDataValue.Key));
                var controllerModel = applicationModel.Controllers.FirstOrDefault(controllerModel => controllerModel.ControllerType == controllerTypeDataValue.Value);

                if (controllerModel != null)
                {
                    foreach (var selectorModel in controllerModel.Selectors)
                    {
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(attributeRoutePrefixModel, attributeRouteModel);
                    }
                }
            }
        }
    }
}