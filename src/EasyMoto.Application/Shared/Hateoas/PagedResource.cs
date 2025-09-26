namespace EasyMoto.Application.Shared.Hateoas;

public record PagedResource<T>(IEnumerable<T> Items, int Page, int PageSize, int Total, IEnumerable<Link> Links);