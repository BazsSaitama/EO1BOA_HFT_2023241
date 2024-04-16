using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_GUI_2023242_WPF_Client.Services
{
    interface IOvenService
    {
        public void Open(RestCollection<Bakery> Bakery, RestCollection<Oven> Oven);
    }
}
