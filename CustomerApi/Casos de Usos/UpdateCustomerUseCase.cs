using CustomerApi.DTOs;
using CustomerApi.Repositories;

namespace CustomerApi.Casos_de_Usos
{
    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDTO?> Execute(CustomerDTO customerDTO);
    }



    public class UpdateCustomerUseCase: IUpdateCustomerUseCase
    {
        private readonly CustomersDBContext _customersDBContext;


        public UpdateCustomerUseCase(CustomersDBContext customersDBContext)
        {
            _customersDBContext = customersDBContext;
        }
        public async Task<CustomerDTO?> Execute(CustomerDTO customerDTO)
        {
            var entity = await _customersDBContext.Get(customerDTO.Id);

            if (entity == null) 
            { 
                return null; 
            }

            entity.FirstName = customerDTO.FirstName;
            entity.LastName = customerDTO.LastName;
            entity.Email = customerDTO.Email;
            entity.Phone = customerDTO.Phone;
            entity.Address = customerDTO.Address;

            await _customersDBContext.Actualizar(entity);

            return entity.ToDTO();


        }




    }
}
