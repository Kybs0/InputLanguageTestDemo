using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InputLanguageTestDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CurrentLanguage = GetCultureType();

            //2.PreferredImeConversionMode方案
            //通过InputMethod.PreferredImeConversionMode附加属性，设置输入框的输入法输入转换模式,即限定输入字符类型
            //InputMethod.SetPreferredImeConversionMode(InputTestTextBox0, ImeConversionModeValues.Alphanumeric);
            //InputMethod.SetPreferredImeConversionMode(InputTestTextBox1, ImeConversionModeValues.Native);
            //3.IsInputMethodEnabled方案
            InputMethod.SetIsInputMethodEnabled(InputTestTextBox0, false);
            InputMethod.SetIsInputMethodEnabled(InputTestTextBox1, true);
        }

        #region 1.InputLanguage方案

        private void SwitchCultureButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cultureType = CurrentLanguage;
            if (cultureType == "en-US")
            {
                SwitchToLanguageMode("zh-CN");
            }
            else if (cultureType == "zh-CN")
            {
                SwitchToLanguageMode("en-US");
            }
        }
        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="cultureType">语言项，如zh-CN，en-US</param>
        private void SwitchToLanguageMode(string cultureType)
        {
            var installedInputLanguages = InputLanguage.InstalledInputLanguages;

            if (installedInputLanguages.Cast<InputLanguage>().Any(i => i.Culture.Name == cultureType))
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo(cultureType));
                CurrentLanguage = cultureType;
            }
        }
        /// <summary>
        /// 获取当前输入法
        /// </summary>
        /// <returns></returns>
        private string GetCultureType()
        {
            var currentInputLanguage = InputLanguage.CurrentInputLanguage;
            var cultureInfo = currentInputLanguage.Culture;
            //同 cultureInfo.IetfLanguageTag;
            return cultureInfo.Name;
        }

        public static readonly DependencyProperty CurrentLanguageProperty = DependencyProperty.Register(
            "CurrentLanguage", typeof(string), typeof(MainWindow), new PropertyMetadata(default(string)));

        public string CurrentLanguage
        {
            get { return (string)GetValue(CurrentLanguageProperty); }
            set { SetValue(CurrentLanguageProperty, value); }
        }

        #endregion

    }
}
