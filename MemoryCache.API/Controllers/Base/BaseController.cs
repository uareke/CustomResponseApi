

using MemoryCache.API.Extension;
using MemoryCache.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MemoryCache.API.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// Cria uma resposta HTTP 200 (OK) com dados e informações adicionais.
        /// </summary>
        protected IActionResult ResponseOk(object result, int totalRecords, string version, string language) =>
            Response(HttpStatusCode.OK, result, totalRecords, null, version, language);

        /// <summary>
        /// Cria uma resposta HTTP 200 (OK) com dados e informações adicionais.
        /// </summary>
        protected IActionResult ResponseOk() =>
            Response(HttpStatusCode.OK);


        /// <summary>
        /// Cria uma resposta HTTP 201 (Created) vazia.
        /// </summary>
        protected IActionResult ResponseCreated() =>
            Response(HttpStatusCode.Created);

        /// <summary>
        /// Cria uma resposta HTTP 201 (Created) vazia.
        /// </summary>
        protected IActionResult ResponseCreated(object data, int totalRecords, string version, string language) =>
            Response(HttpStatusCode.Created, data, totalRecords, version, language);

        /// <summary>
        /// Cria uma resposta HTTP 204 (No Content), indicando que a solicitação foi bem-sucedida,
        /// mas não há nenhum conteúdo a ser retornado.
        /// </summary>
        protected IActionResult ResponseNoContent() =>
            Response(HttpStatusCode.NoContent);

        /// <summary>
        /// Cria uma resposta HTTP 304 (Not Modified), indicando que o recurso não foi modificado
        /// desde a última solicitação.
        /// </summary>
        protected IActionResult ResponseNotModified() =>
            Response(HttpStatusCode.NotModified);

        /// <summary>
        /// Cria uma resposta HTTP 400 (Bad Request) com uma mensagem de erro personalizada.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseBadRequest(string errorMessage) =>
            Response(HttpStatusCode.BadRequest, errorMessage: errorMessage);

        /// <summary>
        /// Cria uma resposta HTTP 400 (Bad Request) com uma mensagem de erro personalizada, 
        /// incluindo informações de versão e idioma.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta.</param>
        /// <param name="version">Versão da API.</param>
        /// <param name="language">Idioma desejado para a resposta.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseBadRequest(string errorMessage, string version, string language) =>
        Response(HttpStatusCode.BadRequest, errorMessage: errorMessage, version: version, language: language);

        /// <summary>
        /// Cria uma resposta HTTP 400 (Bad Request) com uma mensagem padrão indicando que a requisição é inválida.
        /// </summary>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseBadRequest() =>
            Response(HttpStatusCode.BadRequest, errorMessage: "Requisição invalida");

        /// <summary>
        /// Cria uma resposta HTTP 404 (Not Found) com uma mensagem de erro personalizada.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseNotFound(string errorMessage) =>
            Response(HttpStatusCode.NotFound, errorMessage: errorMessage);

        /// <summary>
        /// Cria uma resposta HTTP 404 (Not Found) com uma mensagem padrão indicando que o recurso não foi encontrado.
        /// </summary>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseNotFound() =>
            Response(HttpStatusCode.NotFound, errorMessage: "Recurso não disponivel");

        /// <summary>
        /// Cria uma resposta HTTP 401 (Unauthorized) com uma mensagem de erro personalizada.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseUnauthorized(string errorMessage) =>
            Response(HttpStatusCode.Unauthorized, errorMessage: errorMessage);

        /// <summary>
        /// Cria uma resposta HTTP 500 (Internal Server Error) indicando que ocorreu um erro interno no servidor.
        /// </summary>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseInternalServerError() =>
            Response(HttpStatusCode.InternalServerError);

        /// <summary>
        /// Cria uma resposta HTTP 500 (Internal Server Error) com uma mensagem de erro personalizada,
        /// indicando que ocorreu um erro interno no servidor.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseInternalServerError(string errorMessage) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: errorMessage);

        /// <summary>
        /// Cria uma resposta HTTP 500 (Internal Server Error) com base em uma exceção,
        /// indicando que ocorreu um erro interno no servidor.
        /// </summary>
        /// <param name="exception">Exceção ocorrida no servidor.</param>
        /// <returns>Objeto de Resultado de Ação HTTP.</returns>
        protected IActionResult ResponseInternalServerError(Exception exception) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: exception.Message);

        /// <summary>
        /// Cria um objeto JsonResult com base nos parâmetros fornecidos, gerando uma resposta personalizada.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <param name="data">Dados a serem incluídos na resposta (pode ser nulo).</param>
        /// <param name="totalRecords">Número total de registros, se aplicável (pode ser nulo).</param>
        /// <param name="errorMessage">Mensagem de erro, se houver (pode ser nula ou vazia).</param>
        /// <param name="version">Versão da API (pode ser nula ou vazia).</param>
        /// <param name="language">Idioma desejado para a resposta (pode ser nulo ou vazio).</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>

        protected new JsonResult Response(HttpStatusCode statusCode, object data, int totalRecords, string errorMessage, string version, string language)
        {
            CustomResult result;
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var sucess = statusCode.IsSuccess();

                if (data != null)
                {
                    result = new CustomResult(statusCode, sucess, data, totalRecords, version, language);

                }
                else
                {
                    result = new CustomResult(statusCode, sucess, version, language);
                }
            }
            else
            {
                var errors = new List<string>();
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);
                result = new CustomResult(statusCode, false, errors, version, language);

            }
            return new JsonResult(result) { StatusCode = (int)(result?.StatusCode) };
        }

        /// <summary>
        /// Cria um objeto JsonResult com base nos parâmetros fornecidos, gerando uma resposta personalizada.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <param name="result">Dados a serem incluídos na resposta (pode ser nulo).</param>
        /// <param name="totalRecords">Número total de registros, se aplicável (pode ser nulo).</param>
        /// <param name="version">Versão da API (pode ser nula ou vazia).</param>
        /// <param name="language">Idioma desejado para a resposta (pode ser nulo ou vazio).</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>
        protected new JsonResult Response(HttpStatusCode statusCode, object result, int totalRecords, string version, string language) => Response(statusCode, result, totalRecords, null, version, language);

        /// <summary>
        /// Cria um objeto JsonResult com base nos parâmetros fornecidos, gerando uma resposta personalizada.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <param name="result">Dados a serem incluídos na resposta (pode ser nulo).</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>
        protected new JsonResult Response(HttpStatusCode statusCode, object result) => Response(statusCode, result, 0, null, null);


        /// <summary>
        /// Cria um objeto JsonResult com base no código de status HTTP fornecido, gerando uma resposta personalizada.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>
        protected new JsonResult Response(HttpStatusCode statusCode) => Response(statusCode, null, 0, null, null, null);

        /// <summary>
        /// Cria um objeto JsonResult com base nos parâmetros fornecidos, gerando uma resposta personalizada com uma mensagem de erro,
        /// versão e idioma especificados.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta (pode ser nula ou vazia).</param>
        /// <param name="version">Versão da API (pode ser nula ou vazia).</param>
        /// <param name="language">Idioma desejado para a resposta (pode ser nulo ou vazio).</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>
        protected new JsonResult Response(HttpStatusCode statusCode, string errorMessage, string version, string language) => Response(statusCode, null, 0, errorMessage, version, language);

        /// <summary>
        /// Cria um objeto JsonResult com base nos parâmetros fornecidos, gerando uma resposta personalizada com uma mensagem de erro.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP da resposta.</param>
        /// <param name="errorMessage">Mensagem de erro para incluir na resposta (pode ser nula ou vazia).</param>
        /// <returns>Objeto JsonResult contendo a resposta personalizada.</returns>
        protected new JsonResult Response(HttpStatusCode statusCode, string errorMessage) => Response(statusCode, null, 0, errorMessage, null, null);
    }
}
