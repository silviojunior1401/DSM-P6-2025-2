using CardioCheck.Models;

namespace CardioCheck;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }


    //  função para o botão Sair
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool resposta = await DisplayAlert("Sair", "Você tem certeza que deseja fazer o logout?", "Sim", "Não");

        if (resposta)
        {
            // Apaga as informações de sessão salvas
            SessaoLogin.Token = null;

            // Volta para a página de Login, reiniciando o aplicativo
            Application.Current.MainPage = new LoginPage();
        }
    }
}
