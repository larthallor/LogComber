using Microsoft.Win32;
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
            await ViewModel.NewFileSelectedAsync(dialog.FileName);
        }
    }
}
