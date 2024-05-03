using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EO1BOA_GUI_2023242_WPF_Client.ViewModels
{
    class OvenViewModel : ObservableRecipient
    {

        public bool IsSelected { get; set; } = false;
        public RestCollection<Bread> Breads { get; set; }
        public RestCollection<Oven> Ovens { get; set; }

        public ICommand CreateOvenCommand { get; set; }
        public ICommand UpdateOvenCommand { get; set; }
        public ICommand DeleteOvenCommand { get; set; }


        private Oven selectedOven;
        public Oven SelectedOven
        {
            get { return selectedOven; }
            set
            {
                if (value != null)
                {
                    selectedOven = new Oven();
                    selectedOven.BreadCapacity = value.BreadCapacity;
                    selectedOven.BakingTime = value.BakingTime;
                    selectedOven.Price = value.Price;

                    IsSelected = true;
                    OnPropertyChanged();
                }
                else
                {
                    selectedOven = new Oven();
                    IsSelected = false;
                }
                (DeleteOvenCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateOvenCommand as RelayCommand)?.NotifyCanExecuteChanged();

            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public OvenViewModel() { }

        public OvenViewModel( RestCollection<Oven> ovens, RestCollection<Bread> breads)
        {
            SelectedOven = new Oven();
            if (!IsInDesignMode)
            {
                Breads = breads;
                Ovens = ovens;

                CreateOvenCommand = new RelayCommand(
                    () => Ovens.Add(new Oven()
                    {
                        BreadCapacity = SelectedOven.BreadCapacity,
                        BakingTime = SelectedOven.BakingTime,
                        Price = SelectedOven.Price
                    }));

                DeleteOvenCommand = new RelayCommand(
                    async () =>
                    {
                        await Ovens.Delete(SelectedOven.BreadId);
                        await Breads.Refresh();
                        IsSelected = false;
                    },
                    () => IsSelected == true);

                UpdateOvenCommand = new RelayCommand(
                    () => Ovens.Update(SelectedOven),
                    () => IsSelected == true
                    );
            }
        }
    }
}
