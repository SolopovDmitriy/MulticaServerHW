using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListeneMessage();
        }

        private async void ListeneMessage()
        {
           await Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 45001);
                        socket.Bind(iPEndPoint);
                        MessageBox.Show(iPEndPoint.Address.ToString());

                        socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, 
                            new MulticastOption(IPAddress.Parse("224.5.5.5"), IPAddress.Any));//224.5.5.5 // The multicast group address is the same as the address used by the server.
                                                                                              //-групповой адресс с которого происходит рассылка

                        /*получение данных-----------------------------------start*/
                        byte[] buffer = new byte[1024];
                        socket.Receive(buffer);

                        Dispatcher.Invoke(delegate ()
                        {
                            ListBoxMessages.Items.Add(Encoding.UTF8.GetString(buffer));
                        });
                        
                        /*получение данных-----------------------------------end*/
                        socket.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
