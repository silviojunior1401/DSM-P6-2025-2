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
}