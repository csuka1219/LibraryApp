using EventAggregator.Blazor;
using LibrarianClient.Data;
using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Pages.LoanPanel
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

        [Inject]
        private NavigationManager? NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
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
            }
            catch (Exception)
            {
                NavManager!.NavigateTo("/Notfound");
            }
            finally
            {
                await FrontendHelper.StopLoading(EventHandler!);
            }
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

        private async void BorrowBook()
        {
            if (selectedMember is null)
            {
                Snackbar!.Add("Nincs kiválasztva ügyfél", Severity.Warning);
                return;
            }

            try
            {
                Book selectedBook = availableBooks[(int)selectedBookIndex];
                Loan newLoan = new Loan()
                {
                    InvNumber = selectedBook.InvNumber,
                    ReaderNumber = selectedMember.ReaderNumber,
                    LoanDate = DateTime.Now,
                    returnDeadline = DateTime.Now.AddDays(1),
                };

                if (await ShowBorrowDialog(newLoan))
                {
                    return;
                }

                await FrontendHelper.StartLoading(EventHandler!);
                newLoan.Id = await LoanService!.AddLoanAsync(newLoan);
                loans.Add(newLoan);
                availableBooks.Remove(selectedBook);
                selectedMemberBooks.Add(selectedBook);
                selectedBookIndex = -1;
                StateHasChanged();
                await FrontendHelper.StopLoading(EventHandler!);
                Snackbar!.Add("A könyv kiadása sikeres volt", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba történt a könyv kiadása közben", Severity.Error);
            }
        }

        private async Task<bool> ShowBorrowDialog(Loan newLoan)
        {
            DialogParameters parameters = new DialogParameters { ["Loan"] = newLoan };

            IDialogReference dialog = await DialogService!.ShowAsync<BorrowDialog>("Kiadás", parameters);

            DialogResult result = await dialog.Result;

            return result.Canceled;
        }

        private async void ReturnBook()
        {
            if (selectedMember is null)
            {
                Snackbar!.Add("Nincs kiválasztva ügyfél", Severity.Warning);
                return;
            }

            try
            {
                await FrontendHelper.StartLoading(EventHandler!);
                Book selectedBook = selectedMemberBooks[(int)selectedMemberBookIndex];
                Loan loan = loans.First(l => l.ReaderNumber == selectedMember.ReaderNumber && l.InvNumber == selectedBook.InvNumber);
                await LoanService!.DeleteLoanAsync(loan.Id);
                loans.Remove(loan);
                selectedMemberBooks.Remove(selectedBook);
                availableBooks.Add(selectedBook);
                selectedMemberBookIndex = -1;
                StateHasChanged();
                await FrontendHelper.StopLoading(EventHandler!);
                Snackbar!.Add("A könyv visszavétele sikeres volt", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba történt a könyv visszavétele közben", Severity.Error);

            }
        }
    }
}
