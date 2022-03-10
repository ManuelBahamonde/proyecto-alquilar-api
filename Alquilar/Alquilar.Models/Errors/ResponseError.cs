using System.Text.Json;

namespace Alquilar.Models
{
    public class ResponseError
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
