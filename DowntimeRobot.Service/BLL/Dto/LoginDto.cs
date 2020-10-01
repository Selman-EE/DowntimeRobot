using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class LoginDto
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter Your Email Please", AllowEmptyStrings = false)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                        ErrorMessage = "Invalid please check your email")]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password Please", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
