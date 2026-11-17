using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.DTO
{
    public  class EmailDto
    {
        public EmailDto(string to , string from, string subject, string content )
        {
            To = to;
            From = from;
            Subject = subject;
            Content = content;
            //Code = code;
        }

        public string To { get; set; }
        public string? From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        //public string Code { get; set; }

    }
}
