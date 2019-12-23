using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationAndAuthorization.Util;

namespace AuthenticationAndAuthorization.Data
{
    public class DbInitializer
    {
        public static void Initialize(Context context)
        {
            if (!context.USUARIO.Any() && !context.PERFIL.Any() && !context.PERFIL_USUARIO.Any())
            {
                PERFIL perfil = new PERFIL
                {
                    PER_NOME = "Master"
                };

                USUARIO user = new USUARIO
                {
                    USU_NOME = "Master",
                    USU_EMAIL = "master@email.com",
                    USU_SENHA = MyUtil.ComputeSha256Hash("master")
                };

                PERFIL_USUARIO perfil_usuario = new PERFIL_USUARIO
                {
                    PERFIL = perfil,
                    USUARIO = user
                };

                context.Add(perfil_usuario);
                context.SaveChanges();
            }
        }
    }
}
