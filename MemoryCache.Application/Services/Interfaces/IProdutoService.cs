using MemoryCache.Domain.Entities;
using MemoryCache.Domain.Enum;
using MemoryCache.Domain.Filter;

namespace MemoryCache.Application.Services.Interfaces;

[TipoInjecao(InjectionType.Scoped)]
public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetAll();
    Task<Produto> Get(long Id);
}
