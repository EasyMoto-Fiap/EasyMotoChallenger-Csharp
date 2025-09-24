using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Hypermedia;

public static class HateoasBuilder
{
    public static ResourceDto<T> WithLinks<T>(T data, IEnumerable<LinkDto> links)
    {
        var r = new ResourceDto<T>(data);
        r.Links.AddRange(links);
        return r;
    }

    public static IEnumerable<LinkDto> PagingLinks(IUrlHelper url, string routeName, int page, int size, bool hasPrev, bool hasNext)
    {
        var list = new List<LinkDto>
        {
            new() { Rel = "self", Href = url.Link(routeName, new { page, size }) ?? string.Empty, Method = "GET" }
        };
        if (hasPrev) list.Add(new LinkDto { Rel = "prev", Href = url.Link(routeName, new { page = page - 1, size }) ?? string.Empty, Method = "GET" });
        if (hasNext) list.Add(new LinkDto { Rel = "next", Href = url.Link(routeName, new { page = page + 1, size }) ?? string.Empty, Method = "GET" });
        return list;
    }
}