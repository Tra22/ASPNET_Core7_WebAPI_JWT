using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASPNET_Core7_WebAPI_JWT.Payload.Global {
    public class ValidationResultModel {
        public string Message { get; }  

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)  
        {  
            Message = "Validation Failed"; 
            Errors = modelState.Select(
                    x => new ValidationError(x.Key, 0, x.Value is null ? new List<string>() : x.Value.Errors.Select(err => err.ErrorMessage).ToList())
                ).ToList(); 
        }  
    }
}