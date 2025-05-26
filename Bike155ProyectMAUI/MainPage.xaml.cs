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
            BaseAddress = new Uri("https://localhost:7170/") // Ajusta al puerto real de tu backend
        };

        _ = LoadRutas(); // Llamada inicial para cargar rutas
    }

    // Obtener lista de rutas (GET)
    private async Task LoadRutas()
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

    // Método público para recargar rutas (llamado desde otras páginas)
    public async Task RecargarRutasAsync()
    {
        await LoadRutas();
    }

    // Ir a la página para crear ruta
    private async void OnCrearRutaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearRutaPage());
    }

    // Registrar usuario (opcional)
    private async void OnRegistrarUsuarioClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Acción", "Aquí irá la lógica para registrar usuario.", "OK");
    }
}

// Modelo de Ruta (debe coincidir con la respuesta del backend)
public class Ruta
{
    public int Id { get; set; }
    public string Ubicacion { get; set; }
    public DateTime Fecha { get; set; }
    public int UserId { get; set; }
    public int BikeId { get; set; }
}
