using EventAggregator.Blazor;
using ReaderClient.Data;
using ReaderClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ReaderClient.Pages.BookPanel
{
    public partial class BookIndex
    {
        private List<Book> books = new List<Book>();
        private List<Book> booksBackUp = new List<Book>();
        private List<Loan> loans = new List<Loan>();
        private List<LibraryMember> members = new List<LibraryMember>();
        private BookDetails bookDetails = new BookDetails("Raktáron");

        [Inject]
        private IBookService? BookService { get; set; }

        [Inject]
        private ILoanService? LoanService { get; set; }

        [Inject]
        private ILibraryMemberService? LibraryMemberService { get; set; }

        [Inject]
        private IDialogService? DialogService { get; set; }

        [Inject]
        private IEventAggregator? EventHandler { get; set; }

        [Inject]
        private ISnackbar? Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FrontendHelper.StartLoading(EventHandler!);
            books = await BookService!.GetAllBookAsync() ?? new List<Book>();
            loans = await LoanService!.GetAllLoanAsync() ?? new List<Loan>();
            members = await LibraryMemberService!.GetActiveLibraryMembersAsync() ?? new List<LibraryMember>();
            booksBackUp = new List<Book>(books);
            await FrontendHelper.StopLoading(EventHandler!);
        }

        private string GetBookStatus(int invNumber)
        {
            bool status = loans.Any(l => l.InvNumber == invNumber);
            return status ? "Kiadva" : "Raktáron";
        }

        private string GetBookRenter(int invNumber)
        {
            var loan = loans.FirstOrDefault(l => l.InvNumber == invNumber);
            if (loan != null)
            {
                var member = members.FirstOrDefault(m => m.ReaderNumber == loan.ReaderNumber);
                if (member != null)
                {
                    return member.Name;
                }
            }
            return "-";
        }

     private DateTime GetBookDeadline(int invNumber)
     {
         var loan = loans.FirstOrDefault(l => l.InvNumber == invNumber);
         if (loan != null)
         {
             return loan.returnDeadline;
         }
         return DateTime.MinValue;
     }


    }
}
