   M S B A M L   `   `   `  ���� G  CRedditCLient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:RedditCLientRedditCLient  I)clr-namespace:RedditCLient.MVVM.ViewModelRedditCLient.MVVM.ViewModel      RedditCLient.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:RedditCLient     ?	viewModel)clr-namespace:RedditCLient.MVVM.ViewModel     $	��600��$	��920��  � WindowStyle$
  None=� � 
ResizeMode$ CanResizeWithGrip=�
������  � AllowsTransparency$
 True�� � DataContext 1   +RedditCLient.MVVM.ViewModel.MainWindowModel  .���� ���#202020 �� CornerRadius$ 20��._��  � ColumnDefinitions �� $	��200 ���  � RowDefinitions �� $��75 ��� .+��� $��/images/icon.png��$
��Left=�$��10,0,0,0q�$��45���� $��1��.+�	� $��⌂Home��$��50��
�������$��18(� d� 	IsChecked$
 TrueY�   MenuButtonTheme#��[  ���� *HomePageCommand+	� $	��Hot��$��50��$��20(�
�������#��[  ���� *HotCategoryCommand+	� $	��New��$��50��
�������$��18(�#��[  ���� *NewCategoryCommand+	� $	��Top��$��50��
�������$��18(�#��[  ���� *TopCategoryCommand+�� 
����=H�$��16(�$��Center=�$
��Left=�$��20,0,0,0q����� *UserSubsTitle+�� 
�������
������ $��Disabled=�$
��True��$	��200��  UserSubsStyle#��[ ���� *
UserSubs+ �� SelectedValue �� *SelectedUserSub+� $��1��.+��� -   	 ��Name$	 	SearchBox��$	��200��$��40��$��Center=�$
��Left=�$��5q�  SearchTheme#��[ �� $
��🔍��$��1��$
��Left=�$	��Top=�$��
200,15,0,0q�$��40��$��40��$��20(�$��0q����#586c7d
������ ���� *SearchButtonCommand+� -   
 �Name$
 SearchPopup��$��Bottom=�$��0,40,0,0q�$	��200��$	��200�� � AllowsTransparency$
 True�� � PlacementTarget ��  �� ElementName$ 	SearchBox������ *PopupIsOpen+.���� 
�������$��Disabled=�$
��True��  SearchResultsTheme#��[  �� *SelectedSubreddit+���� *SearchResults+�� -   $��10q�
������ $��Right=�$	��470��$��40��._���  �� Orientation$ 
Horizontal=�$��Right=�.+��� $
��🗕��$��30��$��30��$��20(�$��0q����#586c7d
������ $��0,0,0,3q����� *MinimizeButtonCommand+�� $	��⎕��$��30��$��30��$��20(�$��0q����#586c7d
������ ���� *MaximixeButtonCommand+�� $	��⨉��$��30��$��30��$��20(�$��0q����#586c7d
������ ���� *CloseWindowCommand+�� $��1��$��1��$��	10,0,10,0q�.+��� $ 
Horizontal=�$��Center=�.+��� 
�������$��20(�$��Verdana)����� *Title+�� 
������ ���#586c7d$��0q�$��20,0,0,0q����� *SubscribeButtonText+{��� *SubscribeButtonIsOpen+���� *SubscribeCommand+�� 
�������$
��Wrap=�$��Center=�$��14(�$��Verdana)�$��40������ *Description+�� -    ��Name$ ListView��
������ $��1��$��1��$��
10,60,10,0q�$��Disabled=�$��False��  	ListStyle#��[ ���� *Posts+ �� SelectedItem �� *SelectedPost+���� .R�]� $��4q�����  �� 
TargetType" ]� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� Property � �� �� Value �#303030P�  �@J�IsMouseOver$ False��. ���  � ��
 ���� ��  �� Binding ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ 
IsSelected� �� Value True.a���  � �� �#303030.+��� $��4,0,0,0q�.+��� $ 
Horizontal=�.+��� 
����������#586c7d$��12(�$��Normal$�$��15������ *subreddit_name_prefixed+�� $
��Bold"�$
��Wrap=�
�������$��24(����� *title+�� $��30q�$
��Left=� �� BitmapScalingMode$ HighQuality=�$	��400������ *url_overridden_by_dest+ �� TargetNullValue Z� �� $��CharacterEllipsis=�$	��200��$
��Bold"�$
��Wrap=�
�������$��14(����� *
selftext+�� $ 
Horizontal=�$
��Left=�.+�	� $	��▲��$��18(�
������ $��0q�  UpvoteButton#��[ ���� $ DataContext.UpvoteCommand� �� *FindAncestor+" ���� $��18(����� *score+	� $
��▼ ��$��18(�
������ $��0q�#��[ ���� $! DataContext.DownvoteCommand� �� *FindAncestor+" ��� -   $
 CommentsPopup��$��RelativePoint=�$	��700��$	��465��$��1��$��1�� �� $ ListView������ *CommentsIsOpen+.��� ���#232323.+��� $��Close��$	��Top=�$
��Left=�
����������#232323$��0q����� *CloseCommentsCommand+R� $��Stretch=�$��0,20,0,0q����#232323$��Stretch=�$��Disabled=����� *CommentsList+����  �� ItemsSource �� *RepliesList+.R��� .���� $
��Wrap=�
�������  �� LineStackingStrategy$  BlockLineHeight=�! �� 
LineHeight$! 20��$��16(����� *CommentBody+" �� ItemTemplate" ��  �� *RepliesList+.R��� .���� $  BlockLineHeight=�$! 20��
�������$��14(����� *CommentBody+�� $��1��$��0��$ 
Horizontal=�$��Right=�$��Bottom=�$��0,0,0,10q�.+��� $��80��$��40��$��Authorize app�����#586c7d
������ $��0q�$
��Left=�$��Bottom=����� *AuthorizeCommand+�� $	��←��$��30��$��30��$��20(�$��0q�
�������
������ ���� *LoadPrevPageCommand+�� $��30��$��30��$��16(�# �� Padding$# 6q�
����������� *
PageNumber+�� $	��→��$��30��$��30��$��20(�$��0q�
�������
������ ���� *LoadNextPageCommand+