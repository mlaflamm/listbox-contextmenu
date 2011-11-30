using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MvvmFoundation.Wpf;

namespace WpfListContextMenu
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ICommand ShowSelectedCommand { get; private set; }

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>()
                        {
                            new ItemViewModel() {Name = "Item 1"},
                            new ItemViewModel() {Name = "Item 2"},
                            new ItemViewModel() {Name = "Item 3"},
                            new ItemViewModel() {Name = "Item 4"},
                            new ItemViewModel() {Name = "Item 5"},
                            new ItemViewModel() {Name = "Item 6"},
                            new ItemViewModel() {Name = "Item 7"},
                            new ItemViewModel() {Name = "Item 8"},
                            new ItemViewModel() {Name = "Item 9"},
                        };

            ShowSelectedCommand = new RelayCommand<IEnumerable>(ShowSelected, CanShowSelected);
        }

        private void ShowSelected(IEnumerable items)
        {
            if (items != null)
            {
                StringWriter writer = new StringWriter();
                foreach (ItemViewModel item in items)
                {
                    writer.WriteLine(item.Name);
                }
                MessageBox.Show(writer.ToString());
            }
        }

        private bool CanShowSelected(IEnumerable items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    return true;
                }
            }
            return false;
        }
    }
}