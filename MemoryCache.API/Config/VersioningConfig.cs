using Asp.Versioning;

namespace MemoryCache.API.Config;

/// <summary>
/// Classe estática que fornece métodos de extensão para configurar o versionamento de API no serviço.
/// </summary>
public static class VersioningConfig
{
    /// <summary>
    /// Configura o versionamento de API e explora as versões disponíveis para o serviço.
    /// </summary>
    /// <param name="services">A coleção de serviços para a qual o versionamento de API será configurado.</param>
    /// <returns>A coleção de serviços após a configuração do versionamento de API.</returns>

    public static IServiceCollection ApiVersioning(this IServiceCollection services)
    {

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1); //Define a versão padrão da API. Normalmente, será v1.0
            options.ReportApiVersions = true; //Relata as versões de API suportadas no api-supported-versionscabeçalho de resposta.
            options.AssumeDefaultVersionWhenUnspecified = true; // Usa DefaultApiVersionquando o cliente não forneceu uma versão explícita.
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")); //Configura como ler a versão da API especificada pelo cliente. O valor padrão é QueryStringApiVersionReader.
        })
        .AddMvc() // This is needed for controllers
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        }); //método é útil se você estiver usando o Swagger. Ele consertará as rotas de endpoint e substituirá o parâmetro de rota da versão da API.

        return services;
    }
}
