using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string role) : base(string.Format("User Tidak Berwenang Sebagai {0}!",role)){ }
    }
}
