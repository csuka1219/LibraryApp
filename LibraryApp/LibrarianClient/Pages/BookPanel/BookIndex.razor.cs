using EventAggregator.Blazor;
using LibrarianClient.Data;
using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibrarianClient.Pages.BookPanel
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

        [Inject]
        private NavigationManager? NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await FrontendHelper.StartLoading(EventHandler!);
                books = await BookService!.GetAllBookAsync() ?? new List<Book>();
                loans = await LoanService!.GetAllLoanAsync() ?? new List<Loan>();
                members = await LibraryMemberService!.GetActiveLibraryMembersAsync() ?? new List<LibraryMember>();
                booksBackUp = new List<Book>(books);
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

        private void OnBookSearch(string text)
        {
            books = booksBackUp.Where(b => b.Title.Contains(text)).ToList();
            StateHasChanged();
        }

        private void GetBookDetails(int invId)
        {
            bool status = loans.Any(l => l.InvNumber == invId);

            bookDetails = status ? new BookDetails(
                "Kiadva",
                members.First(m => m.ReaderNumber == loans.First(l => l.InvNumber == invId).ReaderNumber).Name,
                loans.First(l => l.InvNumber == invId).returnDeadline)
            :
            new BookDetails("Raktáron");
            ShowDetailsDialog();
        }

        private async void ShowDetailsDialog()
        {
            DialogParameters parameters = new DialogParameters { ["bookDetails"] = bookDetails };

            await DialogService!.ShowAsync<BookDetailsDialog>("Részletek", parameters);
        }

        private async void AddBook()
        {
            try
            {
                Book book = new Book();
                DialogParameters parameters = new DialogParameters { ["book"] = book };

                IDialogReference dialog = await DialogService!.ShowAsync<BookAddDialog>("Hozzáadás", parameters);

                DialogResult result = await dialog.Result;

                if (!result.Canceled)
                {
                    await FrontendHelper.StartLoading(EventHandler!);
                    int newInvNumber = await BookService!.AddBookAsync(book);
                    StateHasChanged();
                    book.InvNumber = newInvNumber;
                    books.Add(book);
                    booksBackUp.Add(book);
                    StateHasChanged();
                    await FrontendHelper.StopLoading(EventHandler!);
                    Snackbar!.Add("A könyv hozzáadása sikeres volt", Severity.Success);
                }
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba történt a könyv hozzáadása közben", Severity.Error);
            }
        }

        private async void DeleteBook(Book book)
        {
            try
            {
                await FrontendHelper.StartLoading(EventHandler!);
                await BookService!.DeleteBookAsync(book.InvNumber);
                books.Remove(book);
                booksBackUp.Remove(book);
                StateHasChanged();
                await FrontendHelper.StopLoading(EventHandler!);
                Snackbar!.Add("A könyv törlése sikeres volt", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar!.Add("hiba történt a könyv törlése közben", Severity.Error);
            }
        }
    }
}
