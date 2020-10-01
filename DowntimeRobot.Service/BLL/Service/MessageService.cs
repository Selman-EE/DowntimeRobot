using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public static class MessageService
    {
        /// <summary>
        /// Business response message
        /// </summary>
        public const string Success = "The operation successfully";
        public const string Update = "Updated successfully";
        public const string Remove = "Removed successfully";
        public const string ContactToManager = "There is something critical error. Please save error screen and get screenshot then send to the manager with error page's URL.";
        public const string Error = "Error Occurred During Processing. Please check your action !!!";
        public static string ErrorMessage(this Exception exception)
        {
            if (exception == null)
                return Error;

            return $"Error Occurred During Processing{Environment.NewLine}Date: {DateTime.Now.ToString("yy-mm-dd-hh-MM-ss")}{Environment.NewLine}Message: {exception?.Message ?? string.Empty} {Environment.NewLine}InnerException: {exception?.InnerException?.Message ?? string.Empty}{Environment.NewLine}InnerExceptionSquare: {exception.InnerException?.InnerException?.Message ?? string.Empty}{Environment.NewLine}Stacktrace: {exception.StackTrace}";
        }


        /// <summary>
        /// Http response message and general message
        /// </summary>
        public const string NotFound = "NOT_FOUND";
        public const string NotRecord = "NO_RECORDS_FOUND";
        public const string EntityNotFound = "ENTITY_NOT_FOUND";
        public const string ApiKeyNotFound = "API_KEY_NOT_FOUND";
        public const string InvalidApiKey = "INVALID_API_KEY";
        public const string InvalidToken = "INVALID_ACCESS_TOKEN";
        public const string InvalidAppCode = "INVALID_APP_CODE";
        public const string InvalidSecretKey = "INVALID_SECRET_KEY";
        public const string InvalidRequestHeader = "INVALID_REQUEST_HEADER";
        public const string InvalidRequest = "INVALID_REQUEST";
        public const string UnauthorizedUser = "UNAUTHORIZED_USER";
        public const string MissingAuthorizationToken = "MISSING_AUTHORIZATION_TOKEN";
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string WrongPassword = "WRONG_PASSWORD";
        public const string PasswordNotMatch = "PASSWORD_NOT_MATCH";
        public const string NotValidUsername = "NOT_VALID_USERNAME";
        public const string NotValidName = "NOT_VALID_NAME";
        public const string NotValidPassword = "NOT_VALID_PASSWORD";
        public const string Authentication_Failed = "AUTHENTICATION_FAILED";
        public const string Job_NotFound = "JOB_NOT_FOUND";
    }
}
