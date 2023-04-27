using System.Collections;
using System.Collections.ObjectModel;

namespace RedditCLient.MVVM.Model
{
    public class CommentsData
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string kind { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public object? after { get; set; }
            public int? dist { get; set; }
            public object? modhash { get; set; }
            public string? geo_filter { get; set; }
            public Child[] children { get; set; }
            public object? before { get; set; }
        }

        public class Child
        {
            public string? kind { get; set; }
            public Data1? data { get; set; }
        }

        public class Data1
        {
            public object? approved_at_utc { get; set; }
            public string? subreddit { get; set; }
            public string? selftext { get; set; }
            public object[]? user_reports { get; set; }
            public bool? saved { get; set; }
            public object? mod_reason_title { get; set; }
            public int? gilded { get; set; }
            public bool? clicked { get; set; }
            public string? title { get; set; }
            public Link_Flair_Richtext[]? link_flair_richtext { get; set; }
            public string? subreddit_name_prefixed { get; set; }
            public bool? hidden { get; set; }
            public int? pwls { get; set; }
            public string? link_flair_css_class { get; set; }
            public int? downs { get; set; }
            public int? thumbnail_height { get; set; }
            public object? top_awarded_type { get; set; }
            public string? parent_whitelist_status { get; set; }
            public bool? hide_score { get; set; }
            public Media_Metadata? media_metadata { get; set; }
            public string? name { get; set; }
            public bool? quarantine { get; set; }
            public string? link_flair_text_color { get; set; }
            public float? upvote_ratio { get; set; }
            public string? author_flair_background_color { get; set; }
            public string? subreddit_type { get; set; }
            public int? ups { get; set; }
            public int? total_awards_received { get; set; }
            public Media_Embed? media_embed { get; set; }
            public int? thumbnail_width { get; set; }
            public string? author_flair_template_id { get; set; }
            public bool? is_original_content { get; set; }
            public string? author_fullname { get; set; }
            public object? secure_media { get; set; }
            public bool? is_reddit_media_domain { get; set; }
            public bool? is_meta { get; set; }
            public object? category { get; set; }
            public Secure_Media_Embed secure_media_embed { get; set; }
            public string? link_flair_text { get; set; }
            public bool? can_mod_post { get; set; }
            public int? score { get; set; }
            public object? approved_by { get; set; }
            public bool? is_created_from_ads_ui { get; set; }
            public bool? author_premium { get; set; }
            public string? thumbnail { get; set; }
            public bool? edited { get; set; }
            public string? author_flair_css_class { get; set; }
            public Author_Flair_Richtext[]? author_flair_richtext { get; set; }
            public Gildings? gildings { get; set; }
            public object? content_categories { get; set; }
            public bool? is_self { get; set; }
            public object? mod_note { get; set; }
            public float? created { get; set; }
            public string? link_flair_type { get; set; }
            public int? wls { get; set; }
            public object? removed_by_category { get; set; }
            public object? banned_by { get; set; }
            public string? author_flair_type { get; set; }
            public string? domain { get; set; }
            public bool? allow_live_comments { get; set; }
            public string? selftext_html { get; set; }
            public object? likes { get; set; }
            public object? suggested_sort { get; set; }
            public object? banned_at_utc { get; set; }
            public object? view_count { get; set; }
            public bool? archived { get; set; }
            public bool? no_follow { get; set; }
            public bool? is_crosspostable { get; set; }
            public bool? pinned { get; set; }
            public bool? over_18 { get; set; }
            public object[]? all_awardings { get; set; }
            public object[]? awarders { get; set; }
            public bool? media_only { get; set; }
            public string? link_flair_template_id { get; set; }
            public bool? can_gild { get; set; }
            public bool? spoiler { get; set; }
            public bool? locked { get; set; }
            public string? author_flair_text { get; set; }
            public object[]? treatment_tags { get; set; }
            public bool? visited { get; set; }
            public object? removed_by { get; set; }
            public object? num_reports { get; set; }
            public string? distinguished { get; set; }
            public string? subreddit_id { get; set; }
            public bool? author_is_blocked { get; set; }
            public object? mod_reason_by { get; set; }
            public object? removal_reason { get; set; }
            public string? link_flair_background_color { get; set; }
            public string? id { get; set; }
            public bool? is_robot_indexable { get; set; }
            public int? num_duplicates { get; set; }
            public object? report_reasons { get; set; }
            public string? author { get; set; }
            public object? discussion_type { get; set; }
            public int? num_comments { get; set; }
            public bool? send_replies { get; set; }
            public object? media { get; set; }
            public bool? contest_mode { get; set; }
            public bool? author_patreon_flair { get; set; }
            public string? author_flair_text_color { get; set; }
            public string? permalink { get; set; }
            public string? whitelist_status { get; set; }
            public bool? stickied { get; set; }
            public string? url { get; set; }
            public int? subreddit_subscribers { get; set; }
            public float? created_utc { get; set; }
            public int? num_crossposts { get; set; }
            public object[]? mod_reports { get; set; }
            public bool? is_video { get; set; }
            public object? comment_type { get; set; }
            public object? replies { get; set; }
            public object? collapsed_reason_code { get; set; }
            public string? parent_id { get; set; }
            public bool? collapsed { get; set; }
            public string? body { get; set; }
            public bool? is_submitter { get; set; }
            public string? body_html { get; set; }
            public object? collapsed_reason { get; set; }
            public object? associated_award { get; set; }
            public object? unrepliable_reason { get; set; }
            public bool? score_hidden { get; set; }
            public string? link_id { get; set; }
            public int? controversiality { get; set; }
            public int? depth { get; set; }
            public object? collapsed_because_crowd_control { get; set; }
        }

        public class Media_Metadata
        {
            public Apve77hgi3ra1? apve77hgi3ra1 { get; set; }
            public Csy1khgii3ra1? csy1khgii3ra1 { get; set; }
        }

        public class Apve77hgi3ra1
        {
            public string? status { get; set; }
            public string? e { get; set; }
            public string? m { get; set; }
            public P[]? p { get; set; }
            public S? s { get; set; }
            public string id { get; set; }
        }

        public class S
        {
            public int? y { get; set; }
            public int? x { get; set; }
            public string u { get; set; }
        }

        public class P
        {
            public int? y { get; set; }
            public int? x { get; set; }
            public string? u { get; set; }
        }

        public class Csy1khgii3ra1
        {
            public string? status { get; set; }
            public string? e { get; set; }
            public string? m { get; set; }
            public P1[]? p { get; set; }
            public S1? s { get; set; }
            public string? id { get; set; }
        }

        public class S1
        {
            public int? y { get; set; }
            public int? x { get; set; }
            public string? u { get; set; }
        }

        public class P1
        {
            public int? y { get; set; }
            public int? x { get; set; }
            public string? u { get; set; }
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

        public class Link_Flair_Richtext
        {
            public string? e { get; set; }
            public string? t { get; set; }
        }

        public class Author_Flair_Richtext
        {
            public string? a { get; set; }
            public string? u { get; set; }
            public string? e { get; set; }
            public string? t { get; set; }
        }

    }
}
