using System.Text.RegularExpressions;

namespace SQNBack.Utils
{
    public class GeneralValidatons
    {
        public const string ROUTE = "SQN/rest/";
        public static ApiError ValidateObjectId(string id)
        {
            if (!Regex.IsMatch(id, "^[0-9a-fA-F]{24}$"))
                // La cadena no cumple con el formato esperado
                return new ApiError("Error: The id " + id + " isn't a valid ObjectId.",
                    SQNErrorCode.ValidationError);
            // La cadena tiene un formato válido para ObjectId
            return new ApiError();
        }

        public static bool ValidateEmail(string email)
        {
            // Expresión regular para validar el formato del correo electrónico
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if(!Regex.IsMatch(email, pattern))
                return false;
            return true;
        }
    }
}
