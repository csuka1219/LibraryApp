using EventAggregator.Blazor;
using ReaderClient.Data;
using ReaderClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ReaderClient.Pages.LoanPanel
{
    public partial class BookBorrowIndex
    {
        private List<Book> allBooks = new List<Book>();
        private List<Book> availableBooks = new List<Book>();
        private List<Book> selectedMemberBooks = new List<Book>();
        private List<Loan> loans = new List<Loan>();
        private List<LibraryMember> members = new List<LibraryMember>();
        private LibraryMember? selectedMember = null;
        private object selectedBookIndex = -1;
        private object selectedMemberBookIndex = -1;

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
            loans = await LoanService!.GetAllLoanAsync() ?? new List<Loan>();
            allBooks = await BookService!.GetAllBookAsync() ?? new List<Book>();
            availableBooks = new List<Book>(allBooks
                .Where(b => !loans.Select(l => l.InvNumber).Contains(b.InvNumber))
                .OrderBy(ab => ab.Title)
                .ToList());


            members = await LibraryMemberService!.GetAllLibraryMemberAsync() ?? new List<LibraryMember>();
            selectedMemberBooks = new List<Book>();
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

        private void MemberSelectionValueChange(LibraryMember selectedMember)
        {
            this.selectedMember = selectedMember;
            GetSelectedMemberBooks(selectedMember.ReaderNumber);
            selectedMemberBookIndex = -1;
        }

        private void GetSelectedMemberBooks(int selectedMemberId)
        {
            IEnumerable<int> selectedMemberLoans = loans
                .Where(l => l.ReaderNumber == selectedMemberId)
                .Select(l => l.InvNumber);
            selectedMemberBooks = allBooks
                .Where(b => selectedMemberLoans.Contains(b.InvNumber))
                .ToList();
        }

    }
}
