using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Models
{
    public class PERFIL_USUARIO
    {
        public int PER_ID{ get; set; }
        public PERFIL PERFIL { get; set; }
        public int USU_ID { get; set; }
        public USUARIO USUARIO { get; set; }

    }
}
