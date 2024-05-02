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
    class BreadViewModel : ObservableRecipient
    {
        public bool IsSelected { get; set; } = false;
        public RestCollection<Bread> Breads { get; set; }
        public RestCollection<Oven> Ovens { get; set; }
        public RestCollection<Bakery> Bakeries { get; set; }

        public ICommand CreateBreadCommand { get; set; }
        public ICommand UpdateBreadCommand { get; set; }
        public ICommand DeleteBreadCommand { get; set; }


        private Bread selectedBread;
        public Bread SelectedBread 
        {
            get { return selectedBread; }
            set 
            {
                if (value != null)
                {
                    selectedBread = new Bread();
                    selectedBread.Name = value.Name;
                    selectedBread.IsDessert = value.IsDessert;
                    selectedBread.Weight = value.Weight;
                    selectedBread.BakeryId = value.BakeryId;

                    IsSelected = true;
                    OnPropertyChanged();
                }
                else
                {
                    SelectedBread = new Bread();
                    IsSelected = false;
                }
                (DeleteBreadCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateBreadCommand as RelayCommand)?.NotifyCanExecuteChanged();
                
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

        public BreadViewModel() { }

        public BreadViewModel(RestCollection<Bread> breads,RestCollection<Oven> ovens,RestCollection<Bakery> bakeries) 
        {
            selectedBread = new Bread();
            if (!IsInDesignMode)
            {
                Breads = breads;
                Ovens = ovens;
                Bakeries = bakeries;

                CreateBreadCommand = new RelayCommand(
                    () => Breads.Add(new Bread()
                    {
                        Name = selectedBread.Name,
                        IsDessert = selectedBread.IsDessert,
                        Weight = selectedBread.Weight,
                        BakeryId = selectedBread.BakeryId

                    }));

                DeleteBreadCommand = new RelayCommand(
                    async () =>
                    {
                        await Breads.Delete(selectedBread.BreadId);
                        await Bakeries.Refresh();
                        await Ovens.Refresh();
                        IsSelected = false;
                    },
                    () => IsSelected == true);

                UpdateBreadCommand = new RelayCommand(
                    () => Breads.Update(SelectedBread),
                    () => IsSelected == true
                    );
            }
        }
    }
}
