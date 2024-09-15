using MemoryCache.API.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemoryCache.API.Controllers.v1
{
    public class TesteController : BaseController
    {

        [HttpGet]
        [Route("ResponseOkCustom")]
        public IActionResult ResponseOkCustom()
        {
            return ResponseOk("PARECE QUE DEU TUDO CERTO", 1, "1.0", "pt-br");
        }


        [HttpGet]
        [Route("ResponseCreatedCustom")]
        public IActionResult ResponseCreatedCustom()
        {
            return ResponseCreated("AQI VEM OBJETO CRIADO", 1, "1.0","pt-br");
        }


        [HttpGet]
        [Route("BadRequestCustom")]
        public IActionResult BadRequestCustom()
        {
            return ResponseBadRequest("MSG DE ERRO", "1.0", "pt-br");
        }


        [HttpGet]
        [Route("NotFoundCustom")]
        public IActionResult NotFoundCustom()
        {
            return ResponseNotFound("NÃO ENCONTRADO");
        }


        [HttpGet]
        [Route("ResponseInternalServerErrorCustom")]
        public IActionResult ResponseInternalServerErrorCustom()
        {
            return ResponseInternalServerError("NÃO ENCONTRADO");
        }

    }
}
