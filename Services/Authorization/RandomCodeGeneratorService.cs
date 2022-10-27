using System;
using Covid_Project.Domain.Services.Authorization;

namespace Covid_Project.Services.Authorization
{
    public class RandomCodeGeneratorService : IRandomCodeGeneratorService
    {
        public string GenerateRandomCode(int codeLength)
        {
            Random rand = new Random();
            // Characters we will use to generate this random string.
            char[] allowableChars = "ABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray();

            // Start generating the random string.
            string activationCode = string.Empty;
            for (int i = 0; i < codeLength; i++) {
                activationCode += allowableChars[rand.Next(allowableChars.Length - 1)];
            }

            // Return the random string in upper case.
            return activationCode.ToUpper();
        }
    }
}