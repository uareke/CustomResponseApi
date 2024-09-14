using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Domain.Enum
{
    /// <summary>
    /// Enumerador de identifica o tipo de injeção de dependencia
    /// </summary>
    public enum InjectionType
    {
        /// <summary>
        /// O serviço é registrado como Scoped, o que significa que será criado uma vez por requisição.
        /// </summary>
        Scoped,

        /// <summary>
        /// O serviço é registrado como Transient, o que significa que será criado toda vez que for solicitado.
        /// </summary>
        Transient,

        /// <summary>
        /// O serviço é registrado como Singleton, o que significa que será criado uma única vez e compartilhado durante a aplicação inteira.
        /// </summary>
        Singleton
    }
}
