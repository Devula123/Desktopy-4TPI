using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cz1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> imageDescriptions = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pliki\\images");
            string path = "../../../pliki/images";
            var imageFiles = Directory.GetFiles(path, "*.jpg");
            foreach (var imageFile in imageFiles)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imageFile, UriKind.RelativeOrAbsolute);
                listBox.Items.Add(new Image()
                {
                    Source = bitmap,
                    Width = 100,
                    Margin = new Thickness(25)
                });
                bitmap.EndInit();
            }
            string path_d = "../../../pliki/images/opisy.txt";
            imageDescriptions = File.ReadAllLines(path_d).ToList();
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Image image = (Image)listBox.SelectedItem;
            img.Source = image.Source;
            opis.Content = imageDescriptions[listBox.SelectedIndex];
        }
    }
}