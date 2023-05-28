using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Pages.BookPanel
{
    public partial class BookAddDialog
    {
        private bool success = false;
        private MudForm? form;

        [Parameter]

        public Book? Book { get; set; }

        [CascadingParameter]

        private MudDialogInstance? MudDialog { get; set; }

        protected override void OnInitialized()
        {
            if (Book is null)
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            MudDialog!.Cancel();
        }

        private async void Save()
        {
            await form!.Validate();
            if (!success)
            {
                return;
            }

            MudDialog!.Close();
        }
    }
}
