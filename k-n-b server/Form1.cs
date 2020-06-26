using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace k_n_b_server
{
    public partial class Form1 : Form
    {

        private int port2listen = 7676;
        private int port2send = 6767;
        private string gameValue = null;
        private string gameValueEnemy = null;
        UdpClient receiver;
        Thread potok1;

        public Form1()
        {
            InitializeComponent();
            this.receiver = new UdpClient(port2listen); // UdpClient для получения данных
            this.potok1 = new Thread(getValue); // создание отдельного потока
            this.potok1.Start(); // запуск потока
        }
        
        private void setValue(string vl)
        {
            gameValue = vl;
            sendValue(gameValue);
            sendValue(gameValue);
            switch (gameValue)
            {
                case "kam":
                    label3.Text = "Камень";
                    checkWin();
                    break;
                case "noz":
                    label3.Text = "Ножницы";
                    checkWin();
                    break;

                case "bum":
                    label3.Text = "Бумагу";
                    checkWin();
                    break;
            }

        }
        private void checkWin()
        {
            if(gameValue == null)
            {
                if (this.label9.InvokeRequired)
                {
                    this.label9.BeginInvoke((MethodInvoker)delegate () {
                        label9.Text = "Ожидается ваш ход";
                    });
                } else
                {
                    label9.Text = "Ожидается ваш ход";
                }
            } else if(gameValueEnemy == null)
            {
                if (this.label9.InvokeRequired)
                {
                    this.label9.BeginInvoke((MethodInvoker)delegate () {
                        label9.Text = "Ожидается ход противника";
                    });
                }
                else
                {
                    label9.Text = "Ожидается ход противника";
                }
            } else
            {
                string result = "";
                if (gameValue.Equals(gameValueEnemy))
                {
                    result = "Ничья!";
                }else if (gameValue.Equals("kam"))
                {
                    if(gameValueEnemy.Equals("noz"))
                    {
                        result = "Вы выиграли!";
                    }
                    if (gameValueEnemy.Equals("bum"))
                    {
                        result = "Вы проиграли!";
                    }
                }else if(gameValue.Equals("noz"))
                {
                    if (gameValueEnemy.Equals("kam"))
                    {
                        result = "Вы проиграли!";
                    }
                    if (gameValueEnemy.Equals("bum"))
                    {
                        result = "Вы выиграли!";
                    }
                }else if (gameValue.Equals("bum"))
                {
                    if (gameValueEnemy.Equals("kam"))
                    {
                        result = "Вы выиграли!";
                    }
                    if (gameValueEnemy.Equals("noz"))
                    {
                        result = "Вы проиграли!";
                    }
                }
                if (this.label7.InvokeRequired)
                {
                    this.label7.BeginInvoke((MethodInvoker)delegate () {
                        label7.Text = result;
                    });
                }
                else
                {
                    label7.Text = result;
                }
                if (this.label9.InvokeRequired)
                {
                    this.label9.BeginInvoke((MethodInvoker)delegate () {
                        label9.Text = "Игра окончена";
                    });
                }
                else
                {
                    label9.Text = "Игра окончена";
                }
                if (this.button4.InvokeRequired)
                {
                    this.button4.BeginInvoke((MethodInvoker)delegate () {
                        button4.Enabled = true;
                    });
                }
                else
                {
                    button4.Enabled = true;
                }
                if (this.button1.InvokeRequired)
                {
                    this.button1.BeginInvoke((MethodInvoker)delegate () {
                        button1.Enabled = false;
                    });
                }
                else
                {
                    button1.Enabled = false;
                }
                if (this.button2.InvokeRequired)
                {
                    this.button2.BeginInvoke((MethodInvoker)delegate () {
                        button2.Enabled = false;
                    });
                }
                else
                {
                    button2.Enabled = false;
                }
                if (this.button3.InvokeRequired)
                {
                    this.button3.BeginInvoke((MethodInvoker)delegate () {
                        button3.Enabled = false;
                    });
                }
                else
                {
                    button3.Enabled = false;
                }
                switch (gameValueEnemy)
                {
                    case "kam":
                        if (this.label5.InvokeRequired)
                        {
                            this.label5.BeginInvoke((MethodInvoker)delegate ()
                            {
                                label5.Text = "Камень";
                            });
                        }
                        else
                        {
                            label5.Text = "Камень";
                        }
                        break;
                    case "noz":
                        if (this.label5.InvokeRequired)
                        {
                            this.label5.BeginInvoke((MethodInvoker)delegate ()
                            {
                                label5.Text = "Ножницы";
                            });
                        }
                        else
                        {
                            label5.Text = "Ножницы";
                        }
                        break;

                    case "bum":
                        if (this.label5.InvokeRequired)
                        {
                            this.label5.BeginInvoke((MethodInvoker)delegate ()
                            {
                                label5.Text = "Бумагу";
                            });
                        }
                        else
                        {
                            label5.Text = "Бумагу";
                        }
                        break;
                }

            }

        }
        private void getValue()
        {
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receiver.Receive(ref remoteIp);

                    // Преобразуем и отображаем данные
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);

                    switch (message)
                    {
                        case "kam":
                            gameValueEnemy = message;
                            if (this.label5.InvokeRequired)
                            {
                                this.label5.BeginInvoke((MethodInvoker)delegate () {
                                    label5.Text = "******";
                                });
                            }
                            else
                            {
                                label5.Text = "******";
                            }
                            checkWin();
                            break;
                        case "noz":
                            gameValueEnemy = message;
                            if (this.label5.InvokeRequired)
                            {
                                this.label5.BeginInvoke((MethodInvoker)delegate () {
                                    label5.Text = "******";
                                });
                            }
                            else
                            {
                                label5.Text = "******";
                            }
                            checkWin();
                            break;
                            
                        case "bum":
                            gameValueEnemy = message;
                            if (this.label5.InvokeRequired)
                            {
                                this.label5.BeginInvoke((MethodInvoker)delegate () {
                                    label5.Text = "******";
                                });
                            }
                            else
                            {
                                label5.Text = "******";
                            }
                            checkWin();
                            break;
                        case "clean":
                            this.label3.BeginInvoke((MethodInvoker)delegate () {
                                label3.Text = "";
                            });
                            this.label5.BeginInvoke((MethodInvoker)delegate () {
                                label5.Text = "";
                            });
                            this.label7.BeginInvoke((MethodInvoker)delegate () {
                                label7.Text = "";
                            });
                            this.label9.BeginInvoke((MethodInvoker)delegate () {
                                label9.Text = "";
                            });
                            this.button1.BeginInvoke((MethodInvoker)delegate () {
                                button1.Enabled = true;
                            });
                            this.button2.BeginInvoke((MethodInvoker)delegate () {
                                button2.Enabled = true;
                            });
                            this.button3.BeginInvoke((MethodInvoker)delegate () {
                                button3.Enabled = true;
                            });
                            this.button4.BeginInvoke((MethodInvoker)delegate () {
                                button4.Enabled = false;
                            });
                            gameValue = null;
                            gameValueEnemy = null;
                            break;
                    }
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
        private void sendValue(string message)
        {
            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки сообщений
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);

                sender.Send(data, data.Length, "localhost", port2send); // отправка

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setValue("kam");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setValue("noz");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setValue("bum");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendValue("clean");
            sendValue("clean");
            label3.Text = "";
            label5.Text = "";
            label7.Text = "";
            label9.Text = "";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            gameValue = null;
            gameValueEnemy = null;
        }
    }
}
