   M S B A M L   `   `   `  ����G  CRedditCLient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RedditCLient.MVVM.ViewRedditCLient.MVVM.View  5   	   I)clr-namespace:RedditCLient.MVVM.ViewModelRedditCLient.MVVM.ViewModel  5   	   &     RedditCLient.MVVM.View.VideoView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:RedditCLient.MVVM.View     5   	   ?	viewModel)clr-namespace:RedditCLient.MVVM.ViewModel     5   	   $	��600��5	   	   $	��920��6     � WindowStyle$
  None=�5
   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      0   *RedditCLient.MVVM.ViewModel.VideoViewModel  5   
   .��5      �� ���#202020 �� CornerRadius$ 20��5      ._�5   
   �  � RowDefinitions 5      �� 5      $��75 �6    6.   �� 5      6"   .+�5      � $��1��6   .+�5      �� -   5        $��10q�5      
������ $��Right=�5      $	��470��5      $��40��5      ._�5      ��  �� Orientation$ 
Horizontal=�6!   $��Right=�6:   .+�5       �� $
��🗕��6!   $��30��5!   !   $��30��5"   !   $��20(�5#   !   $��0q�5$   !   ���#586c7d
������ $��0,0,0,3q�5'   !   ��5(   !   �� *MinimizeButtonCommand+5*      �� 5+      $	��⎕��6!   $��30��5,      $��30��5-      $��20(�5.      $��0q�5/      ���#586c7d
������ ��52      �� *MaximixeButtonCommand+54      �� 55      $	��⨉��6!   $��30��56      $��30��57      $��20(�58      $��0q�59      ���#586c7d
������ ��5<      �� *CloseWindowCommand+5>      5?      5@      5A      r� 5B      $��1��6    r� Stretch$
 Fill=�5C       r� Source 5D      �� *
Videouri+5F      5G      5H      5I      