/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Framework.Infrastructure.Models.Result;

namespace Framework.Infrastructure.Interfaces.Services
{
    public interface IEmailService
    {
        ReturnModel<bool> Send(string smtpHost, int port, string userName, string password, bool useSSL, string fromDisplayName, string fromEmail, string toDisplayName, string toEmail, string ccEmail, string bccEmail, string subject, string body);
    }
}