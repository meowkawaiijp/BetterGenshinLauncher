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
using System;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.IO;
using Microsoft.Win32;

namespace GenshinPublic
{
    /// <summary>
    /// Ini ファイルの読み書きを扱うクラスです。
    /// </summary>
    public class IniFile
    {
        [DllImport("kernel32.dll")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        /// <summary>
        /// Ini ファイルのファイルパスを取得、設定します。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="filePath">Ini ファイルのファイルパス</param>
        public IniFile(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// Ini ファイルから文字列を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public string GetString(string section, string key, string defaultValue = "")
        {
            var sb = new StringBuilder(1024);
            var r = GetPrivateProfileString(section, key, defaultValue, sb, (uint)sb.Capacity, FilePath);
            return sb.ToString();
        }
        /// <summary>
        /// Ini ファイルから整数を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public int GetInt(string section, string key, int defaultValue = 0)
        {
            return (int)GetPrivateProfileInt(section, key, defaultValue, FilePath);
        }
        /// <summary>
        /// Ini ファイルに文字列を書き込みます。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="value">書き込む値</param>
        /// <returns></returns>
        public bool WriteString(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, FilePath);
        }
    }
    public partial class MainWindow : Window
    {


        public static IniFile ini;
        public MainWindow()
        {
            //設定ファイルの処理
            if(File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\settings.ini"))
            {
                ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\settings.ini");
            }
            else
                   {
                ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\settings.ini");
                ini.WriteString("Genshin", "Path", @"C:\Program Files\Genshin Impact\Genshin Impact game\GenshinImpact.exe");
                ini.WriteString("Genshin", "Senden", "True");
                

            }
         
            string senden  = ini.GetString("Genshin", "Senden", "True");
            if (senden == "True" || senden == "true")
            {
                System.Diagnostics.Process.Start("https://twitter.com/alpha_korona");
                System.Diagnostics.Process.Start("https://github.com/alphaikaduki");
              
                
            }
           //GUIの読み込み
            InitializeComponent();
            
            genshinpath.Text =  ini.GetString("Genshin", "Path", @"C:\Program Files\Genshin Impact\Genshin Impact game\GenshinImpact.exe");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      
        private void genshinrun_Click(object sender, RoutedEventArgs e)
        {
            //セットアップ
            
            var genshin = new System.Diagnostics.Process();
            string path = genshinpath.Text;
            if (path == null)
            {
                return;
            }
       
            genshin.StartInfo.FileName = @path;
            ini.WriteString("Genshin", "Path", path);
            try
            {
               //原神を起動
                genshin.Start();

            }
            catch (InvalidCastException)
            {
                return;
                // error
            }
        }

        private void rogubo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://webstatic-sea.mihoyo.com/ys/event/signin-sea/index.html?act_id=e202102251931481&lang=ja-jp");
            
        }

        private void read_Click(object sender, RoutedEventArgs e)
        {
            // ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "実行ファイル (*.exe)|*.exe";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                // 選択されたファイル名 (ファイルパス) をメッセージボックスに表示
                MessageBox.Show(dialog.FileName + " が読み込まれました。");
                genshinpath.Text = dialog.FileName;
            }
        }

        private void updatecheck_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://act.hoyolab.com/ys/app/interactive-map/index.html?lang=ja-jp#");
        }

        private void ikusei_syoukai_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://enka.shinshin.moe/");
        }
  


          private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
          {

          }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey key = hklm.OpenSubKey(@"HKEY_CURRENT_USER\SOFTWARE\miHoYo\Genshin Impact");
                var data = key.GetValue("GENERAL_DATA_h2389025596");
                userdata.Text = (string)data;
                key.Close();
            }
            catch
            {

            }
            }

        private void userdata_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    }
