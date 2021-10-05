using EcoLease_API.Models;
using EcoLease_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementsController : ControllerBase
    {
        private readonly IAgreementRepository _agreementRepository;

        public AgreementsController(IAgreementRepository agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        // GET api/Agreements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agreement>> GetAgreement(int id)
        {
            return await _agreementRepository.GetByID(id);
        }

        // POST api/Agreements
        [HttpPost]
        public async Task<ActionResult<Agreement>> InsertAgreement([FromBody] Agreement agreement)
        {
            var newAgreement = await _agreementRepository.Insert(agreement);
            return CreatedAtAction(nameof(GetAgreement), new { id = newAgreement.AID }, newAgreement);
        }

        // DELETE api/Agreements
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAgreement(int id)
        {
            var agreementToDelete = await _agreementRepository.GetByID(id);
            if (agreementToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _agreementRepository.Remove(agreementToDelete.AID);
                return NoContent();
            }
        }
    }
}
