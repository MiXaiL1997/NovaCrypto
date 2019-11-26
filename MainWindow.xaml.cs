using JsonFx.IO;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NovaCrypto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ScytalEnCryptedText1.IsReadOnlyCaretVisible = true;
        }

        void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsGood))
                e.CancelCommand();
        }

        bool IsGood(char c)
        {
            if (c >= '1' && c <= '9')
                return true;
            return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ScytalEnCryptedText1.IsReadOnly = true;
            ScytalEnCryptedText1.IsReadOnlyCaretVisible = false;
            ScytalDeCryptedText.IsReadOnly = true;
            ScytalDeCryptedText.IsReadOnlyCaretVisible = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScytalEnCryptedText1.Text = ScytaleCipher.Encrypt(ScytalInputText1.Text, Convert.ToInt32(diameter1.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ScytalDeCryptedText.Text = ScytaleCipher.Decrypt(ScytaEnCryptedText2.Text,Convert.ToInt32(diameter2.Text));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string Input = StreamInputText1.Text;
            string Output = String.Empty;
            int keyS = Convert.ToInt32(key.Text);
            for (int i = 0; i < Input.Length; i++)
            {
                char s = Input[i];
                Output = Output + new CaesarCipher().Encrypt(s.ToString(), keyS);
                keyS = (int)RandomGenerator.congruential(keyS, 20);
            }
            StreamEnCryptedText.Text = Output;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StreamDeCryptedText1.Text = new CaesarCipher().Decrypt(StreamEnCryptedText2.Text, Convert.ToInt32(key2.Text));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string Input = StreamEnCryptedText2.Text;
            string Output = String.Empty;
            int keyS = Convert.ToInt32(key.Text);
            for (int i = 0; i < Input.Length; i++)
            {
                char s = Input[i];
                Output = Output + new CaesarCipher().Decrypt(s.ToString(), keyS);
                keyS = (int)RandomGenerator.congruential(keyS, 20);
            }
            StreamDeCryptedText1.Text = Output;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if ((RSAP.Text.Length > 0) && (RSAQ.Text.Length > 0))
            {
                long p = Convert.ToInt64(RSAP.Text);
                long q = Convert.ToInt64(RSAQ.Text);

                if (RSAChiper.IsTheNumberSimple(p) && RSAChiper.IsTheNumberSimple(q))
                {
                    string s = RSAInput.Text;
                    s = s.ToUpper();
                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long d = RSAChiper.Calculate_d(m);
                    long e_ = RSAChiper.Calculate_e(d, m);
                    var result = RSAChiper.RSA_Endoce(s, e_, n);
                    foreach(var Item in result)
                    {
                        RSAOutput.Text += Item + Environment.NewLine;
                    }
                    RSAD.Text = d.ToString();
                    RSAN.Text = n.ToString();
                    RSAInput2.Text = RSAOutput.Text;
                    RSAP2.Text = RSAP.Text;
                    RSAQ2.Text = RSAQ.Text;
                    RSAD2.Text = RSAD.Text;
                    RSAN2.Text = RSAN.Text;
                }
                else
                    MessageBox.Show("p или q - не простые числа!");
            }
            else
                MessageBox.Show("Введите p и q!");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if ((RSAD.Text.Length > 0) && (RSAN.Text.Length > 0))
            {
                long d = Convert.ToInt64(RSAD.Text);
                long n = Convert.ToInt64(RSAN.Text);

                List<string> input = new List<string>();

                var input2 = RSAInput2.Text.Split();

                foreach(var Item in input2)
                {
                    if (Item != "")
                    {
                        input.Add(Item);
                    }
                }

                string result = RSAChiper.RSA_Dedoce(input, d, n);
                RSAOutput2.Text = result;

            }
            else
                MessageBox.Show("Введите секретный ключ!");
        }
    }
}
