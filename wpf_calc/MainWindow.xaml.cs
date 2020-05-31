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

namespace wpf_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox4.IsReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isParse = int.TryParse(textBox1.Text, out int ss1);
            if ((isParse) && (ss1>1) && (ss1<17))
            {
                textBox1.Opacity = 1;
            }
            else
            {
                textBox1.Opacity = 0.6;
            }
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "От 2 до 16")
            {
                textBox1.Background = Brushes.Transparent;
                textBox1.Clear();
            }
        }

        private void textBox1_LostFocus(object sender, System.EventArgs e)
        {
            bool isParse = int.TryParse(textBox1.Text, out int ss1);
            if ((!isParse) || (ss1 < 2) || (ss1 > 16))
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1.Background = Brushes.Pink;
                    MessageBox.Show("Неверный ввод данных!");
                }
                textBox1.Text = "От 2 до 16";
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isParse = int.TryParse(textBox2.Text, out int ss2);
            if ((isParse) && (ss2 > 1) && (ss2 < 17))
            {
                textBox2.Opacity = 1;
            }
            else
            {
                textBox2.Opacity = 0.6;
            }
        }

        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "От 2 до 16")
            {
                textBox2.Background = Brushes.Transparent;
                textBox2.Clear();
            }
        }

        private void textBox2_LostFocus(object sender, System.EventArgs e)
        {
            bool isParse = int.TryParse(textBox2.Text, out int ss2);
            if ((!isParse) || (ss2 < 2) || (ss2 > 16))
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox2.Background = Brushes.Pink;
                    MessageBox.Show("Неверный ввод данных!");
                }
                textBox2.Text = "От 2 до 16";
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((textBox3.Text != "Не более 16 цифр") && (textBox3.Text.Length < 17))
            {
                textBox3.Opacity = 1;
            }
            else
            {
                textBox3.Opacity = 0.6;
            }
        }

        private void textBox3_GotFocus(object sender, EventArgs e)
        {
            if (textBox3.Text == "Не более 16 цифр")
            {
                textBox3.Background = Brushes.Transparent;
                textBox3.Clear();
            }
        }

        private void textBox3_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Text = "Не более 16 цифр";
            }
            else
            if (textBox3.Text.Length > 16)
            {
                textBox3.Background = Brushes.Pink;
                MessageBox.Show("Неверный ввод данных!\nЧисло должно иметь не более 16 цифр!");
                textBox3.Text = "Не более 16 цифр";
            }
            else
            {
                string numerals = "0123456789AaBbCcDdEeFf";
                if (textBox3.Text.Any(x => !numerals.Contains(x)))
                {
                    textBox3.Background = Brushes.Pink;
                    MessageBox.Show("Неверный ввод данных!\nНесуществующая цифра!");
                    textBox3.Text = "Не более 16 цифр";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isParse1 = int.TryParse(textBox1.Text, out int ss1);
            bool isParse2 = int.TryParse(textBox2.Text, out int ss2);
            if (isParse1 && isParse2)
            {
                string numerals = "0123456789ABCDEF";
                int[] numberIn = new int[textBox3.Text.Length];
                textBox3.Text = textBox3.Text.ToUpper();
                bool isNormal = true;
                for (int i = 0; i < textBox3.Text.Length; i++)
                {
                    numberIn[i] = numerals.IndexOf(textBox3.Text[i]);
                    if (numberIn[i] >= ss1)
                    {
                        isNormal = false;
                        break;
                    }
                }
                if (isNormal)
                {
                    button.Content = "ПЕРЕВЕСТИ СНОВА!";
                    if (ss1 == ss2)
                    {
                        textBox4.Text = textBox3.Text;
                    }
                    else
                    {
                        ulong summary = 0;
                        foreach (int pos in numberIn)
                        {
                            summary = summary * (ulong)ss1 + (ulong)pos;
                        }
                        if (ss2 == 10)
                        {
                            textBox4.Text = Convert.ToString(summary);
                        }
                        else
                        {
                            int[] numberOut = new int[4 * textBox3.Text.Length];
                            int length = 0;
                            while (summary >= (ulong)ss2)
                            {
                                numberOut[length] = (int)(summary % (ulong)ss2);
                                summary /= (ulong)ss2;
                                length++;
                            }
                            numberOut[length++] = (int)summary;
                            for (int i = 0; i < length / 2; i++)
                            {
                                numberOut[i] += numberOut[length - i - 1];
                                numberOut[length - i - 1] = numberOut[i] - numberOut[length - i - 1];
                                numberOut[i] -= numberOut[length - i - 1];
                            }
                            textBox4.Clear();
                            for (int i = 0; i < length; i++)
                            {

                                textBox4.Text += numerals[numberOut[i]];
                            }
                        }
                    }
                }
                else
                {
                    textBox3.Opacity = 0.6;
                    textBox3.Background = Brushes.Pink;
                    MessageBox.Show("Неверный ввод данных!\nНаличие цифры, не входящей в систему счисления!");
                }
            }
            else
            {
                MessageBox.Show("Неверный ввод данных!\nНеверно задана система счисления!");
            }
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox4.Text == "Результат")
            {
                textBox4.Opacity = 0.6;
            }
            else
            {
                textBox4.Opacity = 1;
            }
        }
    }
}
