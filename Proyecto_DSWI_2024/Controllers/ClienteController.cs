using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System.Text;
using Proyecto_DSWI_2024.Models;


namespace S06LaboratorioDSWI_01.Controllers
{
    public class ClienteController : Controller
    {
        public async Task<IActionResult> ListarClientes()
        {
            List<Cliente> lista;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7015/api/Cliente/");
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse);

                }
                else
                {
                    lista = new List<Cliente>();
                }

            }
            return View(await Task.Run(() => lista));
        }



        public async Task<IActionResult> CrearCliente()

        {

            return View(await Task.Run(() => new Cliente()));

        }


        [HttpPost]
        public async Task<ActionResult> CrearCliente(Cliente reg) {

            string mensaje = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7015/api/Cliente/");

                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("", content);
                string apiResponse = await response.Content.ReadAsStringAsync();

                mensaje = apiResponse;
            }
            ViewBag.mensaje = mensaje; 
            

            return RedirectToAction("ListarClientes");


        }






        public async Task<IActionResult> EditarCliente(string id)
        {
            Cliente reg = new Cliente();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7015/api/Cliente/");
                HttpResponseMessage response = await client.GetAsync(id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                reg = JsonConvert.DeserializeObject<Cliente>(apiResponse);

            }

            
            return View(await Task.Run(() =>reg));

        }

        [HttpPost]
        public async Task<IActionResult> EditarCliente(Cliente reg)
        {

            string mensaje = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7015/api/Cliente/");

                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PutAsync("", content);
                string apiResponse = await response.Content.ReadAsStringAsync();


                mensaje = apiResponse;

            }

            ViewBag.mensaje = mensaje;

            return RedirectToAction("ListarClientes");

        }



















}
}
