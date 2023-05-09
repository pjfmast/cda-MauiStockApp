using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiStockApp.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _title;
    public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

    private bool _isLoading;
    public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(); } }

    protected bool initialised = false;

    public INavigation Navigation { get; set; }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


        public void RaisePropertyChanged(params string[] properties)
        {
            foreach (var propertyName in properties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}

