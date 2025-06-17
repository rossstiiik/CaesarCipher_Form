using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaesarCipher
{
   
    public partial class MainForm : Form
    {
        private string russianSmall = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private string russianBig = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private string englishSmall = "abcdefghijklmnopqrstuvwxyz";
        private string englishBig = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
     //   public bool isRussianSmall = false;
      //  public bool isRussianBig = false;
     //   public bool isEnglishSmall = false;
     //   public bool isEnglishBig = false;
    //    public int alphabet;
        
        public int shift;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtShift.Text, out shift)) // Объявляем shift здесь
            {
                MessageBox.Show("Введите целое число для сдвига!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                 
            }
            
            txtOutput.Text = CaesarCipher(txtInput.Text, shift); // Используем shift
            //alphabet = 0;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtShift.Text, out shift)) // Объявляем shift здесь
            {
                MessageBox.Show("Введите целое число для сдвига!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            txtOutput.Text = CaesarCipher(txtInput.Text, -shift); // Используем shift с минусом
          
        }

        private string CaesarCipher(string text, int shift)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                string currentAlphabet = null;

                // Определяем, к какому алфавиту принадлежит символ
                if (russianSmall.Contains(c.ToString()))
                {
                    currentAlphabet = russianSmall;
                    
                } 
                else if (russianBig.Contains(c.ToString()))
                {
                    currentAlphabet = russianBig; 
                    
                }
                    
                else if (englishSmall.Contains(c.ToString()))
                {
                    currentAlphabet = englishSmall;
                    
                }

                else if (englishBig.Contains(c.ToString()))
                {
                    currentAlphabet = englishBig;
                    
                }
                    

                if (currentAlphabet != null)
                {
                    // Применяем сдвиг только по своему алфавиту
                    int index = currentAlphabet.IndexOf(c);
                    int newIndex = (index + shift) % currentAlphabet.Length;
                    if (newIndex < 0)
                        newIndex += currentAlphabet.Length;
                    result[i] = currentAlphabet[newIndex];
                }
                else
                {
                    // Оставляем символы, не входящие в алфавиты
                    result[i] = c;
                }
            }

            return new string(result);
           // alphabet = 0;
        }

        // Ограничиваем ввод в txtShift только цифрами и минусом
        private void txtShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Запрещаем минус не в начале
            if (e.KeyChar == '-' && ((TextBox)sender).SelectionStart != 0)
            {
                e.Handled = true;
            }
        }
    }
}