using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bumble_bee_API_2.Models
{
    public class User
    {
        public int USR_ID { get; set; }
        [Required]
        public string? USR_TYPE { get; set; }
        [Required]
        public string? USR_NIC { get; set; }
        [Required]
        public string? USR_FNAME { get; set; }
        [Required]
        public string? USR_LNAME { get; set; }
        [Required]
        public string? USR_EMAIL { get; set; }  
        [Required]
        public string? USR_PWD { get; set; }
        [Required]
        public bool USR_STATUS { get; set; }
    }
}
