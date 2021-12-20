using Newtonsoft.Json;

namespace AgilityWeb.Common.Exceptions
{
    public class ErrorDetails
    {
        public string TitleError { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}