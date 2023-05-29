using EventAggregator.Blazor;
using LibrarianClient.Data;
using LibrarianClient.Pages.BookPanel;
using LibrarianClient.Services;
using Library;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Reflection.Metadata.BlobBuilder;

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
        private IDialogService? DialogService { get; set; }

        [Inject]
        private IEventAggregator? EventHandler { get; set; }
        [Inject]
        private ISnackbar? Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FrontendHelper.StartLoading(EventHandler!);
            libraryMembers = await LibraryMemberService!.GetAllLibraryMemberAsync();
            if (libraryMembers is not null)
            {
                foreach (LibraryMember member in libraryMembers)
                {
                    extraInfoHelper.Add(member.ReaderNumber, false);
                }
            }

            await FillLoanData();
            await FrontendHelper.StopLoading(EventHandler!);
        }

        private async Task FillLoanData()
        {
            List<Loan>? loans = await LoanService!.GetAllLoanAsync();

            List<Book>? books = await BookService!.GetLoanedBooksAsync();
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

        private async void AddMember()
        {
            try
            {
                LibraryMember member = new LibraryMember();
                member.ReaderNumber = 0;
                DialogParameters parameters = new DialogParameters { ["Member"] = member };

                IDialogReference dialog = await DialogService!.ShowAsync<MemberAddDialog>("Hozzáadás", parameters);

                DialogResult result = await dialog.Result;

                if (!result.Canceled)
                {
                    await FrontendHelper.StartLoading(EventHandler!);
                    int newReaderNumber = await LibraryMemberService!.AddLibraryMemberAsync(member);
                    member.ReaderNumber = newReaderNumber;
                    libraryMembers!.Add(member);
                    extraInfoHelper.Add(member.ReaderNumber, false);
                    StateHasChanged();
                    await FrontendHelper.StopLoading(EventHandler!);
                    Snackbar!.Add("Az új tag hozzáadása sikeres volt", Severity.Success);
                }
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba történt az új tag felvitele közben", Severity.Error);
            }
        }

        private async void EditMember(LibraryMember member)
        {
            try
            {
                DialogParameters parameters = new DialogParameters { ["Member"] = member };

                IDialogReference dialog = await DialogService!.ShowAsync<MemberAddDialog>("Szerkesztés", parameters);

                DialogResult result = await dialog.Result;

                if (!result.Canceled)
                {
                    await FrontendHelper.StartLoading(EventHandler!);
                    await LibraryMemberService!.UpdateLibraryMemberAsync(member.ReaderNumber, member);
                    StateHasChanged();
                    await FrontendHelper.StopLoading(EventHandler!);
                    Snackbar!.Add("Az ügyfél módosítása sikeres volt", Severity.Success);
                }
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba történt az ügyfél szerkesztése közben", Severity.Error);
            }
        }

        private string GetReturnDateWarning(DateTime returnDeadline)
        {
            return (returnDeadline < DateTime.Now) ? "Lejárt!" : string.Empty;
        }

        private async void DeleteMember(LibraryMember libraryMember)
        {
            try
            {
                await FrontendHelper.StartLoading(EventHandler!);
                await LibraryMemberService!.DeleteLibraryMemberAsync(libraryMember.ReaderNumber);
                libraryMembers!.Remove(libraryMember);
                extraInfoHelper.Remove(libraryMember.ReaderNumber);
                StateHasChanged();
                await FrontendHelper.StopLoading(EventHandler!);
                Snackbar!.Add("Az ügyfél törlése sikeres volt", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar!.Add("Hiba az ügyfél törlése közben", Severity.Error);

            }
        }
    }
}
