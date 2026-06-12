using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Sharing
{
    public class EmailStringBody
    {
        
            public static string Send(string email, string token, string component, string message)
            {
                string encodetoken = Uri.EscapeDataString(token);

                var url = $"http://localhost:4200/{component}?email={email}&code={encodetoken}";

                return $@"
            <html>
                <body style='font-family: Arial, sans-serif; padding: 20px;'>
                    
                    <h1>{message}</h1>

                    <p>Hello, <strong>{email}</strong></p>

                    <p>Click the button below to proceed:</p>

                    <a href='{url}'
                       style='background-color:#4CAF50; color:white; padding:10px 20px; text-decoration:none; border-radius:5px; display:inline-block;'>
                       Verify Now
                    </a>

                    <p style='color:gray; font-size:12px; margin-top:20px;'>
                        If you didn't request this, please ignore this email.
                    </p>

                </body>
            </html>";
            }
        }

    }
    

