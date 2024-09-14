using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Domain.Map.@base;

public abstract class MapeadorBase<TRequest, TResponse>
{
    public virtual IEnumerable<TResponse> Mapear(IEnumerable<TRequest> entradas)
    {
        if (entradas is null)
            return Enumerable.Empty<TResponse>();

        var enumerable = entradas.ToList();
        return !enumerable.Any()
            ? new List<TResponse>()
            : enumerable.Select(entrada => Mapear(entrada)).Where(retorno => retorno != null).ToList();
    }

    public abstract TResponse Mapear(TRequest entrada);
}
