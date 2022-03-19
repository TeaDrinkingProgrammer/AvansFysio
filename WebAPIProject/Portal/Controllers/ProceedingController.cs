using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioWebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProceedingController : ControllerBase {

        private readonly ILogger<DiagnosisController> _logger;
        private readonly IProceedingRepo _proceedingRepo;

        public ProceedingController(ILogger<DiagnosisController> logger, IProceedingRepo diagnosisRepo) {
            _logger = logger;
            _proceedingRepo = diagnosisRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Proceeding>> Get() {
            return Ok(await _proceedingRepo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Proceeding>> Get(string id) {
            return Ok(await _proceedingRepo.GetWhereId(id));
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Diagnosis>>> Get(string description) {
        //    if (description != null) {
        //        return Ok(await _proceedingRepo.GetWhereDescription(description));
        //    } else {
        //        return BadRequest();
        //    }

        //}
    }
}
