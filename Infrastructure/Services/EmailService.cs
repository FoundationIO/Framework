/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Framework.Infrastructure.Interfaces.Services;
using Framework.Infrastructure.Models.Result;
using Framework.Infrastructure.Utils;

namespace Framework.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public ReturnModel<bool> Send(string smtpHost, int port, string userName, string password, bool useSSL, string fromDisplayName, string fromEmail, string toDisplayName, string toEmail, string ccEmail, string bccEmail, string subject, string body)
        {
            try
            {
                using (var client = port == 0 ? new SmtpClient(smtpHost) : new SmtpClient(smtpHost, port))
                {
                    var mmail = new MailMessage(new MailAddress(fromEmail, fromDisplayName), new MailAddress(toEmail, toDisplayName))
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        Body = body
                    };

                    mmail.SubjectEncoding = Encoding.UTF8;
                    if (mmail.Subject.IsTrimmedStringNullOrEmpty())
                        mmail.Subject = subject;

                    if (!ccEmail.IsTrimmedStringNullOrEmpty())
                    {
                        mmail.CC.Add(ccEmail);
                    }

                    if (!bccEmail.IsTrimmedStringNullOrEmpty())
                    {
                        mmail.CC.Add(bccEmail);
                    }

                    if ((!userName.IsTrimmedStringNullOrEmpty()) && (!password.IsTrimmedStringNullOrEmpty()))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(userName, password);
                    }

                    if (useSSL)
                        client.EnableSsl = true;

                    client.Send(mmail);

                    return new ReturnModel<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new ReturnModel<bool>("Unexpected error when sending email.", ex);
            }
        }
    }
}
