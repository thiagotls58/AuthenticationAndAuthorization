using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Models
{
    public class USUARIO
    {
        public int USU_ID { get; set; }
        public string USU_NOME { get; set; }
        public string USU_EMAIL { get; set; }
        public string USU_SENHA { get; set; }
        public ICollection<PERFIL_USUARIO> PERFIL_USUARIO { get; set; }

    }
}
