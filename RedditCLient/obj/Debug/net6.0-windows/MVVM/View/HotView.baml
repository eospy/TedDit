   M S B A M L   `   `   `  ����G  CRedditCLient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RedditCLient.MVVM.ViewRedditCLient.MVVM.View  5      I)clr-namespace:RedditCLient.MVVM.ViewModelRedditCLient.MVVM.ViewModel  5      %    RedditCLient.MVVM.View.FeedView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:RedditCLient.MVVM.View     5      ?	viewModel)clr-namespace:RedditCLient.MVVM.ViewModel     5      $	��600��5
   
   $	��920��6     � WindowStyle$
  None=�5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   
������� � AllowsTransparency$
 True��5   	    � DataContext 5      .   (RedditCLient.MVVM.ViewModel.HotViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      $��75 �6   6*   �� 5      6   .+�5   
   �� -   5         ��Name$ 	SearchBox��5      $��1��5      $	��200��6"   $��40��5      $	��Top=�5       $
��Left=�5!      $��5q�5"         SearchTheme#��[  5#      �� 5$   
   $
��🔍��6   $��1��5%      $
��Left=�5&      $	��Top=�5'      $��	200,5,0,0q�5(      $��40��5)      $��40��5*      $��20(�5+      $��0q�5,      
����=H�
������ 5.   ,   � 5/   
   -   5         �Name$ SearchPopup��5/      $��Bottom=�51      $��0,40,0,0q�52      $	��200��53      $	��200��54       � AllowsTransparency$
 True��56      	 � PlacementTarget	 50      �� 
 �� ElementName$
 	SearchBox����55      �� *PopupIsOpen+.��58      �� 
�������$��Disabled=�59      $
��True��5:        SearchResultsTheme#��[ 5=      ��5;      �� *SearchResults+ �� SelectedItem 5<      �� *SelectedSubreddit+5?      5A      5D      5F      