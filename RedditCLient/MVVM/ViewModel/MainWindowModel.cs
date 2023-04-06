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
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

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
        public RelayCommand SearchButtonCommand { get; set; }
        public RelayCommand SubredditSelectCommand { get; set; }
        public RelayCommand AuthorizeCommand { get; set; }

        private string pagecounter;
        private int pagecount = 1;
        string category = "";
        public ObservableCollection<PostModel.Data1> Posts { get; set; }
        public ObservableCollection<SearchData.Data1> SearchResults { get; set; }
        public ObservableCollection<UserSubsData.Data1> UserSubs { get; set; }
        private SearchData.Data1 _selectedSubreddit;
        private UserSubsData.Data1 _selectedusersub;
        private string _title;
        private string _usersubstitle;
        private string _description;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public string Icon { get; set; }
        public string Background { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedhot { get; set; }
        public ObservableCollection<PostModel.Data1> Cachednew { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedtop { get; set; }
        public ObservableCollection<PostModel.Data1> Curposts { get; set; }
        private RestClient _client;
        LocalServer server;
        string baseurl = "https://www.reddit.com/";
        string oauthurl = "https://oauth.reddit.com/";
        private string subreddit = "cats";
        private string secret = "Oif_vzUl172FuMjOYS1Keod0mXHrjQ";
        const string _defgrant= "grant_type=https://oauth.reddit.com/grants/installed_client&device_id=DO_NOT_TRACK_THIS_DEVICE";
        const string redirecturi = "http://127.0.0.1:8080/auth/";
        public string token;
        private string _refreshToken = "";
        public string state;
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
            LoadNextPageCommand = new RelayCommand(o => LoadNextPage());
            LoadPrevPageCommand = new RelayCommand(o => LoadPrevPage());
            Posts = new ObservableCollection<PostModel.Data1>();
            Curposts = new ObservableCollection<PostModel.Data1>();
            SearchResults = new ObservableCollection<SearchData.Data1>();
            UserSubs=new ObservableCollection<UserSubsData.Data1>();
            SearchButtonCommand = new RelayCommand(o => SearchButton());
            AuthorizeCommand = new RelayCommand(o => AuthorizeButton());
            _selectedSubreddit = new SearchData.Data1();
            _selectedusersub=new UserSubsData.Data1();
            PageNumber = "1";
            state=GetRandomString();
            _client = new RestClient();
            server = new LocalServer();
            GetToken();
            GetSubredditInfo();
            GetPostslist();
        }
        public void RefreshToken()
        {
            GetToken("grant_type=refresh_token&refresh_token="+_refreshToken);
        }
        public void GetToken(string granttype=_defgrant)
        {
            string password = secret;
            string username = "m2wRkX8IpaY1u8v6oN7Baw";
            if (granttype != _defgrant)
            {
                username= "BF4HDny1XofDp2ZwrmOYbA";
                password = "";
            }
            _client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest(baseurl + "api/v1/access_token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddBody(granttype);
            var response = _client.Post(request);
            var data = response.Content;
            var root = JsonConvert.DeserializeObject<Token.Rootobject>(data);
            token = root.access_token;
            _refreshToken = root.refresh_token;
        }
        public void GetUserSubs()
        {
            string uri = oauthurl + "/subreddits/mine/subscriber";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            string response = "";
            try
            {
                 response = _client.GetAsync(request).GetAwaiter().GetResult().Content;
            }
            catch(Exception) { }
            var data = JsonConvert.DeserializeObject<UserSubsData.Rootobject>(response).data.children;
            foreach (var sub in data)
            {
                UserSubs.Add(sub.data);
            }
        }
        public void GetPostslist(string next = "", string prev = "")
        {
            Posts.Clear();
            Curposts.Clear();
            string uri = oauthurl + "r/" + Subreddit + "/" + category;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddParameter("after", next);
            request.AddParameter("before", prev);
            RestResponse response = new RestResponse();
            try
            {
                 response = _client.GetAsync(request).GetAwaiter().GetResult();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Subreddit loading problem");
            }
            var root = JsonConvert.DeserializeObject<PostModel.Rootobject>(response.Content);
            NextPage = root.data.after;
            foreach (var post in root.data.children)
            {
                Curposts.Add(post.data);
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
        public void GetSubredditInfo()
        {
            string uri = oauthurl + "r/" + Subreddit + "/" + "about";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            RestResponse response = new RestResponse();
            try
            {
                 response = _client.GetAsync(request).GetAwaiter().GetResult();
                
            }
            catch (Exception) { }
            var data = JsonConvert.DeserializeObject<SubredditData.Rootobject>(response.Content).data;
            Title = data.title;
            Description = data.public_description;
        }
        public void SearchSubreddits(string search)
        {
            string uri = oauthurl + "subreddits/search";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddParameter("q", search);
            RestResponse response = new RestResponse();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();
                
            }
            catch(Exception) { MessageBox.Show("Subreddit search problem"); }
            var root = JsonConvert.DeserializeObject<SearchData.Rootobject>(response.Content).data.children;
            foreach (var subreddit in root)
            {
                SearchResults.Add(subreddit.data);
            }
        }
        public void GetComments(string postLink)
        {
            string uri = oauthurl + "/r/"+Subreddit+"/comments/" + postLink;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            var response = _client.Get(request).Content;
            var data = Deserialize<CommentsData.Class1>(response);
        }
        public static List<T> Deserialize<T>(string SerializedJSONString)
        {
            var list = JsonConvert.DeserializeObject<List<T>>(SerializedJSONString);
            return list;
        }
        public string Subreddit
        {
            get
            {
                return subreddit;
            }
            set
            {
                subreddit = value;
            }
        }
        public UserSubsData.Data1 SelectedUserSub
        {
            get { return _selectedusersub; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedusersub, value);
                    subreddit= _selectedusersub.display_name;
                    Subreddit = subreddit;
                    GetSubredditInfo();
                    GetPostslist();
                }

            }
        }
        public SearchData.Data1 SelectedSubreddit
        {
            get { return _selectedSubreddit; }
            set 
            {
                if (value != null) 
                {
                    SetProperty(ref _selectedSubreddit, value);
                    subreddit= _selectedSubreddit.display_name;
                    Subreddit = subreddit;
                    PopupIsOpen = false;
                    GetSubredditInfo();
                    GetPostslist();
                }
                
            } 
        }
        void SearchButton()
        {
            SearchResults.Clear();
            SearchSubreddits(subreddit);
            PopupIsOpen = true;
        }
        private bool popupIsOpen;

        public bool PopupIsOpen { get => popupIsOpen; set => SetProperty(ref popupIsOpen, value); }

        public string Title
        {
            get { return _title; }
            set { SetProperty<string>(ref _title, value); } 
        }
        public string UserSubsTitle
        {
            get { return _usersubstitle; }
            set { SetProperty<string>(ref _usersubstitle, value); }
        }
        public string Description 
        {
            get { return _description; }
            set { SetProperty<string>(ref _description, value); }
        }
        public string PageNumber
        {
            protected set { SetProperty<string>(ref pagecounter, value); }
            get { return pagecounter; }
        }
        void AuthorizeButton()
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://www.reddit.com/api/v1/authorize?client_id=BF4HDny1XofDp2ZwrmOYbA&response_type=code&state=" 
                + state + "&redirect_uri=" + 
                redirecturi + "&duration=permanent&" +
                "scope=mysubreddits submit save read vote identity",
                UseShellExecute = true });
            string code =server.Start(state);
            string body = "grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirecturi;
            GetToken(body);
            GetUserSubs();
            UserSubsTitle = "Your communities";
        }
        private static string GetRandomString()
        {
            return new string(Enumerable.Repeat(chars, 21).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        void LoadNextPage()
        {
            pagecount++;
            PageNumber = pagecount.ToString();
            GetPostslist(next:NextPage);
            
        } 
        void LoadPrevPage()
        {
            pagecount--;
            PageNumber = pagecount.ToString();
            GetPostslist(prev: NextPage);
        }
        void HotCategory()
        {
            category = "hot";
            pagecount = 1;
            PageNumber = pagecount.ToString();
            if (Cachedhot == null || Cachedhot.Count == 0)
            {
                GetPostslist();
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
                GetPostslist();
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
                GetPostslist();
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
