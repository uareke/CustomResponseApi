using MemoryCache.Application.Services.Interfaces;
using MemoryCache.Domain.Entities;
using MemoryCache.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Application.Services;

public class ProdutoService: IProdutoService
{
    private readonly IMemoryCache _cache;
    private readonly IProdutosRepository _produtoRepository;

    public ProdutoService(IMemoryCache cache, IProdutosRepository produtoRepository)
    {
        _cache = cache;
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto?> Get(long Id)
    {
        string cacheKey = $"Produto_{Id}";
        if (!_cache.TryGetValue(cacheKey, out Produto? produto))
        {
            produto = await _produtoRepository.Get(Id);

            if (produto != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                _cache.Set(cacheKey, produto, cacheOptions);
            }
        }

        return produto;
    }

    public async Task<IEnumerable<Produto>> GetAll()
    {
        // Tenta obter os produtos do cache
        if (!_cache.TryGetValue("ProdutosCacheKey", out IEnumerable<Produto> produtos))
        {
            // Caso não esteja no cache, busca do banco de dados
            produtos = await _produtoRepository.GetAll();

            // Adiciona ao cache por 10 minutos
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            _cache.Set("ProdutosCacheKey", produtos, cacheOptions);
        }

        return produtos;
    }
}
