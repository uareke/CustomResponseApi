using MemoryCache.Domain.Enum;

namespace MemoryCache.Domain.Filter;

[AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
public class TipoInjecaoAttribute(InjectionType tipo) : Attribute
{
    public InjectionType Tipo { get; } = tipo;
}
