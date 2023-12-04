using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;


namespace PaswordGeneration
{
    public class MyPasswordGenerator
    {
        [Function("MyPasswordGenerator")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            const int passwordLength = 12; // Longitud de la contraseña
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+"; // Caracteres válidos para la contraseña

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[passwordLength];
                rng.GetBytes(randomBytes);

                char[] chars = new char[passwordLength];
                int validCharCount = validChars.Length;

                for (int i = 0; i < passwordLength; i++)
                {
                    chars[i] = validChars[randomBytes[i] % validCharCount];
                }

                return new string(chars);
            }
        }
    }
}
