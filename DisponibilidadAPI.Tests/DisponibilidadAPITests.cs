using DisponibilidadAPI.Enums;
using DisponibilidadAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
namespace DisponibilidadAPI.Tests;

public class DisponibilidadAPITests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;


    public DisponibilidadAPITests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SeSolicitaLaDisponibilidadDeUnEmpleadoALaAPIYSeVerificaQueSeaLaEsperadaTest()
    {
        int legajo = 10;
        // Preparación
        var client = _factory.CreateClient();
        var filePath = Path.Combine(Environment.CurrentDirectory, "Data", "data.json");
        var json = File.ReadAllText(filePath);

        //Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/disponibilidadEmpleado/{legajo}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        //Deserializacion 
        CombinacionDataJson data = JsonConvert.DeserializeObject<CombinacionDataJson>(json);

        //Constatación
        Empleado empleadoEsperado = data.empleados.FirstOrDefault(e => e.Legajo == legajo);
        Empleado empleadoObtenido = JsonConvert.DeserializeObject<Empleado>(responseString);


        Assert.Equal(empleadoEsperado, empleadoObtenido);

       
    }
    
    [Fact]
    public async Task VerificaQueLaDisponibilidadDelEmpleadoSeaIgualALaDevolucionDeLaAPITest()
    {
        int legajo = 10;
        // Preparación
        var client = _factory.CreateClient();
        var filePath = Path.Combine(Environment.CurrentDirectory, "Data", "data.json");
        var json = File.ReadAllText(filePath);

        //Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/disponibilidadEmpleado/{legajo}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        //Deserializacion 
        CombinacionDataJson data = JsonConvert.DeserializeObject<CombinacionDataJson>(json);
        Empleado empleadoEncontrado = data.empleados.FirstOrDefault(e => e.Legajo == legajo);

        Empleado actualResponse = JsonConvert.DeserializeObject<Empleado>(responseString);


        //Constatación

        Assert.Equal(empleadoEncontrado.Disponibilidades, actualResponse.Disponibilidades);
    }

    [Fact]
    public async Task SeSolicitaLaDisponibilidadDeUnEmpleadoQueNoExisteTest()
    {
        int legajo = 11;
        // Preparación
        var client = _factory.CreateClient();

        //Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/disponibilidadEmpleado/{legajo}");

        // Constatación
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SeSolicitaLosEmpleadosALaAPIYSeVerificaQueNoEsteVacioTest()
    {
        // Preparación
        var client = _factory.CreateClient();

        //Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/traerempleados");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        //Deserializacion 
        List<Empleado> actualResponse = JsonConvert.DeserializeObject<List<Empleado>>(responseString);


        //Constatación
        Assert.NotEmpty(actualResponse);
    }

    [Fact]
    public async Task SeSolicitalosLosEmpleadosALaAPIYSeVerificaQueNoEsteVacioTest()
    {
        // Preparación
        var client = _factory.CreateClient();

        //Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/traerequipos");
        response.EnsureSuccessStatusCode(); 
        var responseString = await response.Content.ReadAsStringAsync();

        //Deserializacion 
        List<Equipo> actualResponse = JsonConvert.DeserializeObject<List<Equipo>>(responseString);

        //Constatación
        Assert.NotEmpty(actualResponse);

    }
    [Fact]
    public async Task SeSolicitaLosEquiposAunaURLIncorrectaYDevuelvePaginaNoEncontradaTest()
    {
        // Preparación
        var client = _factory.CreateClient();

        // Ejecución
        var response = await client.GetAsync("/api/disponibilidad/traerequiposError");

        // Constatación
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SeChequeaLaDiponibilidadDeLosEmpleadosQuePuedenTrabajarUnMiercolesTest()
    {
        string fecha = "19062024";

        // Preparación
        var client = _factory.CreateClient();
        var filePath = Path.Combine(Environment.CurrentDirectory, "Data", "data.json");
        var json = File.ReadAllText(filePath);

        // Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/disponibles/{fecha}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Deserializar 
        List<Empleado> empleadosRequest = JsonConvert.DeserializeObject<List<Empleado>>(responseString);
        CombinacionDataJson data = JsonConvert.DeserializeObject<CombinacionDataJson>(json);

        Disponibilidad miercoles = new Disponibilidad(TipoDisponibilidad.Miercoles, []);
        List<Empleado> empleadosResultado= [];
        foreach (Equipo equipo in data.equipos)
        {
            if(equipo.ElEquipoEstaDisponible(DateTime.ParseExact(fecha, "ddMMyyyy", null))){ 
                foreach (Empleado empleado in equipo.Empleados)
                {
                    empleadosResultado.Add(empleado);
                }
            }
        }

        foreach (Empleado empleado in data.empleados)
        {
            if (empleado.EstaDisponible(DateTime.ParseExact(fecha, "ddMMyyyy", null)) && empleado.PerteneceAUnEquipo == false)
            {
                empleadosResultado.Add(empleado);
            }
        }
        //Constatación
        Assert.Equal(empleadosResultado, empleadosRequest);

    }

    
    [Fact]
    public async Task SeChequeaQueElfomatoDeFechaEsInvalidoYDaErrorTest()
    {
        // Preparación
        string fecha = "19-06-2024"; // Fecha con formato inválido
        var client = _factory.CreateClient();

        // Ejecución
        var response = await client.GetAsync($"/api/disponibilidad/disponibles/{fecha}");

        // Constatación
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Formato de fecha inválido", content);
    }

}
