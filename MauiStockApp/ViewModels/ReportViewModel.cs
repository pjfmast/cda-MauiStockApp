using CommunityToolkit.Mvvm.Messaging;
using MauiStockApp.Services;
using MauiStockApp.Views;
using Shared.Dtos;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiStockApp.ViewModels;

public class ReportViewModel : BaseViewModel
{
    private readonly IInventoryService _inventoryService;

    public ObservableCollection<InventoryItemDto> Inventory { get; set; } = new();

    public ICommand ShowAboutPageCommand { get; set; }

    public ICommand RefreshCommand => new Command(async () => await Refresh());

    public ReportViewModel(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        IsLoading = true;
        ShowAboutPageCommand = new Command(ShowAboutPage);
        // public static void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = null) where TSender : class;
        MessagingCenter.Subscribe<AppShell>(this, "ThemeChanged", async (obj) => await Refresh());
        // todo:replace with
        // public static void Register<TMessage>(this IMessenger messenger, object recipient, MessageHandler<object, TMessage> handler)
        //   WeakReferenceMessenger.Default.Register<>(this, "ThemeChanged", async (obj) => await Refresh());
    }

    public async Task Init()
    {
        if (initialised)
            return;

        initialised = true;

        await Refresh();
    }
    // No AddRange for ObservableCollection!
    private async Task Refresh()
    {
        IsLoading = true;
        Inventory.Clear();

        var inventory = await _inventoryService.GetInventory();

        foreach (var item in inventory)
        {
            Inventory.Add(item);
        }

        IsLoading = false;
    }

    public void ShowAboutPage()
    {
        var newWindow = new Window(new AboutPage())
        {
            Title = "About",
            Width = 300,
            Height = 300
        };
        Application.Current.OpenWindow(newWindow);
    }
}