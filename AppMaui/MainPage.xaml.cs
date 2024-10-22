namespace AppMaui
{
    public partial class MainPage : ContentPage
    {
        private List<InfosCLientes?> listaclientesgerada = new List<InfosCLientes?>();

        public MainPage()
        {
            InitializeComponent();
            CarregaLista();
        }

        private void CarregaLista()
        {
            List<Color> cores = new List<Color>
            {
                Color.FromArgb("#14C672"),  // Verde
                Color.FromArgb("#6B208B"),  // Roxo
                Color.FromArgb("#FFC700")   // Amarelo
            };

            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                Color corAleatoria = cores[random.Next(cores.Count)];

                listaclientesgerada.Add(new InfosCLientes
                {
                    Cliente = "CLIENTE " + i.ToString(),
                    NomeFantasia = "NOME FANTASIA " + i.ToString(),
                    ColorBorder = corAleatoria
                });
            }

            listClientes.ItemsSource = listaclientesgerada;
        }


        private async void addCliente_Clicked(object sender, EventArgs e)
        {
            var cadastroClientePage = new CadastroCliente(listaclientesgerada);
            cadastroClientePage.ClienteAdicionado += AtualizaLista;
            await Navigation.PushAsync(cadastroClientePage);
        }

        private void AtualizaLista(InfosCLientes novoCliente)
        {
            listaclientesgerada.Add(novoCliente);
            listClientes.ItemsSource = null;
            listClientes.ItemsSource = listaclientesgerada;
        }

        private void txbPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txbPesquisa.Text.Length > 0)
            {
                listClientes.ItemsSource = listaclientesgerada.Where(x => x.Cliente.ToUpper().Contains(txbPesquisa.Text.ToUpper()) ||
                                                                                  x.NomeFantasia.ToUpper().Contains(txbPesquisa.Text.ToUpper()));
            }
            else
            {
                listClientes.ItemsSource = listaclientesgerada;
            }
            
        }
    }

}
