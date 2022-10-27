using System;
using System.Security.Cryptography;
using System.Text;
using Covid_Project.Domain.Services.Authorization;

namespace Covid_Project.Services.Authorization
{
    public class PasswordService : IPasswordService
    {
        public string PasswordDecoder(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] toDecodeByte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
            string result = new string(decodedChar);
            return result;
        }

        public string PasswordEncoder(string password)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));  
            
                //get hash result after compute it  
                byte[] result = md5.Hash;  

                StringBuilder strBuilder = new StringBuilder();  
                for (int i = 0; i < result.Length; i++)  
                {  
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder.Append(result[i].ToString("x2"));  
                }  

                return strBuilder.ToString();

                // byte[] encData_byte = new byte[password.Length];
                // encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                // string encodedData = Convert.ToBase64String(encData_byte);
                // return encodedData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}