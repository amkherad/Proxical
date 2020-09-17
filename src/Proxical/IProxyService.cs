namespace Proxical
{
    public interface IProxyService
    {
        TInterface Proxy<TInterface, TImplementation>(
            ProxyContext context
        );
    }
}