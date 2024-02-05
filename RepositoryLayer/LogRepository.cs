using Microsoft.EntityFrameworkCore;
using ModelLayer;
using ModelLayer.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class LogRepository
    {
        private DataContext _context;
        public LogRepository(DataContext context)
        {
            _context = context;
        }
        public void RegisterLog(string message, ActionType actionType)
        {
            Log log = new Log();
            log.Message = message;
            log.ActionType = actionType.ToString();
            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
