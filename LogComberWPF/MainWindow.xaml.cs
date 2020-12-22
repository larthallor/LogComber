using LogComberWPF.ViewModels;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogComberWPF
{
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
            var dialog = new VistaOpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                ReadOnlyChecked = true,
                ValidateNames = true,
                Title = "Choose Log File"
            };
            
            if(dialog.ShowDialog(this).Value)
            {
                await ViewModel.NewFileSelectedAsync(dialog.FileName);
            }
        }
    }
}
