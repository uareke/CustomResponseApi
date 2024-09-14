using MemoryCache.Domain.Entities;
using MemoryCache.Domain.Interfaces;
using MemoryCache.Infra.Context;
using MemoryCache.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Infra.Repositories
{
    public class ProdutosRepository(AppDBContext context) : BaseRepository<Produto>(context), IProdutosRepository
{

    protected AppDBContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly DbSet<Produto> _dbSet = context.Set<Produto>();

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _dbSet.Where(p => p.Ativo == true).ToListAsync();
        }

        
    }
}
