using Microsoft.AspNetCore.Http;

namespace MoneyChanger.Martin.Sun
{
    public class JsonResult
    {
        public int HttpStatusCode {get;set;}
        public string Message {get;set;}
        public object Data { get; set; }
    }
}
