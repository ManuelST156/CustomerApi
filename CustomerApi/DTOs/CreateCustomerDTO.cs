using System.ComponentModel.DataAnnotations;

namespace CustomerApi.DTOs
{
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage ="El nombre propio tiene que ingresarse")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido propio tiene que ingresarse")]
        public string LastName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage ="Tienes que ingresar correo")]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

    }
}