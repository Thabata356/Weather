using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Models;
using Weather.Services;

namespace Weather.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        [ObservableProperty]
        City city;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string icon;

        [ObservableProperty]
        private string imageUrl;

        [ObservableProperty]
        ImageSource weatherImage;

        [ObservableProperty]
        private string humidity;

        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private string windSpeed;

        [ObservableProperty]
        string cityName;

        [ObservableProperty]
        private string feelsLike;

        [ObservableProperty]
        private string backColor;

        [ObservableProperty]
        WeatherService weatherService;

        public ICommand getCityCommand { get; }

        public WeatherViewModel()
        {
            getCityCommand = new Command(getCity);
            weatherService = new WeatherService();
        }

        public async void getCity()
        {
            City = await WeatherService.GetCityAsync(CityName);
            Name = City.Name + ", " + City.Sys.Country;
            Description = City.Weather[0].Description;
            Humidity = "Humidade: " + City.Main.Humidity.ToString() + "%";
            Icon = City.Weather[0].Icon;
            string ImageUrl = $"https://openweathermap.org/img/wn/{Icon}@2x.png";
            WeatherImage = ImageSource.FromUri(new Uri(ImageUrl));
            Temperature = Math.Round(City.Main.Temp).ToString() + "ºC";
            WindSpeed = "Vento: " + City.Wind.Speed.ToString() + "km/h";
            FeelsLike = "Sensação Térmica: " + Math.Round(City.Main.Feels_like).ToString() + "ºC";

            if (City.Weather[0].Icon.Contains("d"))
            {
                BackColor = "#41A5F5";
            }
            else
            {
                BackColor = "#004f92";
            }
        }
    }
}

