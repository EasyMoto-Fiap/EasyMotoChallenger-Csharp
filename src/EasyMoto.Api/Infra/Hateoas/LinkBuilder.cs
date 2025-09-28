namespace EasyMoto.Api.Infra.Hateoas
{
    public static class LinkBuilder
    {
        public static Dictionary<string, string> Build(string self, string update, string delete)
        {
            var links = new Dictionary<string, string>();
            links["self"] = self;
            links["update"] = update;
            links["delete"] = delete;
            return links;
        }
    }
}