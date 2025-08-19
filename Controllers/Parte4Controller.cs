using Microsoft.AspNetCore.Mvc;
using ProvaPub.Entities;
using ProvaPub.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
    /// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
    /// Tente criar a maior cobertura possível nos testes.
    /// 
    /// Utilize o framework de testes que desejar. 
    /// Crie o teste na pasta "Tests" da solution
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte4Controller : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public Parte4Controller(ICustomerService customerService, TestDbContext ctx)
        {
            _customerService = customerService;
        }

        [HttpGet("CanPurchase")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CanPurchase([FromQuery] int customerId, [FromQuery] decimal purchaseValue)
        {
            try
            {
                bool result = await _customerService.CanPurchase(customerId, purchaseValue);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro inesperado. {ex.Message}");
            }
        }
    }
}
