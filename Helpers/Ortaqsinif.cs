using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogsayt.Models;
using Blogsayt;


namespace Blogsayt.Helpers
{
    public class Ortaqsinif
    {
        databaseblog db = new databaseblog();

        public static bool EditizinyetkiVARMI(int ID,tbl_kullanici user)
        {

            if (user.id == ID)
            {
                return true;
            }
            else if (user.yetkiid > 2)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteizinyetkiVARMI(int ID, tbl_kullanici user)
        {
            //int ID olaraq yazilmis qisim kullanicinin id-sidir.
            if (user.id == ID)
            {
                return true;
            }
            else if (user.yetkiid > 2)
            {
                return true;
            }
            return false;
        }
    }
}