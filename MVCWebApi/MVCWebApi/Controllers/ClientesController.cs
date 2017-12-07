using MVCWebApi.Models;
using MVCWebApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCWebApi.Controllers
{
    public class ClientesController : ApiController
    {

        // GET: api/Clientes
        public IEnumerable<Cliente> Get()
        {
            var clientes = new ClienteService().AllClientes();
            if(clientes != null)
            {
                return clientes;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // GET: api/Clientes/5
        public Cliente Get(int id)
        {
            var cliente = new ClienteService().GetClienteById(id);
            if (cliente != null)
            {
                return cliente;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // POST: api/Clientes
        public HttpResponseMessage Post(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                
                var erroresModelo = ModelState.Values.Where(e => e.Errors.Any()).Select(e => e.Errors.First().ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var resultado = new ClienteService().SetCliente(cliente);
            if (resultado)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                var relativePath = "/api/clientes/" + cliente.Id;
                response.Headers.Location = new Uri(Request.RequestUri, relativePath);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT: api/Clientes/5
        public HttpResponseMessage Put(Cliente clienteActualizar)
        {
            var cliente = new ClienteService().UpdateCliente(clienteActualizar);
            if (cliente)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Clientes/5
        public HttpResponseMessage Delete(int id)
        {
            var cliente = new ClienteService().DeleteCliente(id);
            if (cliente)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
