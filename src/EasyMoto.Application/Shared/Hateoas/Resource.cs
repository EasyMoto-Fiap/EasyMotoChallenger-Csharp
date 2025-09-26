namespace EasyMoto.Application.Shared.Hateoas;

public record Resource<T>(T Data, IEnumerable<Link> Links);