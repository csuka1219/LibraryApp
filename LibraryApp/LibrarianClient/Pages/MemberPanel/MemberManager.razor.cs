using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;

namespace LibrarianClient.Pages.MemberPanel
{
    public partial class MemberManager
    {
        private LibraryMember member = new LibraryMember();

        private DateTime? tmpBirthDay = DateTime.Now.AddYears(-18);

        [Inject]
        private ILibraryMemberService? LibraryMemberService { get; set; }

        [Inject]
        private NavigationManager? NavManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            member.BirthDate = tmpBirthDay!.Value;
            return base.OnInitializedAsync();
        }

        private async void SaveMember()
        {
            member.BirthDate = tmpBirthDay!.Value;
            await LibraryMemberService!.AddLibraryMemberAsync(member);
            NavManager!.NavigateTo("Members");
        }
    }
}
