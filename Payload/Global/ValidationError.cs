namespace ASPNET_Core7_WebAPI_JWT.Payload.Global {
    public class ValidationError  
    {  
        public string? Field { get; }  
        public int Code { get; set; }  

        public string? Message { get; }  

        public ValidationError(string field,int code, string message)  
        {  
            Field = field != string.Empty ? field : null;  
            Code = code != 0 ? code : 1;  //set the default code to 55. you can remove it or change it to 400.  
            Message = message;  
        }  
    }  
}