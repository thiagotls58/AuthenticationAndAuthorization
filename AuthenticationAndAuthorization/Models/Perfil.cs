using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Models
{
    public class PERFIL
    {
        public int PER_ID { get; set; }
        public string PER_NOME { get; set; }
        public List<PERFIL_USUARIO> PERFIL_USUARIO { get; set; }
    }
}
