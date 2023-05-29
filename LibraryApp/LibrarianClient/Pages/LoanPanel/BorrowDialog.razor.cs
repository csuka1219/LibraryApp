using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Pages.LoanPanel
{
    public partial class BorrowDialog
    {
        private bool success = false;
        private MudForm? form;
        private DateTime? tmpReturnDeadLine = DateTime.Now.AddDays(1);

        [Parameter]

        public Loan? Loan { get; set; }

        [CascadingParameter]

        private MudDialogInstance? MudDialog { get; set; }

        protected override void OnInitialized()
        {
            if (Loan is null)
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
            Loan!.returnDeadline = tmpReturnDeadLine!.Value;
            if (!success)
            {
                return;
            }

            MudDialog!.Close();
        }
    }
}
