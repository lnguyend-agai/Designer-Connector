   M S B A M L   `   `   `  ����[  WButtonizedTitleBarWPFUserControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ButtonizedTitleBarWPFUserControl ButtonizedTitleBarWPFUserControl  5      G    AButtonizedTitleBarWPFUserControl.ButtonizedTitleBarWPFUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:ButtonizedTitleBarWPFUserControl     5      U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5	      $��25��5        � 	Resources  5         NavigationButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value	 �� 
TargetName  CaptionButtonStyle
 �Name ��Name �� ClearTypeHint&   System.Windows.Media.TextOptions   TextRenderingMode   TextFormattingMode  MinimizeButtonStyle �� BasedOn  MaximizeButtonStyle  RestoreButtonStyle  CloseButtonStyle��%�  &	        &	 �    &	     7  &	 c    7  &	 �    7  &	 �    7  �� 5   
   " ��6.   .�5      ��  � ��
 ���� 6+   �� 5       � �� �#0000006,   �� 5       � ��$ 1q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ Border��6!   #��z��61   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $	 Border��6O    � �� �#d1e1f35      5      5      5      �� 5!   
   " ��6+   .�5"      ��  � �� 5#      �� 5$      " ��6&   .R�5%      � $
 
LayoutRoot��6   
������ $��44��6L   $��30��6W   .+�5&      �� $	 txt��6(   #��z��65   $��Segoe MDL2 Assets)�6V   $��10(�6u   ���#000000$��Center=�5'   9   $��Center=�6V   $
 Auto=�5(   $   $ Aliased=�6G   $ Display=�6p   5)       5*      P� 5+       �@J�IsMouseOver . . �5,   "   �� $	 
LayoutRoot��6)    � �� �#E5E5E56W   �� 5-   "   $		 txt��6)    � �� �#0000006P   5.      50      52      53      �� 55   
   " ��6,   8   .�56      ��  � ��$	 ��6(   57      �� 59   
   " ��6,   8   .�5:      ��  � ��$	 ��6(   5;      �� 5=   
   " ��6+   8   .�5>      ��  � ��$	 ��6(   5?      �� 5A   
   " ��6)   8   .�5B      ��  � ��$	 ��6(   �� 5C       � �� 5D      �� 5E      " ��6&   .R�5F      � $
 
LayoutRoot��6   
������ $��44��6L   $��30��6W   .+�5G      �� $	 txt��6(   #��z��65   $��Segoe MDL2 Assets)�6V   $��10(�6u   ���#000000$��Center=�5H   9   $��Center=�6V   $
 Auto=�5I   $   $ Aliased=�6G   $ Display=�6p   5J       5K      P� 5L       �@J�IsMouseOver . . �5M   "   �� $	 
LayoutRoot��6)    � �� �#E811236W   �� 5N   "   $		 txt��6)    � �� �#FFFFFF6P   5O      5Q      5S      5T      5U      .��5V      � -   5        $��0��5V      $��0��6   $��11(�5W      $��Segoe UI)�5X      ���#e3ecf6 � RowDefinitions 5Z   
   �� 5[      $��25 �6   6+    � ColumnDefinitions 5]   
   �� 5^      $��5 �6   6+   �� 5_      $��27 �6   6,   �� 5`      $��27 �6   6,   �� 5a      $��27 �6   6,   �� 5b      $��27 �6   6,   �� 5c      6"   �� 5d      $
��Auto �6   6.   .+�5f   
   �� -   5         ��Name$ HomeBtn��5f      $��1��5g      $��0��5h      $��21��5i      $��21��5j      
������ 
������ #��[  5n       � ToolTip$
 Home��5o      .��5p      �� $��home.ico��6   6(   5q      �� 5r   
   -   5        $ BackBtn��5r      $��2��5s      $��0��5t      $��21��5u      $��21��5v      
������ 
������ #��[  5z      $
 Back��5{      .��5|      �� $��back.ico��6   6(   5}      �� 5~   
   -   5        $ 
ForwardBtn��5~      $��3��5      $��0��5�      $��21��5�      $��21��5�      
������ 
������ #��[  5�      $ Forward��5�      .��5�      �� $��forward.ico��6   6+   5�      �� 5�   
   -   5        $ 	ReloadBtn��5�      $��4��5�      $��0��5�      $��21��5�      $��21��5�      
������ 
������ #��[  5�      $ Refresh��5�      .��5�      �� $��
reload.ico��6   6*   5�      5�      5�      