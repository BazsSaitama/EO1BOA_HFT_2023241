using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using EO1BOA_GUI_2023242_WPF_Client.Services;
using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EO1BOA_GUI_2023242_WPF_Client
{
    class MainWindowViewModel : ObservableRecipient
    {
        IBakeryService bakeryService;
        IOvenService ovenService;
        IBreadService breadService;

        public RestCollection<Bakery> bakery { get; set; }
        public RestCollection<Oven> ovens { get; set; }
        public RestCollection<Bread> breads { get; set; }

        public ICommand GetBakeryCommand { get; set; }
        public ICommand GetOvensCommand { get; set; }
        public ICommand GetBreadCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel() 
        {
            if (!IsInDesignMode)
            {
                bakery = new RestCollection<Bakery>("http://localhost:7332/", "Bakery", "hub");
                ovens = new RestCollection<Oven>("http://localhost:7332/", "Oven", "hub");
                breads = new RestCollection<Bread>("http://localhost:7332/", "Bread", "hub");


                bakeryService = Ioc.Default.GetRequiredService<IBakeryService>();
                ovenService = Ioc.Default.GetRequiredService<IOvenService>();
                breadService = Ioc.Default.GetRequiredService<IBreadService>();

                GetBakeryCommand = new RelayCommand(
                    () => bakeryService.Open(bakery,breads,ovens),
                    () => true
                    );
                GetOvensCommand = new RelayCommand(
                    () => ovenService.Open(bakery,ovens),
                    () => true
                    );
                GetBreadCommand = new RelayCommand(
                    () => breadService.Open(breads,ovens),
                    () => true
                    );
            }
        }
    }
}
