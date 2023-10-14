using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace TradeSwing.APIs.Errors;

public class TradeSwingProblemDetailsFactory : ProblemDetailsFactory
  {

    #nullable disable
    private readonly ApiBehaviorOptions _options;
    private readonly Action<ProblemDetailsContext> _configure;


    #nullable enable
    public TradeSwingProblemDetailsFactory(
      IOptions<ApiBehaviorOptions> options,
      IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
      _options = options?.Value ?? throw new ArgumentNullException(nameof (options));
      _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }

    public override ProblemDetails CreateProblemDetails(
      HttpContext httpContext,
      int? statusCode = null,
      string? title = null,
      string? type = null,
      string? detail = null,
      string? instance = null)
    {
      statusCode ??= 500;
      var problemDetails = new ProblemDetails
      {
        Status = statusCode,
        Title = title,
        Type = type,
        Detail = detail,
        Instance = instance
      };
      ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
      return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
      HttpContext httpContext,
      ModelStateDictionary modelStateDictionary,
      int? statusCode = null,
      string? title = null,
      string? type = null,
      string? detail = null,
      string? instance = null)
    {
      if (modelStateDictionary == null)
        throw new ArgumentNullException(nameof (modelStateDictionary));
      
      statusCode ??= 400;
      var validationProblemDetails1 = new ValidationProblemDetails(modelStateDictionary)
        {
          Status = statusCode,
          Type = type,
          Detail = detail,
          Instance = instance
        };
      
      if (title != null)
        validationProblemDetails1.Title = title;
      
      ApplyProblemDetailsDefaults(httpContext, validationProblemDetails1, statusCode.Value);
      
      return validationProblemDetails1;
    }


    #nullable disable
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
      var status = problemDetails.Status;
      
      if (!status.HasValue)
      {
        problemDetails.Status = statusCode;
      }

      if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
      {
        problemDetails.Title ??= clientErrorData.Title;
        problemDetails.Type ??= clientErrorData.Link;
      }
      var str1 = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
      if (str1 != null)
        problemDetails.Extensions["traceId"] = str1;
      
      _configure?.Invoke(new ProblemDetailsContext
      {
        HttpContext = httpContext!,
        ProblemDetails = problemDetails
      });
    }
  }