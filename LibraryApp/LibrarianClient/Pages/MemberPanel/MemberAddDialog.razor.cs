using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Pages.MemberPanel
{
    public partial class MemberAddDialog
    {
        private DateTime? tmpBirthDay = DateTime.Now.AddYears(-18);
        private bool success = false;
        private MudForm? form;

        [CascadingParameter]

        public MudDialogInstance? MudDialog { get; set; }

        [Parameter]

        public LibraryMember? Member { get; set; }

        protected override void OnInitialized()
        {
            if (Member is null)
            {
                Cancel();
            }

            Member!.BirthDate = tmpBirthDay!.Value;
        }

        private void Cancel()
        {
            MudDialog!.Cancel();
        }

        private async void Save()
        {
            Member!.BirthDate = tmpBirthDay!.Value;
            await form!.Validate();
            if (!success)
            {
                return;
            }

            MudDialog!.Close();
        }
    }
}
