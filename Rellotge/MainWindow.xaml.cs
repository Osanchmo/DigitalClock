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
using System.Windows.Threading;
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rellotge
{
    public partial class MainWindow : Window
    {
        Time time;
        public MainWindow()
        {   
            InitializeComponent();
            alarmText.MaxLength = 8;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            if (File.Exists("time"))
            {
                Stream TestFileStream = File.OpenRead("time");
                BinaryFormatter deserializer = new BinaryFormatter();
                 time = (Time)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            AlarmaHora.Content = time.time;

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string hora = (string)timing.Content;
            hora = DateTime.Now.ToLongTimeString();
            timing.Content = hora;
            
            if (hora == alarmText.Text && AlarmaActiva.IsChecked == true)
            {
                MessageBox.Show("Avís d'alarma", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                SystemSounds.Beep.Play();
            }
        }

        private void Sortir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("App amb un rellotge i altres opcions", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void setAlarm_Click(object sender, RoutedEventArgs e)
        {
            AlarmaHora.Content = alarmText.Text;
            time = new Time(alarmText.Text);
            Stream TestFileStream = File.Create("time");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, time);
            TestFileStream.Close();
        }
    }
    [Serializable()]
    public class Time
    {
        public String time { get; set; }

        public Time(String time)
        {
            this.time = time;
        }
    }
 }
