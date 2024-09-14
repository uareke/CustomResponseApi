using MemoryCache.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MemoryCache.API.Config;

/// <summary>
/// Classe estática que fornece métodos de extensão para configurar serviços relacionados ao banco de dados.
/// </summary>
public static class DbConfig
{
    /// <summary>
    /// Configura o serviço de banco de dados utilizando o MySQL como provedor.
    /// </summary>
    /// <param name="services">A coleção de serviços para a qual o contexto do banco de dados será adicionado.</param>
    /// <param name="_config">A configuração usada para obter a string de conexão com o banco de dados.</param>

    public static void AddDbConfiguration(this IServiceCollection services, IConfiguration _config)
    {
        var connectionString = _config.GetConnectionString("mysql");
        services.AddDbContext<AppDBContext>(options =>
            options.UseMySql(connectionString,
                new MySqlServerVersion(new Version(8, 0, 26)))); // Versão do MySQL
    }
}
