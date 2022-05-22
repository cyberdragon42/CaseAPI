using CaseAPI.BusinessLogic.Dto;
using CaseAPI.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly CaseService caseService;

        public CaseController(CaseService service)
        {
            caseService = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] AccountDto dto)
        {
            await caseService.CreateAccountAsync(dto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] ContactDto dto)
        {
            await caseService.CreateContactAsync(dto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCase([FromBody] CaseDto dto)
        {
            await caseService.CreateCaseAsync(dto);
            return Ok();
        }
    }
}
