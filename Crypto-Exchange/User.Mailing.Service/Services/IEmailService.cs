using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailing.Service.Models;

namespace Mailing.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
