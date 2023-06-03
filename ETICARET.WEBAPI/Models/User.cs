using System.ComponentModel.DataAnnotations;

namespace ETICARET.WEBAPI.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }
       
        [Required]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
