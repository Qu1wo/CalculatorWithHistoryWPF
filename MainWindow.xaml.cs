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

namespace CalculatorWithHistory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentOperator = "";
        private double firstOperand = 0;
        private bool isNewInput = true;
        private double memoryValue = 0;
        private List<string> history = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate()
        {
            double secondOperand = Convert.ToDouble(ResultTextBox.Text);
            double result = 0;
            string expression = $"{firstOperand} {currentOperator} {secondOperand}";

            try
            {
                switch (currentOperator)
                {
                    case "+":
                        result = firstOperand + secondOperand;
                        break;

                    case "-":
                        result = firstOperand - secondOperand;
                        break;

                    case "*":
                        result = firstOperand * secondOperand;
                        break;

                    case "/":
                        if (secondOperand == 0)
                            throw new DivideByZeroException();
                        result = firstOperand / secondOperand;
                        break;

                    default:
                        
                        return;
                }

                history.Add($"{expression} = {result}");
                ResultTextBox.Text = result.ToString();
                isNewInput = true;
            }
            catch (DivideByZeroException)
            {
                ResultTextBox.Text = "Ошибка: деление на 0";
                isNewInput = true;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            /// <remarks>null block</remarks>
        }

        private void ButtonMCClick(object sender, RoutedEventArgs e)
        {
            memoryValue = 0;
        }

        private void ButtonMRClick(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = memoryValue.ToString();
            isNewInput = true;
        }

        private void ButtonMSClick(object sender, RoutedEventArgs e)
        {
            memoryValue = Convert.ToDouble(ResultTextBox.Text);
            isNewInput = true;
        }

        private void ButtonMPlusClick(object sender, RoutedEventArgs e)
        {
            memoryValue += Convert.ToDouble(ResultTextBox.Text);
            isNewInput = true;
        }

        private void ButtonMMinusClick(object sender, RoutedEventArgs e)
        {
            memoryValue -= Convert.ToDouble(ResultTextBox.Text);
            isNewInput = true;
        }

        private void ButtonDelClick(object sender, RoutedEventArgs e)
        {
            if (!isNewInput && ResultTextBox.Text.Length > 1)
            {
                ResultTextBox.Text = ResultTextBox.Text.Substring(0, ResultTextBox.Text.Length - 1);
            }
            else if (!isNewInput && ResultTextBox.Text.Length == 1)
            {
                ResultTextBox.Text = "0";
                isNewInput = true;
            }
        }

        private void ButtonCEClick(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "0";
            isNewInput = true;
        }

        private void ButtonCClick(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "0";
            currentOperator = "";
            firstOperand = 0;
            isNewInput = true;
        }

        private void ButtonReverseClick(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(ResultTextBox.Text);
            value = -value;
            ResultTextBox.Text = value.ToString();
        }

        private void ButtonRootClick(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(ResultTextBox.Text);
            if (value >= 0)
            {
                double result = Math.Sqrt(value);
                history.Add($"√{value} = {result}");
                ResultTextBox.Text = result.ToString();
                isNewInput = true;
            }
            else
            {
                ResultTextBox.Text = "Ошибка";
                isNewInput = true;
            }
            
        }

        private void ButtonDivideClick(object sender, RoutedEventArgs e)
        {
            if (!isNewInput)
            {
                Calculate();
            }

            firstOperand = Convert.ToDouble(ResultTextBox.Text);
            currentOperator = "/";
            isNewInput = true;

        }

        private void ButtonPercentClick(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(ResultTextBox.Text);
            double result = value / 100;
            ResultTextBox.Text = result.ToString();
            isNewInput = true;
        }

        private void ButtonMultiplyClick(object sender, RoutedEventArgs e)
        {
            if (!isNewInput)
            {
                Calculate();
            }

            firstOperand = Convert.ToDouble(ResultTextBox.Text);
            currentOperator = "*";
            isNewInput = true;
        }

        private void ButtonRevNumClick(object sender, RoutedEventArgs e)
        {
            double value = Convert.ToDouble(ResultTextBox.Text);
            if (value >= 0)
            {
                double result = 1 / value;
                history.Add($"1/{value} = {result}");
                ResultTextBox.Text = result.ToString();
                isNewInput = true;
            }
            else
            {
                ResultTextBox.Text = "Ошибка";
                isNewInput = true;
            }
        }

        private void ButtonMinusClick(object sender, RoutedEventArgs e)
        {
            if (!isNewInput)
            {
                Calculate();
            }

            firstOperand = Convert.ToDouble(ResultTextBox.Text);
            currentOperator = "-";
            isNewInput = true;
        }

        private void ButtonSumClick(object sender, RoutedEventArgs e)
        {
            if (!isNewInput)
            {
                Calculate();
            }

            firstOperand = Convert.ToDouble(ResultTextBox.Text);
            currentOperator = "+";
            isNewInput = true;
        }

        private void ButtonCommaClick(object sender, RoutedEventArgs e)
        {
            if (!ResultTextBox.Text.Contains(","))
            {
                if (isNewInput)
                {
                    ResultTextBox.Text = "0,";
                    isNewInput = false;
                }
                else
                {
                    ResultTextBox.Text += ",";
                }
            }
        }

        private void ButtonEqualClick(object sender, RoutedEventArgs e)
        {
            Calculate();
            currentOperator = "";
            isNewInput = true;
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string number = button.Content.ToString();

            if (isNewInput || ResultTextBox.Text == "0")
            {
                ResultTextBox.Text = number;
                isNewInput = false;
            }
            else
            {
                ResultTextBox.Text += number;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (history.Count > 0)
            {
                string historyText = string.Join("\n", history);
                MessageBox.Show(historyText, "История вычислений", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("История пуста", "История вычислений", MessageBoxButton.OK);
            }
        }

        private void MenuItemEditHistoryClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemInfoClick(object sender, RoutedEventArgs e)
        {
            string developerInformation =

                "👨‍💻 Разработчик: Глуховский Кирилл\n" +
                "🏫 Учебное заведение: ГБПОУ КК ККЭП\n" +
                "📚 Группа: 106-Д9-2ИСП\n" +
                "📅 Год создания: 2026\n";

                MessageBox.Show(developerInformation, "Разработчик", MessageBoxButton.OK);
        }
    }
}
