namespace EasyMoto.Api.Hypermedia;

public sealed class ResourceDto<T>
{
    public T Data { get; }
    public List<LinkDto> Links { get; } = new();

    public ResourceDto(T data) => Data = data;
}