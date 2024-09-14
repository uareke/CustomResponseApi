using MemoryCache.API.Controllers.Base;
using MemoryCache.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MemoryCache.API.Controllers.v1
{

    [ApiController]
    public class ProdutoController(IProdutoService service) : BaseController
    {

        private readonly IProdutoService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {

            var retorno = await _service.Get(id);

            return Ok(retorno);
        }
    }
}
