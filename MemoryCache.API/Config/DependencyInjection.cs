using MemoryCache.Application.Services.Interfaces;
using MemoryCache.Domain.Enum;
using MemoryCache.Domain.Filter;
using MemoryCache.Infra.Repositories;
using System.Reflection;

namespace MemoryCache.API.Config;

/// <summary>
/// Arquivo de configuração da injecão de dependencia automatica
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra automaticamente todos os serviços e suas implementações encontradas no assembly de serviços.
    /// O tipo de injeção (Scoped, Transient, Singleton) é definido por um atributo personalizado <see cref="TipoInjecaoAttribute"/> na interface do serviço.
    /// Se nenhum atributo for encontrado, o tipo padrão de injeção será Scoped.
    /// </summary>
    /// <param name="services">A coleção de serviços a ser configurada.</param>
    /// <returns>A coleção de serviços configurada.</returns>
    /// <exception cref="ArgumentException">Lançado quando um tipo de injeção inválido é especificado no atributo <see cref="TipoInjecaoAttribute"/>.</exception>

    public static IServiceCollection ResolveDependencias(this IServiceCollection services)
    {
        #region SERVICES
        var serviceAssembly = typeof(IHelperService).Assembly;

        // Registrar serviços do namespace "Gerencial360.Domain.Services"
        var registrationsService =
            from type in serviceAssembly.GetExportedTypes()
            where (type.Namespace?.StartsWith("MemoryCache.Application.Services") ?? false) && !type.IsAbstract
            from service in type.GetInterfaces()
            select new { service, implementation = type };

        foreach (var regService in registrationsService)
        {
            var tipoInjecaoAttr = regService.service.GetCustomAttribute<TipoInjecaoAttribute>();

            if (tipoInjecaoAttr != null)
            {
                // Define o tipo de injeção baseado no atributo
                switch (tipoInjecaoAttr.Tipo)
                {
                    case InjectionType.Scoped:
                        services.AddScoped(regService.service, regService.implementation);
                        break;
                    case InjectionType.Transient:
                        services.AddTransient(regService.service, regService.implementation);
                        break;
                    case InjectionType.Singleton:
                        services.AddSingleton(regService.service, regService.implementation);
                        break;
                    default:
                        throw new ArgumentException($"Tipo de injeção '{tipoInjecaoAttr.Tipo}' não é válido para {regService.service.Name}");
                }
            }
            else
            {
                // Se nenhum atributo for encontrado, usa o padrão Scoped
                services.AddScoped(regService.service, regService.implementation);
            }
        }
        #endregion

        //#region MAP
        //var mapAssembly = typeof(MapProduto).Assembly;
        //var registrationsMap =
        //    from type in mapAssembly.GetExportedTypes()
        //    where type.Namespace?.StartsWith("Gerencial360.Core.Map") == true && !type.IsAbstract
        //    select type;

        //foreach (var mapType in registrationsMap)
        //{
        //    services.AddScoped(mapType);
        //}
        //#endregion MAP

        #region REPOSITORIOS
        var assembly = typeof(ProdutosRepository).Assembly;
        var typesToRegister = assembly.GetExportedTypes()
            .Where(type => type.Namespace == "MemoryCache.Infra.Repositories" && !type.IsInterface && !type.IsAbstract);

        foreach (var type in typesToRegister)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(p => !p.Name.Contains("IRepository"));
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, type);
            }
            else
            {
                services.AddScoped(type);
            }
        }
        #endregion REPOSITORIOS

        return services;
    }
}
