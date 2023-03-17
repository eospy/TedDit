using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RedditCLient.Core;
using RedditCLient.API;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Authenticators;
using RestSharp;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics.Metrics;
using RedditCLient.MVVM.Model;

namespace RedditCLient.MVVM.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand HotCategoryCommand { get; set; }
        public RelayCommand NewCategoryCommand { get; set; }
        public RelayCommand TopCategoryCommand { get; set; }
        public RelayCommand MinimizeButtonCommand { get; set; }
        public RelayCommand MaximixeButtonCommand { get; set; }
        public RelayCommand LoadNextPageCommand { get; set; }
        public RelayCommand LoadPrevPageCommand { get; set; }
        
        private string pagecounter;
        private int pagecount = 1;
        string category = "";
        public ObservableCollection<PostModel.Data1> Posts { get; set; }
        private string title;
        private string description;
        
        public string Icon { get; set; }
        public string Background { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedhot { get; set; }
        public ObservableCollection<PostModel.Data1> Cachednew { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedtop { get; set; }
        public ObservableCollection<PostModel.Data1> Curposts { get; set; }
        RestClient client;
        string baseurl = "https://www.reddit.com/";
        string oauthurl = "https://oauth.reddit.com/";
        private string subreddit="cats";
        public string token;
        public string NextPage { get; set; }
        public MainWindowModel()
        {
            HotCategoryCommand = new RelayCommand(o => HotCategory());
            NewCategoryCommand = new RelayCommand(o => NewCategory());
            TopCategoryCommand = new RelayCommand(o => TopCategory());
            CloseWindowCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
            MinimizeButtonCommand = new RelayCommand(o => MinimizeWindow());
            MaximixeButtonCommand = new RelayCommand(o => MaximizeWindow());
            LoadNextPageCommand=new RelayCommand(o=> LoadNextPage());
            LoadPrevPageCommand = new RelayCommand(o => LoadPrevPage());
            Posts = new ObservableCollection<PostModel.Data1>();
            Curposts = new ObservableCollection<PostModel.Data1>();
            PageNumber = "1";
            client = new RestClient();
            GetToken();
            GetSubredditInfo(token, subreddit);
            GetPostslist(token, Subreddit, category);
        }
        public void GetToken()
        {
            client.Authenticator = new HttpBasicAuthenticator("m2wRkX8IpaY1u8v6oN7Baw", "Oif_vzUl172FuMjOYS1Keod0mXHrjQ");
            var request = new RestRequest(baseurl + "api/v1/access_token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Teddit by eospy");
            string body = "grant_type=https://oauth.reddit.com/grants/installed_client&device_id=DO_NOT_TRACK_THIS_DEVICE";
            request.AddBody(body);
            var response = client.PostAsync(request);
            var data = response.Result.Content;
            var root = JsonConvert.DeserializeObject<Token.Rootobject>(data);
            token = root.access_token;

        }
        public void GetPostslist(string token, string subreddit, string category,string next="",string prev="")
        {
            Posts.Clear();
            Curposts.Clear();
            string uri = oauthurl + "r/" + subreddit + "/" + category;
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddParameter("after",next);
            request.AddParameter("before", prev);
            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var root = JsonConvert.DeserializeObject<PostModel.Rootobject>(response.Content);
            NextPage = root.data.after;
            for (int i = 0; i < root.data.children.Count(); i++)
            {
                var post = root.data.children[i].data;
                Curposts.Add(post);
            }
            switch (category)
            {
                case "new":
                    Cachednew = Curposts; break;
                case "hot":
                    Cachedhot = Curposts; break;
                case "top":
                    Cachedtop = Curposts; break;

            }
            Posts = Curposts;
        }
        public void GetSubredditInfo(string token, string subreddit)
        {
            string uri = oauthurl + "r/" + subreddit + "/" + "about";
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<SubredditData.Rootobject>(response.Content).data;
            Title = data.title;
            Description = data.public_description;
            //Icon = data.community_icon;
            //Background = data.banner_background_image;
        }
        public string Subreddit
        {
            get { return subreddit; }
            set { subreddit = value; GetSubredditInfo(token, subreddit); }
        }
        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); } 
        }
        
        public string Description 
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }
        public string PageNumber
        {
            protected set { SetProperty<string>(ref pagecounter, value); }
            get { return pagecounter; }
        }
        void LoadNextPage()
        {
            pagecount++;
            PageNumber = pagecount.ToString();
            GetPostslist(token, Subreddit, category,next:NextPage);
            
        } 
        void LoadPrevPage()
        {
            pagecount--;
            PageNumber = pagecount.ToString();
            GetPostslist(token, Subreddit, category, prev: NextPage);
        }
        void HotCategory()
        {
            category = "hot";
            pagecount = 1;
            PageNumber = pagecount.ToString();
            if (Cachedhot == null || Cachedhot.Count == 0)
            {
                GetPostslist(token, Subreddit, category);
            }
            else Posts.Clear(); Posts = Cachedhot;
        }
        void NewCategory()
        {
            category = "new";
            pagecount = 1;
            PageNumber = pagecount.ToString();
            if (Cachednew == null || Cachednew.Count == 0)
            {
                GetPostslist(token, Subreddit, category);
            }
            else Posts.Clear(); Posts = Cachednew;
        }
        void TopCategory()
        {
            category = "top";
            pagecount = 1;
            PageNumber = pagecount.ToString();
            if (Cachedtop == null || Cachedtop.Count == 0)
            {
                GetPostslist(token, Subreddit, category);
            }
            else Posts.Clear(); Posts = Cachedtop;
        }
        static void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        static void MaximizeWindow()
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
