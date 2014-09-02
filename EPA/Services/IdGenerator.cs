using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Services
{
    internal class IdGenerator
    {

        static string CreateRandomString(int count)
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(charset[r.Next(charset.Length)]);

                //   yield return charset[r.Next(charset.Length)];
            }
            return sb.ToString();
        }

        static string charsetLowerCase = "abcdefghijklmnopqrstuvwxyz1234567890";
        static string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";


        public static string CreateUniqueKeyForCompany()
        {
            using (var db = new EPA.Data.Db())
            {
                string randomKey;
                do
                {
                    randomKey = CreateRandomString(20);

                } while (db.COMPANIES.Where(a => a.KEY == randomKey).Any());

                return randomKey;

             
            }
        }
    }
}
