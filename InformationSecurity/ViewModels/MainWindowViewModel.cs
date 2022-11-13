using System;
using System.IO;
using ReactiveUI;
using System.Text;
using System.Collections.Generic;

namespace InformationSecurity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
         public MainWindowViewModel()
        {
            bRes = new List<byte>();
        }
        
        public StringBuilder Result{
            get => _result;
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
        
        public int SelectedIndexComboBox
        {
            get => _selectedIndexComboBox;
            set => this.RaiseAndSetIfChanged(ref _selectedIndexComboBox, value);
        }
        
       

        public void ReadFromFile()
        {
            try
            {
                if (_selectedIndexComboBox == 0) Key = File.ReadAllText(_pathToFile);
                if (_selectedIndexComboBox == 1) Message = File.ReadAllText(_pathToFile);
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
            byte[] bMessage = Encoding.Default.GetBytes(Message);
            byte[] bKey = Encoding.Default.GetBytes(Key);
            bRes.Clear();
            for (int i = 0; i < bMessage.Length; i++){
                bRes.Add((byte)(bMessage[i] ^ bKey[i % bKey.Length]));
            }
            Res = Encoding.Default.GetString(bRes.ToArray());
        }

        private string _key;
        
        public string Res 
        { 
            get => _res; 
            set => this.RaiseAndSetIfChanged(ref _res, value); 
        }
        private string _res;

        List<byte> bRes;
        private StringBuilder  _result;
        private string _message;
        private string _pathToFile;
        private int _selectedIndexComboBox;
    }
}