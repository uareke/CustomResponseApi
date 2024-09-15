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
        [Route("Get")]
        public async Task<IActionResult> Get(long id)
        {
            var retorno = await _service.Get(id);
            if (retorno is null) return ResponseNotFound("Produto não encontrado");
            return ResponseOk(retorno,1, "1.0","pt-br");
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var retorno = await _service.GetAll();

            return ResponseOk(retorno, retorno.Count(), "1.0", "pt-br");
        }


    }
}
