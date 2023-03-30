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
        public RelayCommand SearchButtonCommand { get; set; }
        public RelayCommand SubredditSelectCommand { get; set; }

        private string pagecounter;
        private int pagecount = 1;
        string category = "";
        public ObservableCollection<PostModel.Data1> Posts { get; set; }
        public ObservableCollection<SearchData.Data1> SearchResults { get; set; }
        private SearchData.Data1 _selectedSubreddit;
        private string title;
        private string description;

        public string Icon { get; set; }
        public string Background { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedhot { get; set; }
        public ObservableCollection<PostModel.Data1> Cachednew { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedtop { get; set; }
        public ObservableCollection<PostModel.Data1> Curposts { get; set; }
        private RestClient _client;
        string baseurl = "https://www.reddit.com/";
        string oauthurl = "https://oauth.reddit.com/";
        private string subreddit = "cats";
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
            LoadNextPageCommand = new RelayCommand(o => LoadNextPage());
            LoadPrevPageCommand = new RelayCommand(o => LoadPrevPage());
            Posts = new ObservableCollection<PostModel.Data1>();
            Curposts = new ObservableCollection<PostModel.Data1>();
            SearchResults = new ObservableCollection<SearchData.Data1>();
            SearchButtonCommand = new RelayCommand(o => SearchButton());
            _selectedSubreddit = new SearchData.Data1();
            PageNumber = "1";
            _client = new RestClient();
            GetToken();
            GetSubredditInfo();
            GetPostslist();
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
            try
            {
                var response = _client.GetAsync(request).GetAwaiter().GetResult();
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
            catch (Exception ex)
            {
                MessageBox.Show("Subreddit loading problem");
            }
            
        }
        public void GetSubredditInfo()
        {
            string uri = oauthurl + "r/" + Subreddit + "/" + "about";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            var response = _client.GetAsync(request).GetAwaiter().GetResult();
            try
            {
                var data = JsonConvert.DeserializeObject<SubredditData.Rootobject>(response.Content).data;
                Title = data.title;
                Description = data.public_description;
            }
            catch (Exception) { }

        }
        public void SearchSubreddits(string search)
        {
            string uri = oauthurl + "subreddits/search";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", "Teddit by eospy");
            request.AddParameter("q", search);
            var response = _client.GetAsync(request).GetAwaiter().GetResult();
            try
            {
                var root = JsonConvert.DeserializeObject<SearchData.Rootobject>(response.Content).data.children;
                foreach (var subreddit in root)
                {
                    SearchResults.Add(subreddit.data);
                }
            }
            catch(Exception) { MessageBox.Show("Subreddit search problem"); }
           
        }
        public string Subreddit
        {
            get
            {
                if (subreddit != "cats")
                {
                    return _selectedSubreddit.display_name;
                }
                else return subreddit;
            }
            set
            {
                subreddit = value;
                
            }
        }

        public SearchData.Data1 SelectedSubreddit
        {
            get { return _selectedSubreddit; }
            set 
            {
                if (value != null) 
                {
                    SetProperty<SearchData.Data1>(ref _selectedSubreddit, value);
                    Subreddit = _selectedSubreddit.display_name;
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
