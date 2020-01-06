using System;
using System.Windows;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TeleCtrl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Focus speed
        private Regex rxSpeed = new Regex(@"^speed:(\d)\r\n$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private int _focusSpeed;
        public string focusSpeed
        {
            get
            {
                return _focusSpeed.ToString();
            }
            set
            {
                _focusSpeed = Int32.Parse(value);
                OnPropertyChanged("focusSpeed");
            }
        }
        private void IncreaseFocusSpeed()
        {
            //int speed = _focusSpeed;
            //speed++;
            //focusSpeed = speed.ToString();
            Arduino.Send(Arduino.Command.INCREASE_FOCUS_SPEED);
        }
        private void DecreaseFocusSpeed()
        {
            //int speed = _focusSpeed;
            //if (speed > 1)
            //{
            //    speed--;
            //    focusSpeed = speed.ToString();
            //}
            Arduino.Send(Arduino.Command.DECREASE_FOCUS_SPEED);
        }

        // Constructor / Destructor
        public MainWindow()
        {
            InitializeComponent();

            // Binding of focusSpeed
            _focusSpeed = 5;
            DataContext = this;

            // Initialize Arduino
            Arduino.Serial.DataReceived += new SerialDataReceivedEventHandler(ArduinoDataReceived);
            try
            {
                Arduino.OpenConnection();

                if (Arduino.Serial.IsOpen)
                {
                    stsText.Text = "Port open";
                    stsText.Background = Brushes.Green;
                    Arduino.Send(Arduino.Command.GET_FOCUS_SPEED);
                }
            }
            catch (Exception)
            {
                stsText.Text = "Port not found or busy";
                stsText.Background = Brushes.Red;
            }
        }
        ~MainWindow()
        {
            Arduino.CloseConnection();
        }

        // Binding property changes
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }        

        // Arduino events
        private void ArduinoDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            Console.WriteLine(data);
            checkResponse(data);
            
        }

        private void checkResponse(string data)
        {
            // Focus speed
            MatchCollection matchesSpeed = rxSpeed.Matches(data);
            if (matchesSpeed.Count > 0)
            {
                foreach (Match match in matchesSpeed)
                {
                    GroupCollection groups = match.Groups;
                    focusSpeed = groups[1].Value;
                }
            }
            
        }

        // View events
        private void OnFocusOutButtonDown(object sender, RoutedEventArgs e)
        {
            //Arduino.Serial.Write("1");
            Console.WriteLine("FocusOutButtonDown");
        }
        private void OnFocusOutButtonUp(object sender, RoutedEventArgs e)
        {
            //Arduino.Serial.Write("1");
            Console.WriteLine("FocusOutButtonUp");
        }
        private void OnFocusInButtonDown(object sender, RoutedEventArgs e)
        {
            //Arduino.Serial.Write("1");
            Console.WriteLine("FocusInButtonDown");
        }
        private void OnFocusInButtonUp(object sender, RoutedEventArgs e)
        {
            //Arduino.Serial.Write("1");
            Console.WriteLine("FocusInButtonUp");
        }
        private void OnFocusSpeedIncreasePress(object sender, RoutedEventArgs e)
        {
            IncreaseFocusSpeed(); 
        }
        private void OnFocusSpeedDecreasePress(object sender, RoutedEventArgs e)
        {
            DecreaseFocusSpeed();
        }
        private void OnRelay1ButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
                button.Content = "ON";
                Arduino.Send(Arduino.Command.RELAY1_ON);
        }
        private void OnRelay1ButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
                button.Content = "OFF";
                button.Background = Brushes.Red;
                Arduino.Send(Arduino.Command.RELAY1_OFF);
        }
        private void OnRelay2ButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "ON";
            Arduino.Send(Arduino.Command.RELAY2_ON);
        }
        private void OnRelay2ButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "OFF";
            button.Background = Brushes.Red;
            Arduino.Send(Arduino.Command.RELAY2_OFF);
        }
        private void OnRelay3ButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "ON";
            Arduino.Send(Arduino.Command.RELAY3_ON);
        }
        private void OnRelay3ButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "OFF";
            button.Background = Brushes.Red;
            Arduino.Send(Arduino.Command.RELAY3_OFF);
        }
        private void OnRelay4ButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "ON";
            Arduino.Send(Arduino.Command.RELAY4_ON);
        }
        private void OnRelay4ButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            button.Content = "OFF";
            button.Background = Brushes.Red;
            Arduino.Send(Arduino.Command.RELAY4_OFF);
        }
    }
}
