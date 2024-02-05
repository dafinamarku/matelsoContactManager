using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Log 
    {
        [Key, Required]
        public Guid Id { get; set; }

        public string Message { get; set; }
        public string ActionType { get; set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

    }
    public enum ActionType
    {
        Error,
        Info
    }
}
