﻿using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Server_ST.Utilities
{
    #region Consumes
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerConsumesAttribute : Attribute
    {
        public SwaggerConsumesAttribute(params string[] contentTypes)
        {
            this.ContentTypes = contentTypes;
        }

        public IEnumerable<string> ContentTypes { get; }
    }

    public class Consumes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attribute = apiDescription.GetControllerAndActionAttributes<SwaggerConsumesAttribute>().SingleOrDefault();

            if (attribute == null)
            {
                return;
            }

            operation.consumes.Clear();
            operation.consumes = attribute.ContentTypes.ToList();
        }

    }
    #endregion

    #region Produces
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerProducesAttribute : Attribute
    {
        public SwaggerProducesAttribute(params string[] contentTypes)
        {
            this.ContentTypes = contentTypes;
        }

        public IEnumerable<string> ContentTypes { get; }
    }

    public class Produces : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attribute = apiDescription.GetControllerAndActionAttributes<SwaggerConsumesAttribute>().SingleOrDefault();

            if (attribute == null)
            {
                return;
            }

            operation.consumes.Clear();
            operation.consumes = attribute.ContentTypes.ToList();
        }

    }
    #endregion
}