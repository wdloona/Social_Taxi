
namespace Social_Taxi.Models
{
    public class BaseModel<T>
    {
        public bool Success => string.IsNullOrEmpty(ErrorMessage);

        public string ErrorMessage { get; set; }

        public T Data {get; set; }

        public BaseModel()
        {

        }

        public BaseModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public BaseModel(T value, string errorMessage = null)
        {
            Data = value;
            ErrorMessage = errorMessage;
        }

    }
}
