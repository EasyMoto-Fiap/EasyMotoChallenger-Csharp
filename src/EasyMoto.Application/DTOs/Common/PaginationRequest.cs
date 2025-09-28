using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Common
{
    public class PaginationRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 200)]
        public int PageSize { get; set; } = 10;
    }
}