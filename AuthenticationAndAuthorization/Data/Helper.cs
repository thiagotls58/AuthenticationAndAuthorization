using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data
{
    public class Helper
    {
        public static List<List<object>> RawSqlQuery(MyContext db, string query, Dictionary<string, object> paramsSql, string[] columns)
        {
            using (DbCommand command = db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                // Adicionando os parâmetros
                foreach (var item in paramsSql)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = item.Key;
                    param.Value = item.Value;
                    command.Parameters.Add(param);
                }

                db.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    List<List<object>> entities = new List<List<object>>();

                    while (result.Read())
                    {
                        List<object> attributesEntity = new List<object>();

                        if (columns == null || columns.Length == 0)
                        {
                            int qtdColunas = result.FieldCount;
                            for (int i = 0; i < qtdColunas; i++)
                            {
                                attributesEntity.Add(result.GetValue(i));
                            }
                        }
                        else if (columns.Length > 0)
                        {
                            foreach (string col in columns)
                            {
                                attributesEntity.Add(result.GetValue(result.GetOrdinal(col)));
                            }
                        }

                        if (attributesEntity.Count > 0)
                        {
                            entities.Add(attributesEntity);
                        }
                    }
                    return entities;
                }
            }
        }
    }
}
