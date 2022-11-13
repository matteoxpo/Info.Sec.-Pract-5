using System;
using System.IO;
using ReactiveUI;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualBasic;

namespace InformationSecurity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
         public MainWindowViewModel()
        {
            bRes = new List<byte>();
            IsButtonClicked = false;
        }
        
        public StringBuilder Result{
            get => _result;
        }

        public bool IsButtonClicked
        {
            get => _isButtonClicked;
            set
            {
                this.RaiseAndSetIfChanged(ref _isButtonClicked, value);
                IsButtonNotClicked = !value;
            }
        }

        private bool _isButtonClicked;
        private bool _isButtonNotClicked;
        
        public bool IsButtonNotClicked
        {
            get => !_isButtonClicked;
            set => this.RaiseAndSetIfChanged(ref _isButtonNotClicked, value);
        }
        

        public string Key
        {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }
        
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        
        public string PathToFile
        {
            get => _pathToFile;
            set => this.RaiseAndSetIfChanged(ref _pathToFile, value);
        }
        
        public int SelectedIndexComboBoxFile
        {
            get => _selectedIndexComboBoxFile;
            set => this.RaiseAndSetIfChanged(ref _selectedIndexComboBoxFile, value);
        }
        
        
        
        
        
        public string StringRes 
        { 
            get => "Array of chars:" + _stringRes; 
            set => this.RaiseAndSetIfChanged(ref _stringRes, value); 
        }

        public string HexRes
        {
            get => "Hex:" +_hexRes;
            set => this.RaiseAndSetIfChanged(ref _hexRes, value);
        }
        
        
        public void ReadFromFile()
        {
            try
            {
                switch (SelectedIndexComboBoxFile)
                {
                    case 0:
                        Key = File.ReadAllText(PathToFile);
                        break;
                    case 1 :
                        Message = File.ReadAllText(PathToFile);
                        break;
                }
            }
            catch (Exception)
            {
                var message = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Ошибка открытия файла");
                message.Show();
            }
        }

        public void EncryptOrDecrypt()
        {
            
            try
            {
                byte[] bMessage = Encoding.Default.GetBytes(Message);
                byte[] bKey = Encoding.Default.GetBytes(Key);
                string trtanspMess = new string(Message.Reverse().ToArray());

                bRes.Clear();
                for (int i = 0; i < bMessage.Length; i++)
                {
                    bRes.Add((byte)(bMessage[i] ^ bKey[i % bKey.Length]));
                }

                StringRes = Encoding.Default.GetString(bRes.ToArray());
                HexRes = Convert.ToHexString(bRes.ToArray());
                IsButtonClicked = true;
            }
            catch (Exception)
            {
                var message = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Поля не были проинциализированы");
                message.Show();
            }
        }

        
       
        private string _stringRes;
        private string _hexRes;

        
        
        
        List<byte> bRes;
        private StringBuilder  _result;
        private string _message;
        private string _key;
        private string _pathToFile;
        private int _selectedIndexComboBoxFile;
    }
}