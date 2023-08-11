namespace RedditCLient.MVVM.Model
{
    public class HomePageData
    {
        public string kind { get; set; }
        public Data data { get; set; }
        
        public class Data
        {
            public string after { get; set; }
            public int dist { get; set; }
            public object modhash { get; set; }
            public object geo_filter { get; set; }
            public Child[] children { get; set; }
            public object before { get; set; }
        }

        public class Child
        {
            public string kind { get; set; }
            public Data1 data { get; set; }
        }

        public class Data1
        {
            public object approved_at_utc { get; set; }
            public string subreddit { get; set; }
            public string selftext { get; set; }
            public string author_fullname { get; set; }
            public bool saved { get; set; }
            public object mod_reason_title { get; set; }
            public int gilded { get; set; }
            public bool clicked { get; set; }
            public string title { get; set; }
            public Link_Flair_Richtext[] link_flair_richtext { get; set; }
            public string subreddit_name_prefixed { get; set; }
            public bool hidden { get; set; }
            public int? pwls { get; set; }
            public string link_flair_css_class { get; set; }
            public int downs { get; set; }
            public int? thumbnail_height { get; set; }
            public object top_awarded_type { get; set; }
            public bool hide_score { get; set; }
            public string name { get; set; }
            public bool quarantine { get; set; }
            public string link_flair_text_color { get; set; }
            public float upvote_ratio { get; set; }
            public string author_flair_background_color { get; set; }
            public int ups { get; set; }
            public int total_awards_received { get; set; }
            public Media_Embed media_embed { get; set; }
            public int? thumbnail_width { get; set; }
            public string author_flair_template_id { get; set; }
            public bool is_original_content { get; set; }
            public object[] user_reports { get; set; }
            public object secure_media { get; set; }
            public bool is_reddit_media_domain { get; set; }
            public bool is_meta { get; set; }
            public object category { get; set; }
            public Secure_Media_Embed secure_media_embed { get; set; }
            public string link_flair_text { get; set; }
            public bool can_mod_post { get; set; }
            public int score { get; set; }
            public object approved_by { get; set; }
            public bool is_created_from_ads_ui { get; set; }
            public bool author_premium { get; set; }
            public string thumbnail { get; set; }
            public object edited { get; set; }
            public string author_flair_css_class { get; set; }
            public Author_Flair_Richtext[] author_flair_richtext { get; set; }
            public Gildings gildings { get; set; }
            public string post_hint { get; set; }
            public object content_categories { get; set; }
            public bool is_self { get; set; }
            public string subreddit_type { get; set; }
            public float created { get; set; }
            public string link_flair_type { get; set; }
            public int? wls { get; set; }
            public object removed_by_category { get; set; }
            public object banned_by { get; set; }
            public string author_flair_type { get; set; }
            public string domain { get; set; }
            public bool allow_live_comments { get; set; }
            public string selftext_html { get; set; }
            public object likes { get; set; }
            public string suggested_sort { get; set; }
            public object banned_at_utc { get; set; }
            public string url_overridden_by_dest { get; set; }
            public object view_count { get; set; }
            public bool archived { get; set; }
            public bool no_follow { get; set; }
            public bool is_crosspostable { get; set; }
            public bool pinned { get; set; }
            public bool over_18 { get; set; }
            public Preview preview { get; set; }
            public All_Awardings[] all_awardings { get; set; }
            public object[] awarders { get; set; }
            public bool media_only { get; set; }
            public string link_flair_template_id { get; set; }
            public bool can_gild { get; set; }
            public bool spoiler { get; set; }
            public bool locked { get; set; }
            public string author_flair_text { get; set; }
            public object[] treatment_tags { get; set; }
            public bool visited { get; set; }
            public object removed_by { get; set; }
            public object mod_note { get; set; }
            public string distinguished { get; set; }
            public string subreddit_id { get; set; }
            public bool author_is_blocked { get; set; }
            public object mod_reason_by { get; set; }
            public object num_reports { get; set; }
            public object removal_reason { get; set; }
            public string link_flair_background_color { get; set; }
            public string id { get; set; }
            public bool is_robot_indexable { get; set; }
            public object report_reasons { get; set; }
            public string author { get; set; }
            public object discussion_type { get; set; }
            public int num_comments { get; set; }
            public bool send_replies { get; set; }
            public string whitelist_status { get; set; }
            public bool contest_mode { get; set; }
            public object[] mod_reports { get; set; }
            public bool author_patreon_flair { get; set; }
            public string author_flair_text_color { get; set; }
            public string permalink { get; set; }
            public string parent_whitelist_status { get; set; }
            public bool stickied { get; set; }
            public string url { get; set; }
            public int subreddit_subscribers { get; set; }
            public float created_utc { get; set; }
            public int num_crossposts { get; set; }
            public object media { get; set; }
            public bool is_video { get; set; }
            public Tournament_Data tournament_data { get; set; }
            public Crosspost_Parent_List[] crosspost_parent_list { get; set; }
            public string crosspost_parent { get; set; }
        }

        public class Media_Embed
        {
        }

        public class Secure_Media_Embed
        {
        }

        public class Gildings
        {
        }

        public class Preview
        {
            public Image[] images { get; set; }
            public bool enabled { get; set; }
        }

        public class Image
        {
            public Source source { get; set; }
            public Resolution2[] resolutions { get; set; }
            public Variants variants { get; set; }
            public string id { get; set; }
        }

        public class Source
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Variants
        {
            public Gif gif { get; set; }
            public Mp4 mp4 { get; set; }
        }

        public class Gif
        {
            public Source1 source { get; set; }
            public Resolution[] resolutions { get; set; }
        }

        public class Source1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Mp4
        {
            public Source2 source { get; set; }
            public Resolution1[] resolutions { get; set; }
        }

        public class Source2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Tournament_Data
        {
            public string status { get; set; }
            public int total_participants { get; set; }
            public string subreddit_id { get; set; }
            public string name { get; set; }
            public Prediction[] predictions { get; set; }
            public string currency { get; set; }
            public string theme_id { get; set; }
            public string tournament_id { get; set; }
        }

        public class Prediction
        {
            public string status { get; set; }
            public string body { get; set; }
            public bool is_rtjson { get; set; }
            public bool is_nsfw { get; set; }
            public string title { get; set; }
            public long created_at { get; set; }
            public long voting_end_timestamp { get; set; }
            public string id { get; set; }
            public object vote_updates_remained { get; set; }
            public object resolved_option_id { get; set; }
            public object user_won_amount { get; set; }
            public bool is_spoiler { get; set; }
            public object user_selection { get; set; }
            public Option[] options { get; set; }
            public int total_stake_amount { get; set; }
            public int total_vote_count { get; set; }
        }

        public class Option
        {
            public int total_amount { get; set; }
            public string text { get; set; }
            public int vote_count { get; set; }
            public object user_amount { get; set; }
            public object image_url { get; set; }
            public string id { get; set; }
        }

        public class Link_Flair_Richtext
        {
            public string a { get; set; }
            public string e { get; set; }
            public string u { get; set; }
            public string t { get; set; }
        }

        public class Author_Flair_Richtext
        {
            public string a { get; set; }
            public string e { get; set; }
            public string u { get; set; }
            public string t { get; set; }
        }

        public class All_Awardings
        {
            public object giver_coin_reward { get; set; }
            public object subreddit_id { get; set; }
            public bool is_new { get; set; }
            public object days_of_drip_extension { get; set; }
            public int coin_price { get; set; }
            public string id { get; set; }
            public object penny_donate { get; set; }
            public string award_sub_type { get; set; }
            public int coin_reward { get; set; }
            public string icon_url { get; set; }
            public object days_of_premium { get; set; }
            public object tiers_by_required_awardings { get; set; }
            public Resized_Icons[] resized_icons { get; set; }
            public int icon_width { get; set; }
            public int static_icon_width { get; set; }
            public object start_date { get; set; }
            public bool is_enabled { get; set; }
            public object awardings_required_to_grant_benefits { get; set; }
            public string description { get; set; }
            public object end_date { get; set; }
            public object sticky_duration_seconds { get; set; }
            public int subreddit_coin_reward { get; set; }
            public int count { get; set; }
            public int static_icon_height { get; set; }
            public string name { get; set; }
            public Resized_Static_Icons[] resized_static_icons { get; set; }
            public string icon_format { get; set; }
            public int icon_height { get; set; }
            public int? penny_price { get; set; }
            public string award_type { get; set; }
            public string static_icon_url { get; set; }
        }

        public class Resized_Icons
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Crosspost_Parent_List
        {
            public object approved_at_utc { get; set; }
            public string subreddit { get; set; }
            public string selftext { get; set; }
            public string author_fullname { get; set; }
            public bool saved { get; set; }
            public object mod_reason_title { get; set; }
            public int gilded { get; set; }
            public bool clicked { get; set; }
            public string title { get; set; }
            public object[] link_flair_richtext { get; set; }
            public string subreddit_name_prefixed { get; set; }
            public bool hidden { get; set; }
            public int? pwls { get; set; }
            public string link_flair_css_class { get; set; }
            public int downs { get; set; }
            public int? thumbnail_height { get; set; }
            public object top_awarded_type { get; set; }
            public bool hide_score { get; set; }
            public string name { get; set; }
            public bool quarantine { get; set; }
            public string link_flair_text_color { get; set; }
            public float upvote_ratio { get; set; }
            public object author_flair_background_color { get; set; }
            public string subreddit_type { get; set; }
            public int ups { get; set; }
            public int total_awards_received { get; set; }
            public Media_Embed1 media_embed { get; set; }
            public int? thumbnail_width { get; set; }
            public object author_flair_template_id { get; set; }
            public bool is_original_content { get; set; }
            public object[] user_reports { get; set; }
            public object secure_media { get; set; }
            public bool is_reddit_media_domain { get; set; }
            public bool is_meta { get; set; }
            public object category { get; set; }
            public Secure_Media_Embed1 secure_media_embed { get; set; }
            public string link_flair_text { get; set; }
            public bool can_mod_post { get; set; }
            public int score { get; set; }
            public object approved_by { get; set; }
            public bool is_created_from_ads_ui { get; set; }
            public bool author_premium { get; set; }
            public string thumbnail { get; set; }
            public bool edited { get; set; }
            public object author_flair_css_class { get; set; }
            public object[] author_flair_richtext { get; set; }
            public Gildings1 gildings { get; set; }
            public string post_hint { get; set; }
            public object content_categories { get; set; }
            public bool is_self { get; set; }
            public object mod_note { get; set; }
            public float created { get; set; }
            public string link_flair_type { get; set; }
            public int? wls { get; set; }
            public object removed_by_category { get; set; }
            public object banned_by { get; set; }
            public string author_flair_type { get; set; }
            public string domain { get; set; }
            public bool allow_live_comments { get; set; }
            public string selftext_html { get; set; }
            public object likes { get; set; }
            public object suggested_sort { get; set; }
            public object banned_at_utc { get; set; }
            public string url_overridden_by_dest { get; set; }
            public object view_count { get; set; }
            public bool archived { get; set; }
            public bool no_follow { get; set; }
            public bool is_crosspostable { get; set; }
            public bool pinned { get; set; }
            public bool over_18 { get; set; }
            public Preview1 preview { get; set; }
            public object[] all_awardings { get; set; }
            public object[] awarders { get; set; }
            public bool media_only { get; set; }
            public bool can_gild { get; set; }
            public bool spoiler { get; set; }
            public bool locked { get; set; }
            public object author_flair_text { get; set; }
            public object[] treatment_tags { get; set; }
            public bool visited { get; set; }
            public object removed_by { get; set; }
            public object num_reports { get; set; }
            public object distinguished { get; set; }
            public string subreddit_id { get; set; }
            public bool author_is_blocked { get; set; }
            public object mod_reason_by { get; set; }
            public object removal_reason { get; set; }
            public string link_flair_background_color { get; set; }
            public string id { get; set; }
            public bool is_robot_indexable { get; set; }
            public object report_reasons { get; set; }
            public string author { get; set; }
            public object discussion_type { get; set; }
            public int num_comments { get; set; }
            public bool send_replies { get; set; }
            public string whitelist_status { get; set; }
            public bool contest_mode { get; set; }
            public object[] mod_reports { get; set; }
            public bool author_patreon_flair { get; set; }
            public object author_flair_text_color { get; set; }
            public string permalink { get; set; }
            public string parent_whitelist_status { get; set; }
            public bool stickied { get; set; }
            public string url { get; set; }
            public int subreddit_subscribers { get; set; }
            public float created_utc { get; set; }
            public int num_crossposts { get; set; }
            public object media { get; set; }
            public bool is_video { get; set; }
            public string link_flair_template_id { get; set; }
        }

        public class Media_Embed1
        {
        }

        public class Secure_Media_Embed1
        {
        }

        public class Gildings1
        {
        }

        public class Preview1
        {
            public Image1[] images { get; set; }
            public bool enabled { get; set; }
        }

        public class Image1
        {
            public Source3 source { get; set; }
            public Resolution3[] resolutions { get; set; }
            public Variants1 variants { get; set; }
            public string id { get; set; }
        }

        public class Source3
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Variants1
        {
        }

        public class Resolution3
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

    }
}
