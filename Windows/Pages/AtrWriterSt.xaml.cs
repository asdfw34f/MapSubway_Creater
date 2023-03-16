// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

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
using System.Windows.Markup;
using DrawMapMetroLibrary.Saving;

namespace EditorSubwayMap.Pages
{
    /// <summary>
    /// Логика взаимодействия для AtrWriterSt.xaml
    /// </summary>
    public partial class AtrWriterSt : Page
    {
        Brush brush;
        Point Position;

        public AtrWriterSt(Brush brush, Point point)
        {
            InitializeComponent();
            
            Position= point;
            this.brush = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveStation ss = new SaveStation();

            ss.AddStation(NameBox.Text, Convert.ToInt32(NextWayBox.Text),
                Convert.ToInt32(BackWayBox.Text), NameBoxWay.Text, brush, Position);
        }

        private void BackWayBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BackWayBox.Text = null;

        }

        private void NextWayBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NextWayBox.Text = null;

        }

        private void NameBoxWay_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NameBoxWay.Text = null;

        }

        private void NameBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NameBox.Text = null;

        }
    }
}
