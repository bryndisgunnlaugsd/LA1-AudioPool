using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AudioPool.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiTokenAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private const string HeaderName = "api-token";
    private const string ValidToken = "2fc8813c-be65-4dba-8b91-6e73d928ed56";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var headers = context.HttpContext.Request.Headers;

        // Block the req if the header is missing or the token doesn't match
        if (!headers.TryGetValue(HeaderName, out var token) || token.ToString() != ValidToken)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}