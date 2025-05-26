using System.Net.Http.Json;

namespace Bike155ProyectMAUI;

public partial class CrearRutaPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public CrearRutaPage()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7170/") // Cambia al puerto de tu API
        };
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            var nuevaRuta = new RutaCreateDto
            {
                Ubicacion = UbicacionEntry.Text,
                Fecha = FechaPicker.Date,
                UserId = int.Parse(UserIdEntry.Text),
                BikeId = int.Parse(BikeIdEntry.Text)
            };

            var response = await _httpClient.PostAsJsonAsync("api/Rutas", nuevaRuta);

            var contenido = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Ruta creada correctamente", "OK");
                await Navigation.PopAsync(); // Regresar a la página principal
            }
            else
            {
                await DisplayAlert("Error", $"Código: {response.StatusCode}\n{contenido}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

}


// Modelo DTO
public class RutaCreateDto
{
    public string Ubicacion { get; set; }
    public DateTime Fecha { get; set; }
    public int UserId { get; set; }
    public int BikeId { get; set; }
}
