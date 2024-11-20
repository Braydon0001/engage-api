namespace Engage.Application.Filters;

using Engage.Application.Contracts;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using MassTransit;

public class TenantConsumeFilter<T>(IMultiTenantContextSetter MultiTenantContextSetter, IMultiTenantStore<TenantAndSupplierInfo> MultiTenantStore) : IFilter<ConsumeContext<T>> where T : BaseContract
{
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var tenantIdentifier = context.Message.TenantIdentifier;
        if (tenantIdentifier is not null)
        {
            var tenantInfo = await MultiTenantStore.TryGetByIdentifierAsync(tenantIdentifier);
            MultiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantAndSupplierInfo> { TenantInfo = tenantInfo };
        }

        await next.Send(context);
    }

    public void Probe(ProbeContext context) { }
}

