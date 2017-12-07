using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVCWebApi.ConexionBD
{
    public class Conexion
    {
        public string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["wsclientes"].ConnectionString;
        }
    }
}