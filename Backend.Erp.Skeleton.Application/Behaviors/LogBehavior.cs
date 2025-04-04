using Backend.Erp.Skeleton.Domain.Auxiliares;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Behaviors
{
    public class LogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly IMongoRepository<Logs> _mongoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogBehavior(IMongoRepository<Logs> mongoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mongoRepository = mongoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            var controller = context.Request.Path.Value.Split('/');

            var jsonParametros = JsonSerializer.Serialize(request);

            var parametros = RemoveFieldsByName(jsonParametros, ["password"]);

            var logs = new Logs()
            {
                Method = context.Request.Method,
                RequestHeaders = string.Join("\n", context.Request.Headers.Select((x, y) => $"{x}")),
                Controller = controller[controller.Length - 2],
                Url = $"{controller[controller.Length - 2]}/{controller[controller.Length - 1]}",
                RequestBody = parametros,
                QueryString = context.Request.QueryString.Value
            };

            try
            {
                var response = await next();
                await SalvarLogs(controller, logs, true);
                return response;
            }
            catch (Exception ex)
            {
                logs.Error = ex.Message.ToString();
                await SalvarLogs(controller, logs, false);
                throw;
            }
        }

        private async Task SalvarLogs(string[] controller, Logs logs, bool success)
        {
            logs.Success = success;
            await _mongoRepository.InsertOneAsync(controller[3].ToLower(), logs);
        }

        private static string RemoveFieldsByName(string json, List<string> fieldName)
        {
            JsonNode node = JsonNode.Parse(json);
            if (node is not null)
                RemoveFieldRecursively(node, fieldName);


            return RemoveNewLines(node?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }) ?? json);
        }

        private static void RemoveFieldRecursively(JsonNode node, List<string> fieldName)
        {
            if (node is JsonObject jsonObject)
            {
                var keysToRemove = new List<string>();

                foreach (var property in jsonObject)
                    if (fieldName.Contains(property.Key))
                        keysToRemove.Add(property.Key);
                    else if (property.Value is JsonObject || property.Value is JsonArray)
                        RemoveFieldRecursively(property.Value, fieldName);

                foreach (var key in keysToRemove)
                    jsonObject.Remove(key);
            }
            else if (node is JsonArray array)
                foreach (var item in array)
                    if (item is JsonObject || item is JsonArray)
                        RemoveFieldRecursively(item, fieldName);
        }

        private static string RemoveNewLines(string input)
                => input.Replace("\n", "").Replace("\r", "").Replace(" ", "").Trim();
    }
}
