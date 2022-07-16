using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ClipboardWorker.Model;
using static System.Net.Mime.MediaTypeNames;

namespace ClipboardWorker.ViewModels
{
    internal class MainViewModel
    {
        private MainWindow _mainWindow;

        public ObservableCollection<ItemModel> Items { get; set; }
        private ItemModel _selectedItem = null!;

        public ItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(value, "SelectedItem");
            }
        }

        public delegate void SetupClipboardEventHandler(object? sender, PropertyChangedEventArgs e, ItemModel itemModel);
        public event SetupClipboardEventHandler? PropertyChanged;

        private void OnPropertyChanged(ItemModel value, [CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName), value);
            }
        }
        private Command _removeItemCommand;
        public Command RemoveItemCommand
        {
            get
            {
                if (_removeItemCommand == null)
                {
                    _removeItemCommand = new Command((SelectedItem) => { Items.Remove((ItemModel)SelectedItem); });
                }
                return _removeItemCommand;
            }
            private set
            {
                _removeItemCommand = value;
            }
        }

        private void MainViewModel_SetupClipboard(object? sender, PropertyChangedEventArgs e, ItemModel itemModel)
        {
            if (itemModel == null) return;
            Clipboard.SetText(itemModel.Text);

        }

        public MainViewModel()
        {
            Items = new ObservableCollection<ItemModel>();
            _mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            PropertyChanged += MainViewModel_SetupClipboard;
            
            UpdateClipboard();
        }
        private void UpdateClipboard()
        {
            
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    ItemModel itemClipboard = ModelFactory.GetItemModel(Clipboard.GetText());
                    if (itemClipboard.Text != null && !Items.Any(x => x.Text == itemClipboard.Text))
                    {
                        _mainWindow.Dispatcher.Invoke(() =>
                        {
                            Items.Add(itemClipboard);
                        });
                    }
                    Thread.Sleep(500);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();


        }

    }
}
