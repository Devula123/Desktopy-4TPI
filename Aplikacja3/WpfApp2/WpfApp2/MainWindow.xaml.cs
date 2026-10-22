
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Dictionary<string, SolidColorBrush> kolory_ = new Dictionary<string, SolidColorBrush>
        {
            ["Czerwony"] = Brushes.Red,
            ["Niebieski"] = Brushes.Blue,
            ["Żółty"] = Brushes.Yellow,
        };
        public ContextMenu stworz_menu(Shape ksztalt)
        {
            ContextMenu cm = new ContextMenu();
            MenuItem im = new MenuItem();
            im.Header = "Usun ksztalt";
            im.Click += (sender, args) =>
            {
                canva.Children.Remove(ksztalt);
            };
            cm.Items.Add(im);
            return cm;
        }
        private void stworzKsztalt(
            double szerokosc,
            double wysokosc,
            Shape ksztalt,
            string kolor)
        {
            ksztalt.Width = szerokosc;
            ksztalt.Height = wysokosc;
            ksztalt.Fill = kolory_[kolor];
            ksztalt.ContextMenu = stworz_menu(ksztalt);
        }
        private void stworzKsztaltZContexMenu( { 
        double szerokosc,
        double wysokosc,
        Shape ksztalt,
        string kolor)
        {
            ksztalt.Width = szerokosc;
            ksztalt.Height = wysokosc;
            ksztalt.Fill = kolory_[kolor];
            ksztalt.ContextMenu = stworz_menu(ksztalt);
        }
        public Random rnd = new Random();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RadioButton ksztalt = Panel.Children
                .OfType<RadioButton>()
                .FirstOrDefault(r =>
                    r.GroupName == "ksztalt" && r.IsChecked == true);

            RadioButton kolor = Panel.Children
                .OfType<RadioButton>()
                .FirstOrDefault(r =>
                    r.GroupName == "kolor" && r.IsChecked == true);

            double slider_value = slider_.Value;

            if (ksztalt == null || kolor == null)
            {
                return;
            }

            Shape figura;

            if (ksztalt.Content?.ToString() == "Kolo")
            {
                figura = new Ellipse();

                stworzKsztalt(
                    slider_value,
                    slider_value,
                    figura,
                    kolor.Content.ToString());
            }
            else if (ksztalt.Content?.ToString() == "Kwadrat")
            {
                figura = new Rectangle();

                stworzKsztalt(
                    slider_value,
                    slider_value,
                    figura,
                    kolor.Content.ToString());
            }
            else
            {
                figura = new Rectangle();

                stworzKsztalt(
                    slider_value,
                    slider_value * 1.25,
                    figura,
                    kolor.Content.ToString());
            }

            canva.Children.Add(figura);

            Canvas.SetLeft(
                figura,
                rnd.NextDouble() *
                Math.Max(0, canva.ActualWidth - figura.Width));

            Canvas.SetTop(
                figura,
                rnd.NextDouble() *
                Math.Max(0, canva.ActualHeight - figura.Height));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canva.Children.Clear();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
