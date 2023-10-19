using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Models
{
    public class HttpMessage
    {
        public ErrorMessage GetOops()
        {
            ErrorMessage errmsg = new ErrorMessage();
            errmsg.message = "Oops something went wrong !!!";
            return errmsg;
        }
        public string GetLogin(string s)
        {
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : null;
        }
    }
    public class ErrorMessage
    {
        public string message { get; set; }
    }
    public static class Status
    {
        public const string ok = "ok";
        public const string OK = "OK";     
    }
    public class MessageStatus
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

}
