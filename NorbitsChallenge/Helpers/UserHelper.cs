using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Helpers
{
    /*
    Ein metode som skal hente ein ID,
    Returnerer 1, uansett.
    I stedet for kunne en hatt noe som hentet fra cookies, localStorage?
    Hatt ein mer statisk åpningsside, og tastet inn navnet på bedriften i stedet?
    Ignorert autentisering...

     
     */
    public static class UserHelper
    {
        public static int GetLoggedOnUserCompanyId()
        {
            return 1;
        }
    }
}
