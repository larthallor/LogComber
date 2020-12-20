using LogComberWPF.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        private void ChooseDirectoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Multiselect = true,
                InitialDirectory = ViewModel.FileDirectory
            };
            
            if(dialog.ShowDialog(this).Value)
            {
                ViewModel.Filenames.Clear();
                foreach (var filename in dialog.FileNames)
                {
                    ViewModel.FileDirectory = System.IO.Path.GetDirectoryName(filename);
                    ViewModel.Filenames.Add(System.IO.Path.GetFileName(filename));
                }
            }
        }
    }
}
