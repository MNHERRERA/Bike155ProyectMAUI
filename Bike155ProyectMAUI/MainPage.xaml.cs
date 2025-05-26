using System.Net.Http.Json;
using Microsoft.Maui.Controls;

namespace Bike155ProyectMAUI;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7170/") // Cambia al puerto de tu backend
        };

        LoadRutas();
    }

    // Obtener lista de rutas (GET)
    private async void LoadRutas()
    {
        try
        {
            var rutas = await _httpClient.GetFromJsonAsync<List<Ruta>>("api/Rutas");
            RutasCollectionView.ItemsSource = rutas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Crear una ruta nueva (POST)
    private async void OnCrearRutaClicked(object sender, EventArgs e)
    {
        try
        {
            // Ejemplo de datos para la nueva ruta
            var nuevaRuta = new Ruta
            {
                Nombre = $"Ruta creada en {DateTime.Now}"
            };

            var response = await _httpClient.PostAsJsonAsync("api/Rutas", nuevaRuta);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Ruta creada correctamente", "OK");
                LoadRutas(); // Recargar la lista
            }
            else
            {
                await DisplayAlert("Error", $"Código: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnRegistrarUsuarioClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Acción", "Aquí irá la lógica para registrar usuario.", "OK");
    }
}

// Modelo de Ruta
public class Ruta
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
