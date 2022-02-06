using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;

namespace LogComberWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown(0);
    }

    private async void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false,
            ReadOnlyChecked = true,
            ValidateNames = true,
            Title = "Choose Log File"
        };

        if (dialog.ShowDialog(this).Value)
        {
            DummyLogEntriesDataGrid.IsEnabled = false;
            await Task.Yield();
            try
            {
                await ViewModel.LoadFileAsync(dialog.FileName);

            }
            finally
            {
                DummyLogEntriesDataGrid.IsEnabled = true;
            }
        }
    }

    private async void ReloadMenuItem_Click(object sender, RoutedEventArgs args)
    {
        if(ViewModel.Filename != default)
        {
            DummyLogEntriesDataGrid.IsEnabled = false;
            await Task.Yield();

            try
            {
                await ViewModel.LoadFileAsync(ViewModel.Filename);
            }
            finally
            {
                DummyLogEntriesDataGrid.IsEnabled = true;
            }
        }
    }
}
