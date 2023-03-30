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
using Newtonsoft.Json.Linq;
using RedditCLient.MVVM.Model;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace RedditCLient.MVVM.ViewModel
{
    public class HotViewModel:ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string subreddit = "cats";
        private string _selectedSubreddit = "";
        public string token;
        private RestClient _client;
        string baseurl = "https://www.reddit.com/";
        string oauthurl = "https://oauth.reddit.com/";
        public ObservableCollection<SearchData.Data1> SearchResults { get; set; }
        public HotViewModel() 
        {
            SearchResults = new ObservableCollection<SearchData.Data1>();
            _client = new RestClient();
            GetToken();
            SearchSubreddits("cats");
        }
        public string Subreddit
        {
            get { return subreddit; }
            set
            {
                subreddit = value;
                SearchResults.Clear();
                SearchSubreddits(subreddit);
                PopupIsOpen=true;
            }
        }
        public string SelectedSubreddit
        {
            get { return _selectedSubreddit; }
            set
            {
                subreddit= value;
                PopupIsOpen = false;
            }
        }
        public void GetToken()
        {
            _client.Authenticator = new HttpBasicAuthenticator("m2wRkX8IpaY1u8v6oN7Baw", "Oif_vzUl172FuMjOYS1Keod0mXHrjQ");
            var request = new RestRequest(baseurl + "api/v1/access_token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Teddit by eospy");
            string body = "grant_type=https://oauth.reddit.com/grants/installed_client&device_id=DO_NOT_TRACK_THIS_DEVICE";
            request.AddBody(body);
            var response = _client.PostAsync(request);
            var data = response.Result.Content;
            var root = JsonConvert.DeserializeObject<Token.Rootobject>(data);
            token = root.access_token;

        }
        public void SearchSubreddits(string search)
        {
            string uri = oauthurl + "subreddits/search";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddParameter("q", search);
            var response = _client.GetAsync(request).GetAwaiter().GetResult();
            var root = JsonConvert.DeserializeObject<SearchData.Rootobject>(response.Content).data.children;
            foreach (var subreddit in root)
            {
                SearchResults.Add(subreddit.data);
            }
        }
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool popupIsOpen;

        public bool PopupIsOpen { get => popupIsOpen; set => SetProperty(ref popupIsOpen, value); }

    }
}
