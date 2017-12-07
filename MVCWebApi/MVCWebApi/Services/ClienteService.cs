using MVCWebApi.ConexionBD;
using MVCWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCWebApi.Services
{
    public class ClienteService
    {
        public string cadenaConexion
        {
            get
            {
                return new Conexion().getConnection();
            }
        }

        public List<Cliente> AllClientes()
        {
            List<Cliente> lstClientes = null;
            Cliente clientes = null;

            try
            {
                using(SqlConnection sql=new SqlConnection(this.cadenaConexion))
               {
                    using(SqlCommand cmd=new SqlCommand("SP_GetClientesLista", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Open();
                        var rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            lstClientes = new List<Cliente>();
                            while (rd.Read())
                            {
                                clientes = new Cliente();
                                clientes.Id = (int)rd["Id"];
                                clientes.Nombre = (string)rd["Nombre"];
                                clientes.User = (string)rd["Usuario"];
                                lstClientes.Add(clientes);
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return lstClientes;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;
            try
            {
                using (SqlConnection sql = new SqlConnection(this.cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetClientesById", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Open();
                        cmd.Parameters.Add(new SqlParameter("id", id));
                        var rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                cliente = new Cliente();
                                cliente.Id = (int)rd["Id"];
                                cliente.Nombre = (string)rd["Nombre"];
                                cliente.User = (string)rd["Usuario"];
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return cliente;
        }

        public bool SetCliente(Cliente cliente)
        {
            bool resultado = true;
            try
            {
                using(SqlConnection sql=new SqlConnection(this.cadenaConexion))
                {
                    using(SqlCommand cmd=new SqlCommand("SP_CreateCliente", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@usuario", cliente.User));
                        sql.Open();
                        var respuesta = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();
                        if (respuesta != 0) transaccion.Commit();
                        else transaccion.Rollback();
                    }
                }
            }catch
            {
                resultado = false;
            }
            return resultado;
        }


        public bool UpdateCliente(Cliente cliente)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection sql = new SqlConnection(this.cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateCliente", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", cliente.Id));
                        cmd.Parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@usuario", cliente.User));
                        sql.Open();
                        var respuesta = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();
                        if (respuesta != 0) transaccion.Commit();
                        else transaccion.Rollback();
                    }
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }


        public bool DeleteCliente(int id)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection sql = new SqlConnection(this.cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteCliente", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        sql.Open();
                        var respuesta = cmd.ExecuteNonQuery();
                        var transaccion = sql.BeginTransaction();
                        if (respuesta != 0) transaccion.Commit();
                        else transaccion.Rollback();
                    }
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }

    }
}