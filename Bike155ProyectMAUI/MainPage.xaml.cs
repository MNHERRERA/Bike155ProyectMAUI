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
            BaseAddress = new Uri("https://localhost:7170/") // Ajusta si tu backend usa otro puerto
        };

        LoadRutas();
    }

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

    private async void OnRegistrarUsuarioClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Acción", "Aquí irá la lógica para registrar usuario.", "OK");
    }

    private async void OnCrearRutaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Acción", "Aquí irá la lógica para crear ruta.", "OK");
    }
}

public class Ruta
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
