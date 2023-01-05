
/// <summary>
/// this Class file is the extension for Dynamics CRM
/// </summary>
namespace RexStudios.CsharpExtensions
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Collections.Generic;

    public static class EntityExtension
    {
        public static T Clone<T>(this T entity) where T : Entity
        {
            Entity clone = new Entity(entity.LogicalName);
            foreach (KeyValuePair<string, object> attr in entity.Attributes)
            {
                if (attr.Key.ToLower() == entity.LogicalName.ToLower() + "id")
                    continue;
                clone[attr.Key] = attr.Value;
            }
            return clone.ToEntity<T>();
        }
    }

    // Usage
    //Entity account = OrganizationService.Retrieve("account", Guid.Parse("6a7cb8d1-7038-ea11-a813-000d3a385a1c"), new ColumnSet(true));
    //Entity clone = account.Clone();
    //Guid cloneId = OrganizationService.Create(clone);



    public static class IPluginExecutionContextExtension
    {
        public static Entity RetrieveEntity(this IPluginExecutionContext context)
        {
            return (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity) ? (Entity)context.InputParameters["Target"] : null;
        }

        public static EntityReference RetrieveEntityReference(this IPluginExecutionContext context)
        {
            return (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference) ? (EntityReference)context.InputParameters["Target"] : null;
        }

        public static Entity RetrievePreImageEntity(this IPluginExecutionContext context, string preImageName)
        {
            return (context.PreEntityImages.Contains(preImageName) && context.PreEntityImages[preImageName] is Entity) ? (Entity)context.PreEntityImages[preImageName] : null;
        }

        public static Relationship RetrieveRelationship(this IPluginExecutionContext context)
        {
            return (context.InputParameters.Contains("Relationship") && context.InputParameters["Relationship"] is Relationship) ? (Relationship)context.InputParameters["Relationship"] : null;
        }

        public static EntityReferenceCollection RetrieveRelatedEntities(this IPluginExecutionContext context)
        {
            return (context.InputParameters.Contains("RelatedEntities") && context.InputParameters["RelatedEntities"] is EntityReferenceCollection) ? (EntityReferenceCollection)context.InputParameters["RelatedEntities"] : null;
        }
        
         public static ExecuteMultipleResponse ExecuteMultiple(this ExecuteMultipleRequest request, IOrganizationService service, ITracingService tracingService)
        {
            try
            {
                var response = (ExecuteMultipleResponse)service.Execute(request);

                // Check the responses for any errors
                foreach (var responseItem in response.Responses)
                {
                    if (responseItem.Fault != null)
                    {
                        // Throw an exception to handle the error
                        throw new Exception(responseItem.Fault.Message);
                    }
                }
                return response;
            }

            catch (FaultException<OrganizationServiceFault> ex)
            {
                tracingService?.Trace($"Error thrown at Execute Multiple Resposne, {ex.Message}");
                throw ex;
            }
            catch (Exception ex)
            {
                tracingService?.Trace($"Error thrown at Execute Multiple Resposne, {ex.Message}");
                throw ex;
            }
        }

        public static ExecuteMultipleRequest AddRequests(this ExecuteMultipleRequest request, IEnumerable<OrganizationRequest> requests)
        {
            var orgReqCollection = new OrganizationRequestCollection();
            orgReqCollection.AddRange(requests);
            request.Requests = orgReqCollection;
            return request;
        }
    }

    public static class CrmHelperExtensions
    {

        //This will be updated as I have additional Extension methods I wish to share/blog about

        public static Entity Retrieve(this IOrganizationService service, string entityName, Guid id,
            params string[] columns)
        {
            return service.Retrieve(entityName, id,
                columns != null && columns.Length > 0
                    ? new Microsoft.Xrm.Sdk.Query.ColumnSet(columns)
                    : new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        }

        public static Entity Retrieve<T>(this IOrganizationService service, string entityName, Guid id,
            params string[] columns) where T : Entity
        {
            return service.Retrieve(entityName, id, columns).ToEntity<T>();
        }


    }
}
