using Microsoft.Maui.Controls;

namespace CardioCheck;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void OnEntrarClicked(object sender, EventArgs e)
    {
        string usuario = UsuarioEntry.Text?.Trim();
        string senha = SenhaEntry.Text?.Trim();

        if (usuario == "admin" && senha == "admin")
        {

            Microsoft.Maui.Controls.Application.Current.Dispatcher.Dispatch(() =>
            {
                Application.Current.MainPage = new MenuPrincipal();
            });
        }
        else
        {
            MensagemErro.Text = " Usu�rio ou senha incorretos! \nTente novamente.";
        }
    }

}