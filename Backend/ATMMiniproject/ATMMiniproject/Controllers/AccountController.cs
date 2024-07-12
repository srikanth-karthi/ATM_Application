using ATMMiniproject.Exceptions;
using ATMMiniproject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMMiniproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       
           private readonly IAccountService _accountService;

            public AccountController(IAccountService accountService)
            {
                _accountService = accountService;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(int id, string pin)
            {
                try
                {
                    var token = await _accountService.Login(id, pin);
                    return Ok(new { Token = token });
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(new { Message = ex.Message });
                }
            catch (NoSuchIteminDbException ex)
            {
                return StatusCode(404, new { Error = ex.Message });
            }
            catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred while processing your request." , Error = ex.Message });
                }
         
            }
             [Authorize]
            [HttpPost("withdraw")]
            public async Task<IActionResult> Withdraw(int amount)
            {
                try
                {
                    var transaction = await _accountService.Withdraw(amount);
                    return Ok(transaction);
                }
                catch (ExceedAmountException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred while processing your request." , Error = ex.Message });
                }
            }
            [Authorize]
            [HttpPost("deposit")]
            public async Task<IActionResult> Deposit(int amount)
            {
                try
                {
                    var transaction = await _accountService.Deposit(amount);
                    return Ok(transaction);
                }
                catch (ExceedAmountException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message ="An error occurred while processing your request.", Error = ex.Message });
                     
                }
            }
        [Authorize]
        [HttpPost("CheckBalance")]
        public async Task<IActionResult> CheckBalanace()
        {
            try
            {
                var balance = await _accountService.GetAccountBalance();
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("gettransactionhistory")]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _accountService.GetAllTransaction();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }
    }
}
