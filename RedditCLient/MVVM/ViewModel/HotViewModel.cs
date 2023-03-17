using Newtonsoft.Json;
using RedditCLient.API;
using RedditCLient.Core;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCLient.MVVM.ViewModel
{
    public class HotViewModel:ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public HotViewModel() 
        {
        }
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
