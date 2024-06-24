using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentation.Service
{
    internal class ApiService
    {
        public static string GetMembers()
        {
            try
            {


                return null;
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
