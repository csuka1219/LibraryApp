using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;

namespace LibrarianClient.Pages.MemberPanel
{
    public partial class MemberIndex
    {
        private List<LibraryMember>? libraryMembers = new List<LibraryMember>();
        private List<LoanData> loanDatas = new List<LoanData>();
        private Dictionary<int, bool> extraInfoHelper = new Dictionary<int, bool>();

        [Inject]
        private ILibraryMemberService? LibraryMemberService { get; set; }

        [Inject]
        private IBookService? BookService { get; set; }

        [Inject]
        private ILoanService? LoanService { get; set; }

        [Inject]
        private NavigationManager? NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            libraryMembers = await LibraryMemberService!.GetAllLibraryMemberAsync();
            if (libraryMembers is not null)
            {
                foreach (LibraryMember member in libraryMembers)
                {
                    extraInfoHelper.Add(member.ReaderNumber, false);
                }
            }

            await FillLoanData();
        }

        private async Task FillLoanData()
        {
            List<Loan>? loans = await LoanService!.GetAllLoanAsync();

            // TODO az összes könyv lekérése helyett egy plusz végpont, ami csak a kiadott könyveket adja vissza
            List<Book>? books = await BookService!.GetAllBookAsync();
            if (books is not null && loans is not null)
            {
                foreach (Loan loan in loans)
                {
                    loanDatas.Add(new LoanData
                    {
                        ReaderId = loan.ReaderNumber,
                        Title = books.First(b => b.InvNumber == loan.InvNumber).Title,
                        LoanDate = loan.LoanDate,
                        ReturnDeadline = loan.returnDeadline,
                    });
                }
            }
        }

        private string GetReturnDateWarning(DateTime returnDeadline)
        {
            return (returnDeadline < DateTime.Now) ? "Lejárt!" : string.Empty;
        }

        private async void DeleteMember(LibraryMember libraryMember)
        {
            await LibraryMemberService!.DeleteLibraryMemberAsync(libraryMember.ReaderNumber);
            libraryMembers!.Remove(libraryMember);
            extraInfoHelper.Remove(libraryMember.ReaderNumber);
            StateHasChanged();
        }
    }

    public class LoanData
    {
        public int ReaderId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }

        public DateTime ReturnDeadline { get; set; }
    }
}
