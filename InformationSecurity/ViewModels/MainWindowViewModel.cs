using System;
using System.IO;
using ReactiveUI;
using System.Text;
using System.Globalization;
using System.Linq;

namespace InformationSecurity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            IsButtonClicked = false;
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
            set { this.RaiseAndSetIfChanged(ref _key, value); }
        }

        public string Message
        {
            get => _message;
            set { this.RaiseAndSetIfChanged(ref _message, value); }
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

        private string _stringInput;

        public string StringInput
        {
            get => _stringInput;
            set => this.RaiseAndSetIfChanged(ref _stringInput, value);
        }

        private string _hexInput;

        public string HexInput
        {
            get => _hexInput;
            set => this.RaiseAndSetIfChanged(ref _hexInput, value);
        }

        private string _binInput;

        public string BinInput
        {
            get => _binInput;
            set => this.RaiseAndSetIfChanged(ref _binInput, value);
        }

        private string _binRes;

        public string BinRes
        {
            get => _binRes;
            set => this.RaiseAndSetIfChanged(ref _binRes, value);
        }


        public string StringRes
        {
            get => $"Результат: {_stringRes}";
            set => this.RaiseAndSetIfChanged(ref _stringRes, value);
        }

        public string HexRes
        {
            get => _hexRes;
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
                    case 1:
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
                var bMessage = Encoding.Default.GetBytes(Message);
                var bKey = Encoding.Default.GetBytes(Key);
                var res = bMessage.Select((b, i) => (byte)(b ^ bKey[i % bKey.Length])).ToArray();


                StringInput = new string($"Сообщение: {Message}\n" +
                                         $"Ключ: {Key}");
                HexInput = new string($"Сообщение: {Convert.ToHexString(bMessage)}\n" +
                                      $"Ключ: {Convert.ToHexString(bKey)}");
                BinInput = new string($"Сообщение: {string.Join("", Message.Select(b => Convert.ToString(b, 2)))}\n" +
                                      $"Ключ: {string.Join("", Key.Select(b => Convert.ToString(b, 2)))}");

                StringRes = new string(Encoding.Default.GetString(res));
                HexRes = new string("Результат: ") + Convert.ToHexString(res);
                BinRes = new string("Результат: ") + string.Join("", res.Select(b => Convert.ToString(b, 2)));
                IsButtonClicked = true;
            }
            catch (Exception)
            {
                IsButtonClicked = false;
                var message = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Поля не были проинциализированы");
                message.Show();
            }
        }


        private string _stringRes;
        private string _hexRes;
        private string _message;
        private string _key;
        private string _pathToFile;
        private int _selectedIndexComboBoxFile;
    }
}