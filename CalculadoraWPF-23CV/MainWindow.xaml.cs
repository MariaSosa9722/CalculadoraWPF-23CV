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

namespace CalculadoraWPF_23CV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {

                Button button = (Button)sender;

                string value = (string)button.Content;

                if (IsNumber(value))
                {
                    HandleNumbers(value);

                }
                else if (IsOperator(value))
                {
                    HandleOperator(value);
                }
                else if (value == "CE")
                {
                    Screen.Clear();
                }
                else if (value == "=")
                {
                    HandleEquals(Screen.Text);
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error " + ex.Message);
            }

        }


        //Métodos auxiliares
        private bool IsNumber(string num)
        {
            return double.TryParse(num, out _);
        }

        private void HandleNumbers(string value)
        {
            if (String.IsNullOrEmpty(Screen.Text))
            {
                Screen.Text = value;
            }
            else
            {
                Screen.Text += value;
            }

        }

        private bool IsOperator(string possibleOperator)
        {
            return possibleOperator == "+" || possibleOperator == "-"
                || possibleOperator == "*" || possibleOperator == "/";
        }


        private void HandleOperator(string value)
        {
            if (!String.IsNullOrEmpty(Screen.Text) && !ContainsOtherOperators(Screen.Text))
            {
                Screen.Text += value;
            }
        }


        //VALIDACION DE NO AGREGAR MAS DE DOS VECES EL OPERADOR

        private bool ContainsOtherOperators(string screenContent)
        {
            return screenContent.Contains("+") || screenContent.Contains("-")
                   || screenContent.Contains("*") || screenContent.Contains("/");
        }

        private void HandleEquals(string screenContent)
        {
            string op = FindOperator(screenContent);

            if (!String.IsNullOrEmpty(op))
            {
                switch (op)
                {
                    case "+":

                        Screen.Text = Sum();

                        break;
                    default:
                        break;
                }

            }
    

        }

        private string FindOperator(string screenContent)
        {
            foreach (var c in screenContent)
            {
                if (IsOperator(c.ToString()))
                {
                    return c.ToString();
                }
            }
            return " ";
        } 

        private string Sum()
        {
            string[] number = Screen.Text.Split('+');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();

            

        }




    }
}
