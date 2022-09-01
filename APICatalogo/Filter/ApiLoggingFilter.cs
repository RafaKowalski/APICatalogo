using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filter
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        //executa antes da Action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogTrace("Executando -> OnActionExecuting");
            _logger.LogTrace("--------------------------------------");
            _logger.LogTrace($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogTrace($"ModelState : {context.ModelState.IsValid}");
            _logger.LogTrace("--------------------------------------");
        }

        //executa depois da Action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogTrace("Executando -> OnActionExecuting");
            _logger.LogTrace("--------------------------------------");
            _logger.LogTrace($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogTrace("--------------------------------------");
        }
    }
}
