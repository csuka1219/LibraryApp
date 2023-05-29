using EventAggregator.Blazor;
using LibrarianClient.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Shared
{
    public partial class MainLayout : IHandle<LoadEvent>
    {
        private bool isDrawerOpen = true;
        private bool loading = false;

        private MudTheme myCustomTheme = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.Red.Default,
                Secondary = Colors.Green.Accent4,
                AppbarBackground = Colors.Red.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#00e676ff",
                Secondary = "#FFFFFF",
                Tertiary = "#00C853",
                Background = "#37474fff",
                Dark = "#263238ff",
                DrawerBackground = "#263238ff",
                AppbarBackground = "#263238ff",
                DrawerText = "#FFFFFF",
                DrawerIcon = "#FFFFFF",
                Surface = "#263238ff",
                TableStriped = "#37474fff",
                TableLines = "#424242ff",
            },
        };

        [Inject]
        public IEventAggregator? EventHandler { get; set; }

        public async Task HandleAsync(LoadEvent msg)
        {
            loading = msg.State;
            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            EventHandler!.Subscribe(this);
        }

        private void ToggleDrawer()
        {
            isDrawerOpen = !isDrawerOpen;
        }

        private string GetAvatarSize()
        {
            return isDrawerOpen ? "width:56px;height:56px;" : "width:40px;height:40px;";
        }
    }
}
