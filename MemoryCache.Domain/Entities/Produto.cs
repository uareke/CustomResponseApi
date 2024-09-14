using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Domain.Entities
{
    public class Produto
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = null!;
        public bool Ativo { get; set; }
        public float QtdeMax { get; set; }
        public float QtdeMin { get; set; }
    }
}
