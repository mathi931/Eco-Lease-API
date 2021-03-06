using EcoLease_API.Models;
using EcoLease_API.Repositories;
using EcoLease_API.Validators;
using FluentValidation.Results;
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
        //variables to reach the repositories
        private readonly IAgreementRepository _agreementRepository;
        private readonly IReservationRepository _reservationRepository;

        public AgreementsController(IAgreementRepository agreementRepository, IReservationRepository reservationRepository)
        {
            _agreementRepository = agreementRepository;
            _reservationRepository = reservationRepository;
        }

        //instance of validator
        IDValidator validatorID = new IDValidator();
        FileValidator validatorFileName = new FileValidator();

        // GET api/Agreements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agreement>> GetAgreement(int id)
        {
            ValidationResult validation = validatorID.Validate(id);
            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            var agrWithoutReservation = await _agreementRepository.GetByID(id);

            return new Agreement {
                AID = agrWithoutReservation.AID,
                FileName = agrWithoutReservation.FileName,
                Reservation = await _reservationRepository.GetByID(id)
            };
            
        }

        // POST api/Agreements
        [HttpPost]
        public async Task<ActionResult<Agreement>> InsertAgreement([FromBody] Agreement agreement)
        {
            ValidationResult validationID = validatorID.Validate(agreement.Reservation.RID);
            ValidationResult validationFileName = validatorFileName.Validate(agreement.FileName);
            if (!validationID.IsValid || !validationFileName.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            var newAgreement = await _agreementRepository.Insert(agreement);
            newAgreement.Reservation = await _reservationRepository.GetByID(agreement.Reservation.RID);
            return CreatedAtAction(nameof(GetAgreement), new { id = newAgreement.AID }, newAgreement);
        }

        // DELETE api/Agreements
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAgreement(int id)
        {
            ValidationResult validation = validatorID.Validate(id);
            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

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
