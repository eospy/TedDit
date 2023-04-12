using Newtonsoft.Json;
using RedditCLient.API;
using RedditCLient.Core;
using RedditCLient.MVVM.Model;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace RedditCLient.MVVM.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        /// <summary>
        /// Button commands
        /// </summary> 
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
        public RelayCommand CloseCommentsCommand { get; set; }
        public RelayCommand SubscribeCommand { get; set; }
        public RelayCommand HomePageCommand { get; set; }
        public RelayCommand UpvoteCommand { get; set; }
        public RelayCommand DownvoteCommand { get; set; }

        /// <summary>
        /// Data collections
        /// </summary>
        public ObservableCollection<PostModel.Data1> Posts { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedhot { get; set; }
        public ObservableCollection<PostModel.Data1> Cachednew { get; set; }
        public ObservableCollection<PostModel.Data1> Cachedtop { get; set; }
        public ObservableCollection<PostModel.Data1> Curposts { get; set; }
        public ObservableCollection<SearchData.Data1> SearchResults { get; set; }
        public ObservableCollection<UserSubsData.Data1> UserSubs { get; set; }
        public ObservableCollection<Comment> CommentsList { get; set; }


        private SearchData.Data1 _selectedSubreddit;
        private UserSubsData.Data1 _selectedusersub;
        private PostModel.Data1 _selectedPost;
        private string _title;
        private string _usersubstitle;
        private string _description;
        private bool _popupIsOpen;
        private bool _commentsIsOpen;
        private bool _subscribeButtonIsOpen;
        private bool authorized = false;
        private string pagecounter;
        private int pagecount = 1;
        string category = "";
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random;
        private RestClient _client;
        LocalServer server;
        public string Icon { get; set; }
        public string NextPage { get; set; }
        public string Background { get; set; }   
        private string subreddit = "Home Page";
        private string _subscribeButtonText;


        string baseurl = "https://www.reddit.com/";
        string oauthurl = "https://oauth.reddit.com/";
        const string _defgrant = "grant_type=https://oauth.reddit.com/grants/installed_client&device_id=DO_NOT_TRACK_THIS_DEVICE";
        

        //script only parameters 
        private string _secret = "Oif_vzUl172FuMjOYS1Keod0mXHrjQ";
        private string _client_id = "m2wRkX8IpaY1u8v6oN7Baw";

        //app only auth client id 
        private string _app_client_id = "BF4HDny1XofDp2ZwrmOYbA";

        //parameters generated while app registration
        const string redirecturi = "http://127.0.0.1:8080/auth/";
        private string _useragent = "Teddit by eospy";

        private string _action;
        private bool _subscribeButtonState;
        public string token;
        private string _refreshToken = "";
        public string state;
        
        public MainWindowModel()
        {
            HotCategoryCommand = new RelayCommand(o => HotCategory());
            NewCategoryCommand = new RelayCommand(o => NewCategory());
            TopCategoryCommand = new RelayCommand(o => TopCategory());
            CloseWindowCommand = new RelayCommand(o => Application.Current.Shutdown());
            MinimizeButtonCommand = new RelayCommand(o => MinimizeWindow());
            MaximixeButtonCommand = new RelayCommand(o => MaximizeWindow());
            LoadNextPageCommand = new RelayCommand(o => LoadNextPage());
            LoadPrevPageCommand = new RelayCommand(o => LoadPrevPage());
            SearchButtonCommand = new RelayCommand(o => SearchButton());
            AuthorizeCommand = new RelayCommand(o => AuthorizeButton());
            CloseCommentsCommand = new RelayCommand(o => CloseComments());
            SubscribeCommand=new RelayCommand(o=>Subscribe());
            HomePageCommand = new RelayCommand(o => SetHomePage());
            UpvoteCommand = new RelayCommand(o => Upvote());
            DownvoteCommand = new RelayCommand(o => Downvote());
            Posts = new ObservableCollection<PostModel.Data1>();
            Curposts = new ObservableCollection<PostModel.Data1>();
            SearchResults = new ObservableCollection<SearchData.Data1>();
            UserSubs = new ObservableCollection<UserSubsData.Data1>();
            CommentsList=new ObservableCollection<Comment>();

            _selectedSubreddit = new SearchData.Data1();
            _selectedusersub = new UserSubsData.Data1();
            _selectedPost = new PostModel.Data1();
            PageNumber = "1";
            random = new Random();
            state = GetRandomString();
            _client = new RestClient();
            server = new LocalServer();
            GetToken();
            GetPostslist();
        }
        public void RefreshToken()
        {
            GetToken("grant_type=refresh_token&refresh_token=" + _refreshToken);
        }
        public void GetToken(string granttype = _defgrant)
        {
            string password = _secret;
            string username = _client_id;
            if (granttype != _defgrant)
            {
                username = _app_client_id;
                password = "";
            }
            _client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest(baseurl + "api/v1/access_token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", _useragent);
            request.AddBody(granttype);
            var response = _client.Post(request);
            var data = response.Content;
            var root = JsonConvert.DeserializeObject<Token.Rootobject>(data);
            token = root.access_token;
            _refreshToken = root.refresh_token;
        }
        bool CanVote() => authorized && _selectedPost.name != null;
        void Upvote()
        {
            if(CanVote())
                Vote(1);
        }
        void Downvote()
        {
            if (CanVote())
                Vote(-1);
        }
        public void Vote(int vote)
        {
            string uri = oauthurl + "api/vote/";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", _useragent);
            request.AddParameter("dir", vote);
            request.AddParameter("id", _selectedPost.name);
            _client.Post(request);
        }
        void SetHomePage()
        {
            Subreddit = "Home Page";
            GetHomePage();
        }
        public void GetHomePage(string next = "", string prev = "")
        {
            Posts.Clear();
            Curposts.Clear();
            Title = "";
            Description = "";
            SubscribeButtonText = "";
            string uri = oauthurl + "/" + category;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", _useragent);
            request.AddParameter("after", next);
            request.AddParameter("before", prev);
            RestResponse response = new();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception) { MessageBox.Show("Subreddit loading problem"); }
            var root = JsonConvert.DeserializeObject<HomePageData.Rootobject>(response.Content);
            NextPage = root.data.after;
            root.data.children.Select(r => r.data).ToList().ForEach(r => Curposts.Add(new PostModel.Data1(r.subreddit_name_prefixed, r.id,r.subreddit,r.title, r.url_overridden_by_dest, r.selftext, r.score,r.num_comments)));
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
        public void GetPostslist(string next = "", string prev = "")
        {
            if (Subreddit == "Home Page")
            {
                GetHomePage(next,prev);
                return;
            }
            Posts.Clear();
            Curposts.Clear();
            string uri = oauthurl + "r/" + Subreddit + "/" + category;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", _useragent);
            request.AddParameter("after", next);
            request.AddParameter("before", prev);
            RestResponse response = new();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();

            }
            catch (Exception)
            {
                MessageBox.Show("Subreddit loading problem");
            }
            var root = JsonConvert.DeserializeObject<PostModel.Rootobject>(response.Content).data;
            NextPage = root.after;
            root.children.Select(r => r.data).ToList().ForEach(r => Curposts.Add(r));

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
        public void GetUserSubs()
        {
            string uri = oauthurl + "/subreddits/mine/subscriber";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", _useragent);
            string response = "";
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult().Content;
            }
            catch (Exception) { }
            var root = JsonConvert.DeserializeObject<UserSubsData.Rootobject>(response).data.children;
            root.Select(r => r.data).ToList().ForEach(r => UserSubs.Add(r));
        }
        public void Subscribe()
        {
            string uri = oauthurl + "api/subscribe/";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", _useragent);
            request.AddParameter("action", _action);
            request.AddParameter("sr_name",Subreddit);
            var response = _client.Post(request);
            SubscribeButtonState(!_subscribeButtonState);
        }
        public void GetSubredditInfo()
        {
            string uri = oauthurl + "r/" + Subreddit + "/" + "about";
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", _useragent);
            RestResponse response = new();
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
            request.AddHeader("user-agent", _useragent);
            request.AddParameter("q", search);
            RestResponse response = new();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();

            }
            catch (Exception) { MessageBox.Show("Subreddit search problem"); }
            var root = JsonConvert.DeserializeObject<SearchData.Rootobject>(response.Content).data.children;
            root.Select(r => r.data).ToList().ForEach(r => SearchResults.Add(r));
        }
        public List<Comment> CommentsRecursiveAdd(string body,string repliesstring)
        {
            List<Comment> replieslist = new List<Comment>();
            if (repliesstring != "")
            {
                var list = JsonConvert.DeserializeObject<RepliesData.Rootobject>(repliesstring).data.children;
                replieslist = new List<Comment>();
                foreach (var r in list)
                {
                    Comment c = new Comment(r.data.body);
                    if (r.data.replies != null)
                    {
                        c.RepliesList = CommentsRecursiveAdd(r.data.body,r.data.replies.ToString());
                    }
                    replieslist.Add(c);

                }
            }
            return replieslist;
           
        }
        public void GetComments(string postLink)
        {
            string uri = oauthurl +"/comments/" + postLink;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("user-agent", _useragent);
            var response = _client.Get(request).Content;
            var root = Deserialize<CommentsData.Class1>(response)[1].data.children;
            foreach (var c in root)
            {
                Comment comment = new(c.data.body);
                if (c.data.replies != null)
                {
                    string repliesstring = c.data.replies.ToString();
                    comment.RepliesList=CommentsRecursiveAdd(c.data.body,repliesstring);
                }
                CommentsList.Add(comment);

            }

        }
        public static List<T> Deserialize<T>(string SerializedJSONString)
        {
            return JsonConvert.DeserializeObject<List<T>>(SerializedJSONString);
        }
        public string Subreddit
        {
            get=> subreddit;
            set=> subreddit = value;
        }
        public PostModel.Data1 SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                _selectedPost = value; 
                if (_selectedPost != null && _selectedPost.num_comments > 0)
                    OpenComments();
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
                    Subreddit = _selectedusersub.display_name;
                    GetSubredditInfo();
                    GetPostslist();
                    SubscribeButtonState(true);
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
                    Subreddit = _selectedSubreddit.display_name; ;
                    PopupIsOpen = false;
                    GetSubredditInfo();
                    GetPostslist();
                    SubscribeButtonState(UserSubs.Any(s => s.display_name == Subreddit));
                }

            }
        }
        void OpenComments()
        {
            GetComments(SelectedPost.id);
            CommentsIsOpen = true;
        }
        void CloseComments()
        {
            CommentsIsOpen = false;
            CommentsList.Clear();
        }
        void SearchButton()
        {
            SearchResults.Clear();
            SearchSubreddits(subreddit);
            PopupIsOpen = true;
        }
        
        public bool CommentsIsOpen
        {
            get => _commentsIsOpen;
            set => SetProperty(ref _commentsIsOpen, value);
        }

        public bool PopupIsOpen
        {
            get => _popupIsOpen;
            set => SetProperty(ref _popupIsOpen, value);
        }
        public bool SubscribeButtonIsOpen
        {
            get => _subscribeButtonIsOpen;
            set=>SetProperty(ref _subscribeButtonIsOpen,value);
        }
        public string SubscribeButtonText
        {
            get { return _subscribeButtonText; }
            set { SetProperty(ref _subscribeButtonText, value); }
        }
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
            protected set => SetProperty<string>(ref pagecounter, value);
            get { return pagecounter; }
        }
        void AuthorizeButton()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"https://www.reddit.com/api/v1/authorize?client_id="+_app_client_id+"&response_type=code&state="
                + state + "&redirect_uri=" +
                redirecturi + "&duration=permanent&" +
                "scope=mysubreddits submit save read vote identity subscribe",
                UseShellExecute = true
            });
            string code = server.Start(state);
            if(code != "")
            {
                string body = "grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirecturi;
                GetToken(body);
                GetUserSubs();
                UserSubsTitle = "Your communities";
                SubscribeButtonIsOpen = true;
                SubscribeButtonState(UserSubs.Any(s => s.display_name == Subreddit));
                authorized = true;
            }
            
        }
        void SubscribeButtonState(bool state)
        {
            if(!SubscribeButtonIsOpen) return; 
            _subscribeButtonState=state;
            if (state)
            {
                _action = "unsub";
                SubscribeButtonText = "Unsubscribe";
            }
            else
            {
                _action = "sub";
                SubscribeButtonText = "Subscribe";
            }
            
        }
        private static string GetRandomString()
        {
            return new string(Enumerable.Repeat(chars, 21).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        void LoadNextPage()
        {
            pagecount++;
            PageNumber = pagecount.ToString();
            GetPostslist(next: NextPage);

        }
        void LoadPrevPage()
        {
            pagecount--;
            if (pagecount < 2) pagecount = 1;
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
       
    }
    public class Comment : ViewModelBase
    {
        private string _commentbody;
        private List<Comment> _replieslist;
        public string CommentBody
        {
            get { return _commentbody; }
            set { SetProperty(ref _commentbody, value); }
        }
        public List<Comment> RepliesList
        {
            get { return _replieslist; }
            set { SetProperty(ref _replieslist, value); }
        }
        public Comment(string body, List<Comment> replies = null)
        {
            _commentbody = body;
            _replieslist = replies;
        }
    }
}
