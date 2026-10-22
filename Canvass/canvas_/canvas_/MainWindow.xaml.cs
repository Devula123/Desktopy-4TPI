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

namespace canvas_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Point punkt;
        bool czyAktywne = false;
        List<Shape> lista_ksztaltow = new List<Shape>();
        Dictionary<string, SolidColorBrush> lista_kolorow = new Dictionary<string, SolidColorBrush>()
        {
            ["Czerwony"] = Brushes.Red,
            ["Niebieski"] = Brushes.Blue,
            ["Żółty"] = Brushes.Yellow
        };
        public MainWindow()
        {
            InitializeComponent();

        }
        ContextMenu stworzMenu(Shape shape)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem() { Header = "Obróć mebel" };
            MenuItem item2 = new MenuItem() { Header = "Zmień kolor" };
            MenuItem item3 = new MenuItem() { Header = "Usuń ksztalt" };

            foreach (string kolor in lista_kolorow.Keys)
            {
                MenuItem cl = new MenuItem();
                cl.Header = kolor;
                cl.Click += (s, e) =>
                {
                    shape.Fill = lista_kolorow[kolor];
                };
                item2.Items.Add(cl);
            }
            item.Click += (s, e) =>
            {
                double temp = shape.Width;
                shape.Width = shape.Height;
                shape.Height = temp;
            };
            item3.Click += (s, e) =>
            {
                canv.Children.Remove(shape);
            };

            menu.Items.Add(item);
            menu.Items.Add(item2);
            menu.Items.Add(item3);
            return menu;
        }
        // Za braklo czasu na dokonczenie window
        //ContextMenu stworzMenu(Shape shape)
        //{
        //    ContextMenu menu = new ContextMenu();
        //    MenuItem item = new MenuItem() { Header = "Obróć mebel" };
        //    MenuItem item2 = new MenuItem() { Header = "Zmień kolor" };
        //    MenuItem item3 = new MenuItem() { Header = "Usuń ksztalt" };
        //    MenuItem item4 = new MenuItem() { Header = "Ustaw rozmiar" };

        //    foreach (string kolor in lista_kolorow.Keys)
        //    {
        //        MenuItem cl = new MenuItem();
        //        cl.Header = kolor;
        //        cl.Click += (s, e) =>
        //        {
        //            shape.Fill = lista_kolorow[kolor];
        //        };
        //        item2.Items.Add(cl);
        //    }
        //    item.Click += (s, e) =>
        //    {
        //        double temp = shape.Width;
        //        shape.Width = shape.Height;
        //        shape.Height = temp;
        //    };
        //    item3.Click += (s, e) => {
        //        canv.Children.Remove(shape);
        //    };
        //    item4.Click += (s, e) => { 
        //        Window okno = new Window();
        //        okno.Width = 300;
        //        okno.Height = 200;
        //        okno.Title = "Nowy rozmiar";
        //        okno.Owner = this;
        //        StackPanel sp = new StackPanel();
        //        sp.Children.Add(new TextBlock()
        //        {
        //            Text = "Podaj wartosc na suwaku od 10 do 200"
        //        });
        //        Slider slider = new Slider()
        //        {
        //            Value = 50,
        //            Minimum = 10,
        //            Maximum = 200,
        //        };
        //        sp.Children.Add(slider);
        //        okno.Content = sp;
        //    };
        //    menu.Items.Add(item);
        //    menu.Items.Add(item2);
        //    menu.Items.Add(item3);
        //    return menu;
        //}

        void ustawPozycje(Shape shape, Point punkt)
        {
            double x = punkt.X - shape.Width/2;
            double y = punkt.Y - shape.Height / 2;

            x = Math.Max(0, Math.Min(x, canv.ActualWidth - shape.Width));
            y = Math.Max(0, Math.Min(y, canv.ActualHeight - shape.Height));

            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }

        void stworzKsztalt(Shape shape, double szerokosc, double wysokosc, Brush kolor)
        {
            shape.Width = szerokosc;
            shape.Height = wysokosc;
            shape.Fill = kolor;
            shape.StrokeThickness = 2;
            shape.Stroke = Brushes.Black;
            shape.MouseLeftButtonDown += (s, e) =>
            {
                czyAktywne= true;
                e.Handled = true;
                shape.CaptureMouse();
            };
            shape.MouseMove += (s, e) =>
            {
                if (czyAktywne)
                {
                    bool czyKolzija = false;
                    Point punkt = e.GetPosition(canv);
                    if (lista_ksztaltow.Count == 1)
                    {
                        ustawPozycje(shape, punkt);

                    }
                    else {
                        foreach (Shape shp in lista_ksztaltow)
                        {
                            if (shp == shape)
                            {
                                continue;
                            }
                            double x_figury = Canvas.GetLeft(shp) + (shp.Width / 2);
                            double y_figury = Canvas.GetTop(shp) + (shp.Height / 2);

                            double x_odleglosc = Math.Abs(x_figury - punkt.X);
                            double y_odleglosc = Math.Abs(y_figury - punkt.Y);

                            double x_granica = (shp.Width / 2) + (shape.Width /2);
                            double y_granica = (shp.Height / 2)+ (shape.Height /2);
                            if (x_granica < x_odleglosc || y_granica < y_odleglosc)
                            {

                            }else
                            {
                                czyKolzija = true;
                                break;
                            }
                        }
                        if (!czyKolzija)
                        {
                            ustawPozycje(shape, punkt);

                        }
                    }
                }
                e.Handled = true;
            };
            shape.MouseLeftButtonUp += (s, e) =>
            {
                czyAktywne = false;
                shape.ReleaseMouseCapture();
                e.Handled = true;
            };
            shape.ContextMenu = stworzMenu(shape);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ksztalt = (sender as Button).Content.ToString();
            Shape ksz = new Rectangle();
            double bok = 50;

            if (ksztalt == "Dodaj Stół")
            {
                stworzKsztalt(ksz, bok * 1.5, bok, Brushes.Brown);
            }else
            {
                stworzKsztalt(ksz, bok, bok * 1.5, Brushes.Blue);
            }
            Canvas.SetLeft(ksz, (canv.ActualWidth - ksz.Width) / 2);
            Canvas.SetTop(ksz, (canv.ActualHeight - ksz.Height) / 2);
            lista_ksztaltow.Add(ksz);
            canv.Children.Add(ksz);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canv.Children.Clear();
            lista_ksztaltow.Clear();
        }
    }
}