// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EditorSubwayMap.Pages
{
    /// <summary>
    /// Логика взаимодействия для SavedFile.xaml
    /// </summary>
    public partial class SavedFile : Window
    {
        Canvas can;
        public SavedFile(Canvas canvas)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            can = canvas;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}