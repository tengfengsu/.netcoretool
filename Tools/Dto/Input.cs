using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tools.Dto
{
    public class Input:ValidateInterfaceDto,IInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public interface IInput
    {
        [Required]
        int Id { get; set; }
        [MaxLength(10, ErrorMessage = "姓名不能超过{1}字符")]
        string Name { get; set; }
    }
}
