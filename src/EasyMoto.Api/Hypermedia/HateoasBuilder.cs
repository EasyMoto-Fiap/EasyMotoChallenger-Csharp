using Microsoft.AspNetCore.Mvc;
using EasyMoto.Application.Shared.Hateoas;

namespace EasyMoto.Api.Hypermedia;

public static class HateoasBuilder
{
    public static IEnumerable<Link> PagingLinksByCount(IUrlHelper url, string routeName, int page, int pageSize, int itemsCount)
    {
        var links = new List<Link>
        {
            new("self", url.Link(routeName, new { page, pageSize })!, "GET")
        };
        if (page > 1)
            links.Add(new("prev", url.Link(routeName, new { page = page - 1, pageSize })!, "GET"));
        if (itemsCount >= pageSize)
            links.Add(new("next", url.Link(routeName, new { page = page + 1, pageSize })!, "GET"));
        return links;
    }
}