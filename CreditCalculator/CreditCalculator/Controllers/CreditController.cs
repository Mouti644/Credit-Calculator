using CreditCalculator.Models;
using CreditCalculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CreditCalculator.Controllers
{
    public class CreditController : ApiController
    {
        private readonly CreditCalculatorService _service;

        public CreditController()
        {
            _service = new CreditCalculatorService();
        }

        // POST api/credit/calculate
        [HttpPost]
        [Route("api/credit/calculate")]
        public IHttpActionResult Calculate([FromBody] CreditRequest request)
        {
            if (request == null)
                return BadRequest("Requête invalide");

            try
            {
                CreditResult result = _service.Calculer(request);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
