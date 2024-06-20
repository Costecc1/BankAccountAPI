using AutoMapper;
using BankAccountAPI.Data;
using BankAccountAPI.Models;
using BankAccountAPI.Models.Dto;
using BankAccountAPI.Repository;
using BankAccountAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class BankAccountApiController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepository;
		private ResponseDto _response;
        private IMapper _mapper;
        private ILogger<BankAccountApiController> _logger;

        public BankAccountApiController(IBankAccountRepository bankAccountRepository, IMapper mapper, ILogger<BankAccountApiController> logger)
        {
			_bankAccountRepository = bankAccountRepository;              
            _response = new ResponseDto();
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Get()
        {
            try
            {
				IEnumerable<BankAccount> objList = _bankAccountRepository.GetAllAccounts().ToList();
				var result = _mapper.Map<IEnumerable<BankAccountDto>>(objList);
				return Ok(new ResponseDto { IsSuccess = true, Result = result });
			}
            catch (Exception ex) 
            {
				_logger.LogError(ex, Message.ErrorGettingBankAccounts);
				_response.IsSuccess = false;
                _response.Message = ex.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }

        [HttpGet]
        [Route("{id:int}")]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Get(int id)
        {
            try
            {
				BankAccount obj = _bankAccountRepository.GetAccountById(id);
				if (obj == null)
				{
					return NotFound();
				}
				var result = _mapper.Map<BankAccountDto>(obj);
				return Ok(new ResponseDto { IsSuccess = true, Result = result });
			}
            catch (Exception ex)
            {
				_logger.LogError(ex, Message.ErrorGettingBankAccountByID(id));

				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }

        [HttpGet]
        [Route("number/{accountNumber}")]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Get(string accountNumber)
        {
            try
            {
                BankAccount obj = _bankAccountRepository.GetAccountByNumber(accountNumber);
                if(obj == null)
                {
                    _response.IsSuccess = false;
                }
				var result = _mapper.Map<BankAccountDto>(obj);
				return Ok(new ResponseDto { IsSuccess = true, Result = result });

			}
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.ErrorGettingBankAccountByAccountNumber(accountNumber));
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }

        [HttpPost]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Post([FromBody] BankAccountDto bankAccountDto)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					_response.IsSuccess = false;
					_response.Message = BadRequest().ToString();
				}

				BankAccount obj = _mapper.Map<BankAccount>(bankAccountDto);
                _bankAccountRepository.CreateAccount(obj);

				var result = _mapper.Map<BankAccountDto>(obj);
				return CreatedAtAction(nameof(Get), new { id = result.BankAccountId }, new ResponseDto { IsSuccess = true, Result = result });

			}
            catch (Exception ex)
            {
				_logger.LogError(ex, Message.ErrorCreatingBankAccount);
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }

        [HttpPut]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Put([FromBody] BankAccountDto bankAccountDto)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					_response.IsSuccess = false;
                    _response.Message = BadRequest().ToString();
				}

				BankAccount obj = _mapper.Map<BankAccount>(bankAccountDto);
				_bankAccountRepository.UpdateAccount(obj);

				var result = _mapper.Map<BankAccountDto>(obj);
				return Ok(new ResponseDto { IsSuccess = true, Result = result });

			}
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }

        [HttpDelete]
        [Route("{id:int}")]
		[Produces("application/json", "application/xml")]
		public ActionResult<ResponseDto> Delete(int id)
        {
            try
            {
                _bankAccountRepository.DeleteAccount(id);
				return NoContent();

			}
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { IsSuccess = false, Message = ex.Message });
			}
        }
    }
}