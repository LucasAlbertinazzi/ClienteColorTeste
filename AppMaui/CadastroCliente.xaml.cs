using System.Text.RegularExpressions;

namespace AppMaui;

public partial class CadastroCliente : ContentPage
{
    public List<InfosCLientes?> listacliente;
    public event Action<InfosCLientes>? ClienteAdicionado;

    public CadastroCliente(List<InfosCLientes?> listaclientesgerada)
	{
		InitializeComponent();
        listacliente = listaclientesgerada;
    }

    private async void addCliente_Clicked(object sender, EventArgs e)
    {
		if(!string.IsNullOrEmpty(txbNome.Text) &&
            !string.IsNullOrEmpty(txbCodigo.Text) &&
            !string.IsNullOrEmpty(txbFantasia.Text))
        {
            if (IsValidHexColor(txbCodigo.Text))
            {
                SalvaInfos();
            }
            else
            {
                await DisplayAlert("Erro", "Código de cor inválido. Insira um código de cor em formato HEX.", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Aviso", "Preencha todos os campos.", "Ok");
        }
    }

    private bool IsValidHexColor(string hexColor)
    {
        string pattern = "^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";
        return Regex.IsMatch(hexColor, pattern);
    }

    private async void SalvaInfos()
    {
        var novoCliente = new InfosCLientes
        {
            Cliente = txbNome.Text.ToUpper(),
            NomeFantasia = txbFantasia.Text.ToUpper(),
            ColorBorder = Color.FromArgb(txbCodigo.Text.ToUpper())
        };

        ClienteAdicionado?.Invoke(novoCliente);

        await DisplayAlert("Aviso", "Cliente cadastrado!", "Ok");
        await Navigation.PopAsync();
    }
}