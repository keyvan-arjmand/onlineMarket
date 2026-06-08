using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
       [Key] public int Id { get; set; }
       [JsonIgnore] 
       public bool IsDelete { get; set; }= false;
    }
}
