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

namespace MulticaServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = TextBoxMesage.Text;
            if (message.Length <= 5)
            {
                MessageBox.Show("");
            }
            try
            {
                /*Multicast - отправка пакетов  группе получателей за один сеанс отправки*/
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

                //MulticastTimeToLive - время жизни пакета
                //1 - не выходим за пределы локальной сети
                //2 - пакет данных будет роходить через несколько роутеров


                //1)  localhost 127.0.0.1
                //2)  192.168.0.133
                //3)  93.78.142.126
                //4)  224.5.5.5


                /*Multicast захватывает ip адресса в диапазоне: 224.0.0.0 и до 239.255.255.255*/
                IPAddress iPAddress = IPAddress.Parse("224.5.5.5");
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 45001);

                socket.Connect(iPEndPoint);
                socket.Send(Encoding.UTF8.GetBytes(message));
                socket.Close();
                TextBoxMesage.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

//public partial class MainWindow : Window
//{
//    public MainWindow()
//    {
//        InitializeComponent();
//    }

//    private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
//    {
//        string message = TextBoxMesage.Text;
//        if (message.Length <= 5)
//        {
//            MessageBox.Show("");
//        }
//        try
//        {
//            /*Multicast - отправка пакетов  группе получателей за один сеанс отправки*/
//            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

//            //MulticastTimeToLive - время жизни пакета
//            //1 - не выходим за пределы локальной сети
//            //2 - пакет данных будет роходить через несколько роутеров


//            //1)  localhost 127.0.0.1
//            //2)  192.168.0.133
//            //3)  93.78.142.126
//            //4)  224.5.5.5


//            /*Multicast захватывает ip адресса в диапазоне: 224.0.0.0 и до 239.255.255.255*/
//            IPAddress iPAddress = IPAddress.Parse("224.5.5.5");
//            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 45001);

//            socket.Connect(iPEndPoint);
//            socket.Send(Encoding.UTF8.GetBytes(message));
//            socket.Close();
//            TextBoxMesage.Text = "";
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message);
//        }
//    }
//}
