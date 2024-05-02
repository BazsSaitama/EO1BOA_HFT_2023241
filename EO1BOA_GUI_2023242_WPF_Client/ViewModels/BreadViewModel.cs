using CommunityToolkit.Mvvm.ComponentModel;
using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_GUI_2023242_WPF_Client.ViewModels
{
    class BreadViewModel : ObservableRecipient
    {
        public bool IsSelected { get; set; } = false;
        public RestCollection<Bread> Breads { get; set; }

        public BreadViewModel(RestCollection<Bread> breads,RestCollection<Oven> ovens,RestCollection<Bakery> bakeries) 
        {
            ;
        }
    }
}
