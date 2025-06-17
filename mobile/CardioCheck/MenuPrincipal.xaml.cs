using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;


namespace CardioCheck;

public partial class MenuPrincipal : Microsoft.Maui.Controls.TabbedPage
{
    public MenuPrincipal()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
    }


    private void CalcularRisco_Clicked(object sender, EventArgs e)
    {
        bool idadeValida = int.TryParse("0", out int idade);
        bool pressaoValida = int.TryParse(PressaoEntry.Text, out int pressao);
        bool colesterolValido = int.TryParse(ColesterolEntry.Text, out int colesterol);

        if (!idadeValida || !pressaoValida || !colesterolValido)
        {
            ResultadoLabel.Text = "Por favor, preencha todos os campos corretamente.";
            ResultadoLabel.TextColor = Colors.Red;
            return;
        }

        int score = 0;
        if (idade > 50) score += 2;
        if (pressao > 130) score += 2;
        if (colesterol > 200) score += 2;

        string risco = score switch
        {
            0 => "Baixo risco card aco",
            2 => "Risco moderado",
            >= 4 => "Alto risco card aco",
            _ => "Risco indefinido"
        };

        ResultadoLabel.Text = $"Resultado: {risco}";
        ResultadoLabel.TextColor = score >= 4 ? Colors.Red : Colors.Green;
    }
}
