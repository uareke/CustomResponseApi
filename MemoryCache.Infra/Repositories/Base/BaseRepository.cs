using MemoryCache.Domain.Interfaces;
using MemoryCache.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache.Infra.Repositories.Base;

public abstract class BaseRepository<T>(AppDBContext context) : IRepository<T> where T : class
{
    protected readonly AppDBContext _context = context;
    protected DbSet<T> EntitySet => _context.Set<T>();

    private bool _disposed = false;
    private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

    /// <summary>
    /// Altera a entidade fornecida no contexto do banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser alterada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a entidade alterada.</returns>

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }


    /// <summary>
    /// Exclui a entidade fornecida do contexto do banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser excluída.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a entidade excluída.</returns>

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Remove(entity);

        return await Task.FromResult(entity);
    }


    /// <summary>
    /// Inclui a entidade fornecida no contexto do banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser incluída.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a entidade incluída.</returns>

    public async Task<T> CreateAsync(T entity)
    {
        var entidadeCriada = await _context.AddAsync(entity);
        if (entidadeCriada == null) return Activator.CreateInstance<T>();

        return await Task.FromResult(entity);
    }

    /// <summary>
    /// Lista todas as entidades do tipo especificado no contexto do banco de dados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo uma lista de todas as entidades.</returns>
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Retorna a entidade do tipo especificado com o identificador fornecido.
    /// </summary>
    /// <param name="Id">O identificador da entidade a ser recuperada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a entidade com o identificador fornecido.</returns>

    public async Task<T> Get(long Id)
    {
        var entity = await _context.Set<T>().FindAsync(Id);
        return entity;
    }


    /// <summary>
    /// Salva todas as alterações feitas no contexto do banco de dados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona de salvar as alterações.</returns>

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Libera os recursos não gerenciados utilizados por esta instância.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // Certifique-se de que isso esteja chamando 'this'
    }

    /// <summary>
    /// Libera os recursos não gerenciados utilizados por esta instância, opcionalmente liberando também os recursos gerenciados.
    /// </summary>
    /// <param name="disposing">Indica se deve liberar os recursos gerenciados além dos recursos não gerenciados.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _safeHandle?.Dispose();
        }
        _disposed = true;
    }

    // Finalizer (destructor)
    ~BaseRepository()
    {
        Dispose(false);
    }

}
