using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EO1BOA_GUI_2023242_WPF_Client.ViewModels
{
    class BakeryViewModel : ObservableRecipient
    {
        
        public bool IsSelected { get; set; } = false;
        public RestCollection<Bread> Breads { get; set; }
        public RestCollection<Bakery> Bakeries { get; set; }
        public RestService NonCrud { get; set; }
        public ObservableCollection<Bread> SweetsNonCrud { get; set; }
        public ObservableCollection<Bread> BreadsNonCrud { get; set; }
        public ICommand CreateBakeryCommand { get; set; }
        public ICommand UpdateBakeryCommand { get; set; }
        public ICommand DeleteBakeryCommand { get; set; }
        public ICommand BreadsInBakeryCommand { get; set; }
        public ICommand SweetsCommand { get; set; }


        private Bakery selectedBakery;
        public Bakery SelectedBakery
        {
            get { return selectedBakery; }
            set
            {
                if (value != null)
                {
                    selectedBakery = new Bakery();
                    selectedBakery.BakeryId = value.BakeryId;
                    selectedBakery.Name = value.Name;
                    selectedBakery.Location = value.Location;
                    selectedBakery.Rating = value.Rating;

                    IsSelected = true;
                    OnPropertyChanged();
                }
                else
                {
                    selectedBakery = new Bakery();
                    IsSelected = false;
                }
                (DeleteBakeryCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateBakeryCommand as RelayCommand)?.NotifyCanExecuteChanged();

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

        public BakeryViewModel() { }

        public BakeryViewModel(RestCollection<Bakery> bakeries,RestCollection<Bread> breads)
        {
            SelectedBakery = new Bakery();
            if (!IsInDesignMode)
            {
                Breads = breads;
                Bakeries = bakeries;
                NonCrud = new RestService("http://localhost:39340/");
                BreadsNonCrud = new ObservableCollection<Bread>();
                SweetsNonCrud = new ObservableCollection<Bread>();

                CreateBakeryCommand = new RelayCommand(
                    () => Bakeries.Add(new Bakery()
                    {
                        Name = SelectedBakery.Name,
                        Location = SelectedBakery.Location,
                        Rating = SelectedBakery.Rating

                    }));

                DeleteBakeryCommand = new RelayCommand(
                    async () =>
                    {
                        await Breads.Delete(SelectedBakery.BakeryId);
                        await Breads.Refresh();
                        IsSelected = false;
                    },
                    () => IsSelected == true);

                UpdateBakeryCommand = new RelayCommand(
                    () => Bakeries.Update(SelectedBakery),
                    () => IsSelected == true
                    );

                BreadsInBakeryCommand = new RelayCommand(
                    () => 
                    {
                        var a = NonCrud.Get<Bread>($"/NonCrud/AllBreadsFromBakery/{SelectedBakery.BakeryId}");
                        foreach (var item in a)
                        {
                            BreadsNonCrud.Add(item);
                        }
                    }, () => { return SelectedBakery != null; }
                    );

                SweetsCommand = new RelayCommand(
                    () =>
                    {
                        var a = NonCrud.Get<Bread>($"/NonCrud/AllSweetsFromBakery/{SelectedBakery.BakeryId}");
                        foreach (var item in a)
                        {
                            SweetsNonCrud.Add(item);
                        }
                    }, () => { return SelectedBakery != null; }
                    );
            }
        }
    }
}

