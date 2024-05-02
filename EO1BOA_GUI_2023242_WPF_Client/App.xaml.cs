using CommunityToolkit.Mvvm.DependencyInjection;
using EO1BOA_GUI_2023242_WPF_Client.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EO1BOA_GUI_2023242_WPF_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IBreadService, BreadViaWindow>()
                    .AddSingleton<IOvenService, OvensViaWindow>()
                    .AddSingleton<IBakeryService, BakeryViaWindow>()
                    .BuildServiceProvider()
                    );
        }
    }
}
