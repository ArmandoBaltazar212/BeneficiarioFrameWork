using BeneficiarioWeb.Models;
using Newtonsoft.Json;

namespace BeneficiarioWeb.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string? _usuario;
        private static string? _clave;
        private static string? _baseUrlEmpleado;
        private static string? _baseUrlBeneficiario;
        HttpClient _empleado;
        HttpClient _beneficiario;
        // private static string _token;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseUrlEmpleado = builder.GetSection("ApiSettings:baseUrlEmpleado").Value;
            _baseUrlBeneficiario = builder.GetSection("ApiSettings:baseUrlBeneficiario").Value;
            HttpClientHandler handler = new HttpClientHandler();
            _empleado = new HttpClient();
            _beneficiario = new HttpClient();
            _empleado.BaseAddress = new Uri(_baseUrlEmpleado );
            _beneficiario.BaseAddress = new Uri(_baseUrlBeneficiario);
        }

        public Task Autenticar()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrlEmpleado);
            return Task.CompletedTask;
        }


        public async Task<List<Empleado>> ListaEmpleados()
        {
            List<Empleado> Objeto = new List<Empleado>();

            using (_empleado)
            {
                HttpResponseMessage response = await _empleado.GetAsync("ListaEmpleado");
                if (response.IsSuccessStatusCode)
                {
                    string respuesta = await response.Content.ReadAsStringAsync();
                    Objeto = JsonConvert.DeserializeObject<List<Empleado>>(respuesta);
                }
            }
            return Objeto;
        }

        public async Task<List<Beneficiario>> listaBeneficiario(int idEmpleado)
        {
            List<Beneficiario> Objeto = new List<Beneficiario>();

            HttpResponseMessage response = await _beneficiario.GetAsync($"ListaBeneficiarios{idEmpleado}");
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                Objeto = JsonConvert.DeserializeObject<List<Beneficiario>>(respuesta);
            }
            return Objeto;
        }

        public async Task<Empleado> DetalleEmpleado(int idEmpleado)
        {
            Empleado objeto = new Empleado();

            // _cliente.BaseAddress = new Uri(_baseUrlEmpleado);
            HttpResponseMessage mensaje = await _empleado.GetAsync($"GetEmpleado{idEmpleado}");
            if (mensaje.IsSuccessStatusCode)
            {
                string respuesta = await mensaje.Content.ReadAsStringAsync();
                objeto = JsonConvert.DeserializeObject<Empleado>(respuesta);
            }

            return objeto;
        }

        public async Task<Empleado> ObtenerEmpleado(int idEmpleado)
        {
            Empleado objeto = new Empleado();
            using (_empleado)
            {
                // _cliente.BaseAddress = new Uri(_baseUrlEmpleado + $"GetEmpleado{idEmpleado}");
                HttpResponseMessage mensaje = await _empleado.GetAsync($"GetEmpleado{idEmpleado}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    objeto = JsonConvert.DeserializeObject<Empleado>(respuesta);
                }
            }
            return objeto;
        }

        public async Task<Beneficiario> ObtenerBeneficiario(int idBeneficiario)
        {
            Beneficiario objeto = new Beneficiario();
            using (_beneficiario)
            {
                // _cliente.BaseAddress = new Uri(_baseUrlBeneficiario + $"SelecionaBeneficiario{idBeneficiario}");
                HttpResponseMessage mensaje = await _beneficiario.GetAsync($"SelecionaBeneficiario{idBeneficiario}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    objeto = JsonConvert.DeserializeObject<Beneficiario>(respuesta);
                }
            }
            return objeto;
        }

        public async Task<bool> GuardarEmpleado(Empleado empleado)
        {
            //var cliente = new HttpClient();
            //  _cliente.BaseAddress = new Uri(_baseUrlEmpleado + $"ActualizaEmpleado");
            StringContent contenido = new StringContent(
      JsonConvert.SerializeObject(empleado), System.Text.Encoding.UTF8, "application/json");
            var response = await _empleado.PostAsync($"ActualizaEmpleado", contenido);

            return true;
        }

        public async Task<bool> GuardarBeneficiario(Beneficiario beneficiario)
        {
            //var cliente = new HttpClient();
            //_Beneficiario.BaseAddress = new Uri(_baseUrlBeneficiario + $"ActualizaBeneficiario");
            StringContent contenido = new StringContent(
      JsonConvert.SerializeObject(beneficiario), System.Text.Encoding.UTF8, "application/json");
            var response = await _beneficiario.PostAsync($"ActualizaBeneficiario", contenido);
            return true;
        }


        public async Task<bool> BorraEmpleado(int idEmpleado)
        {
            // var cliente = new HttpClient();
            // _cliente.BaseAddress = new Uri(_baseUrlEmpleado + $"BorrarEmpleado{idEmpleado}");
            var response = await _empleado.DeleteAsync($"BorrarEmpleado{idEmpleado}");
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        public async Task<bool> BorraBeneficiario(int idBeneficiario)
        {
            //var cliente = new HttpClient();
            //  _cliente.BaseAddress = new Uri(_baseUrlBeneficiario + $"BorraBeneficiario{idBeneficiario}");
            var response = await _beneficiario.DeleteAsync($"BorraBeneficiario{idBeneficiario}");
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> CreaEmpleado(Empleado empleado)
        {
            // var cliente = new HttpClient();
            // _cliente.BaseAddress = new Uri(_baseUrlEmpleado + $"AgregaEmpleado");
            StringContent contenido = new StringContent(
      JsonConvert.SerializeObject(empleado), System.Text.Encoding.UTF8, "application/json");

            var response = await _empleado.PostAsync($"AgregaEmpleado", contenido);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> CreaBeneficiario(Beneficiario beneficiario)
        {
            //var cliente = new HttpClient();
            // _cliente.BaseAddress = new Uri(_baseUrlBeneficiario + $"AgregaBeneficiario");
            StringContent contenido = new StringContent(
      JsonConvert.SerializeObject(beneficiario), System.Text.Encoding.UTF8, "application/json");
            var response = await _beneficiario.PostAsync($"AgregaBeneficiario", contenido);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        // GET:totalPorcentajeBeneficiarios
        public async Task<int> TotalPorcentaje(int idEmpleado, int Porcentaje)
        {

            int Res = 0;
            // using (var cliente = new HttpClient())
            using (_beneficiario)
            {
                HttpResponseMessage mensaje = await _beneficiario.GetAsync($"TotalPorcentajes{idEmpleado}");
                if (mensaje.IsSuccessStatusCode)
                {
                    var respuesta = await mensaje.Content.ReadAsStringAsync();
                    int x = Int32.Parse(respuesta) + Porcentaje;
                    if (x > 100)
                    {
                        Res = -1; // -1 Significa que con el porcentaje requerido sobre pasa el 100% total del empleado
                    }
                    else
                        //Res = Int32.Parse(respuesta);
                        Res = JsonConvert.DeserializeObject<int>(respuesta);
                }
            }
            return Res;
        }

        // GET:totalPorcentajeBeneficiarios
        public async Task<int> TotalPorcentajeMenosBeneficiario(int idEmpleado, int idBeneficiario, int Porcentaje)
        {

            int Res = 0;
            // using (var cliente = new HttpClient())
            using (_beneficiario)
            {
                HttpResponseMessage mensaje = await _beneficiario.GetAsync($"TotalPorcentajesMenosBeneficiario{idEmpleado}/{idBeneficiario}");
                if (mensaje.IsSuccessStatusCode)
                {
                    var respuesta = await mensaje.Content.ReadAsStringAsync();
                    int x = Int32.Parse(respuesta) + Porcentaje;
                    if (x > 100)
                    {
                        Res = -1; // -1 Significa que con el porcentaje requerido sobre pasa el 100% total del empleado
                    }
                    else
                        //Res = Int32.Parse(respuesta);
                        Res = JsonConvert.DeserializeObject<int>(respuesta);
                }
            }
            return Res;
        }






    }
}
