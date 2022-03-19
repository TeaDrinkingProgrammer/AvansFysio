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
    public class DiagnosisController : ControllerBase {

        private readonly ILogger<DiagnosisController> _logger;
        private readonly IDiagnosisRepo _diagnosisRepo;

        public DiagnosisController(ILogger<DiagnosisController> logger, IDiagnosisRepo diagnosisRepo) {
            _logger = logger;
            _diagnosisRepo = diagnosisRepo;

        }

        [HttpGet]
        public async Task<ActionResult<Diagnosis>> Get() {
            return Ok(await _diagnosisRepo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnosis>> Get(int id) {
            return Ok(await _diagnosisRepo.GetWhereId(id));
        }
        //[HttpGet]
        //[Route("/{pathology=pathology}")]
        //public async Task<ActionResult<IEnumerable<Diagnosis>>> Get([FromQuery] string pathology) {
        //    if (pathology != null) {
        //        return Ok(await _diagnosisRepo.GetWherePathology(pathology));
        //    } else {
        //        return BadRequest("");
        //    }
        //}
    }
}
