using CustomerApi.Casos_de_Usos;
using CustomerApi.DTOs;
using CustomerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace CustomerApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomersDBContext _customersDBContext;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;
        public CustomerController(CustomersDBContext customersDBContext, IUpdateCustomerUseCase updateCustomerUseCase)
        {
            _customersDBContext=customersDBContext;
            _updateCustomerUseCase=updateCustomerUseCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDTO>))]
        public async Task<IActionResult> GetCustomers()
        {
            var result= _customersDBContext.Customer.Select(c=>c.ToDTO()).ToList();

            return new OkObjectResult(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomerDTO))]
        public async Task<IActionResult> GetCustomer(long id)
        {
            //luego del task es el objeto que se devuelve

            CustomerEntity result = await _customersDBContext.Get(id);

            return new OkObjectResult(result.ToDTO());
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(bool))]
        public async Task<IActionResult> DeleteCustomers(long id)
        {
            var result= await _customersDBContext.Delete(id);

            return new OkObjectResult(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDTO))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customer)
        {
            CustomerEntity result = await _customersDBContext.Add(customer);

            return new CreatedResult($"http://localhost:7211/api/customer/{result.Id}", null);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomerDTO))]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customer)
        {
            CustomerDTO? result=await _updateCustomerUseCase.Execute(customer);
            
            if(result == null)
            {
               return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        

        
    }
}
