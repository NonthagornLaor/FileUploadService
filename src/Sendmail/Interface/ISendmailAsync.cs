using Sendmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendmail.Interface
{
    public interface ISendmailAsync
    {
        Task SendEmailAsync(Message message);
    }
}
