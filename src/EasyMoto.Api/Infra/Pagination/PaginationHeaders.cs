using System.Text.Json;

namespace EasyMoto.Api.Infra.Pagination
{
    public static class PaginationHeaders
    {
        public static void Write(HttpResponse response, int page, int pageSize, int total)
        {
            var payload = JsonSerializer.Serialize(new { page, pageSize, total });
            response.Headers["X-Pagination"] = payload;
        }
    }
}