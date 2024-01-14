namespace FileUploadService.API.Models
{
    public class ResponseModels
    {
        public string? RESULT_CODE { get; set; }
        public string? RESULT_DESC { get; set; }
        public object? RESULT_DATA { get; set; }

        public static ResponseModels Success(object result_data, string result_code = "200", string result_desc = "Success")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc,
                RESULT_DATA = result_data
            };
        }

        public static ResponseModels Success(string? result_desc = "Success")
        {
            return new ResponseModels
            {
                RESULT_CODE = "200",
                RESULT_DESC = result_desc,
                RESULT_DATA = null
            };
        }
        public static ResponseModels Error(string? result_desc = null, string? result_code = "500")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc ?? "Error",
                RESULT_DATA = null
            };
        }

        public static ResponseModels Forbidden(string? result_desc = null, string? result_code = "403")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc ?? "Forbidden",
                RESULT_DATA = null
            };
        }

        public static ResponseModels Unauthorized(string? result_desc = null, string? result_code = "401")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc ?? "Unauthorized",
                RESULT_DATA = null
            };
        }

        public static ResponseModels NotFound(string? result_desc = null, string? result_code = "404")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc ?? "NotFound",
                RESULT_DATA = null
            };
        }
        public static ResponseModels BadRequest(string? result_desc = null, string? result_code = "400")
        {
            return new ResponseModels
            {
                RESULT_CODE = result_code,
                RESULT_DESC = result_desc ?? "BadRequest",
                RESULT_DATA = null
            };
        }

    }
}
