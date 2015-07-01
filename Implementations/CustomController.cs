using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class CustomController: ICustomController
    { 
        public IRepository Repository { get; set; }
        public IEmailService EmailService { get; set; }
        public CustomController(IRepository repository, IEmailService emailService)
        {
            Repository = repository;
            EmailService = emailService;
        }
    }
}
