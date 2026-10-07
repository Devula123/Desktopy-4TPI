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

namespace Aplikacja2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point point;
        bool drag;
        public MainWindow()
        {
            InitializeComponent();
            drawCanva.MouseRightButtonDown += (e, args) =>
            {
                point = args.GetPosition(drawCanva);
            };
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            double szerokosc;
            double wysokosc;
            if (mi.Header.ToString() == "Koło")
            {
                Ellipse ksztalt = new Ellipse()
                {
                    Width = 60,
                    Height = 60,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = Brushes.Gray
                };
                setShape(ksztalt);
            }
            else if(mi.Header.ToString() == "Kwadrat")
            {
                Rectangle ksztalt = new Rectangle()
                {
                    Width = 60,
                    Height = 60,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = Brushes.Gray
                };
                setShape(ksztalt);
            }
            else
            {
                Rectangle ksztalt = new Rectangle()
                {
                    Width = 100,
                    Height = 60,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = Brushes.Gray
                };
                setShape(ksztalt);
            }
            stbLiczby.Content = $"Liczba elementow: {drawCanva.Children.Count}";
        }
        private void setShape(Shape shape)
        {
            Canvas.SetLeft(shape, point.X - (shape.Width / 2));
            Canvas.SetTop(shape, point.Y - (shape.Height / 2));
            drawCanva.Children.Add(shape);
            shape.ContextMenu = menuCreate(shape);
            shape.MouseLeftButtonDown += (e, args) =>
            {
                drag = true;
                shape.CaptureMouse();
                args.Handled = true;
            };
            shape.MouseMove += (e, args) =>
            {
                if (drag)
                {
                    Point curr = args.GetPosition(drawCanva);
                    Canvas.SetLeft(shape, curr.X - (shape.Width / 2));
                    Canvas.SetTop(shape, curr.Y - (shape.Height / 2));
                }
            };
            shape.MouseLeftButtonUp += (e, args) =>
            {
                if (drag)
                {
                    shape.ReleaseMouseCapture();
                    drag = false;
                    args.Handled = true;
                }
            };
        }
        private ContextMenu menuCreate(Shape shape)
        {
            ContextMenu cm = new ContextMenu();
            Dictionary<string,SolidColorBrush> kolory = new Dictionary<string, SolidColorBrush>() {
                {"Czerwony", Brushes.Red},
                {"Zielony", Brushes.Green},
                {"Niebieski", Brushes.Blue},
                {"Żółty", Brushes.Yellow},
            };
            MenuItem zm = new MenuItem()
            {
                Header = "Zmień kolor wypełnienia"
            };
            foreach (string key in kolory.Keys)
            {
                MenuItem Item = new MenuItem() { 
                Header = key,
                };
                Item.Click += (e, args) =>
                {
                    shape.Fill = kolory[key];
                };
                zm.Items.Add(Item);
            }
            MenuItem zm2 = new MenuItem()
            {
                Header = "Usuń obiekt",
            };
            zm2.Click += (e, args) =>
            {
                drawCanva.Children.Remove(shape);
                stbLiczby.Content = $"Liczba elementow: {drawCanva.Children.Count}";
            };
            cm.Items.Add(zm);
            cm.Items.Add(zm2);
            return cm;
        }
    }
}