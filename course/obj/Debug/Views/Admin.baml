   M S B A M L   `   `   `  ����A  =course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:course.Viewscourse.Views  5   	       course.Views.Admin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:course.Views     5   	     � Title$  Admin��5   	   $	��500��6   $
��1500��6$    � 
ResizeMode$ NoResize=�61   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��300 �6   6-   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5   
   �� $��25��6   $��0,0,0,30q�6   $��Показать��61   ��6D   �� *	ViewAdmin+�� 5   
   $��25��6   $��3��6   $��0,0,0,30q�6.   $��Изменить��6@   ��6S   �� *AdminCommand+�� 5   
   -   5         ��Name$
 chek��5      $��2��6"   $��40��6t   $��
0,0,0,-190q�6�   ��62   �� *StatusAdmin+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��1��6   .��5      �� .+�5      �� $��10q�6   $��22(�6)   $��Medium"�67   �� 5      $
��20 0q�6   .+�5      �� -   5         ��Name$ ListViewProducts��5      $��Disabled=�6d   $��Disabled=�5      ��5   8   �� *_UserA+ �� Mode$ Default=� �� SelectedIndex 5   V   �� *SelectIndexAdmin+��5      �� 5      .R�5   "   �� $��10q�6)   $	��450��65   $
��auto��6A   $��2q�6O   
���   �._�5   &   �� .+�5   *   ��  �� Orientation$ 
Horizontal=�65   .+�5   .   �� $��Логин: ��68   �� 5   .   $��25��68   ��6D   �� *
Логин+6^   5    +   �� 5!   *   $ 
Horizontal=�65   .+�5"   .   �� $��e-mail: ��68   �� 5#   .   $��25��68   ��6D   �� *	e_maeil+6_   5$   +   5%   '   5&   #   5'      5)      5*      5+      5,      �� 5/   
   $��3��6   .+�50      �� $��e-mail��6   $��15��6&   �� 51      $��15��67   ��6   �� *E_mail_for_reg1+67   52      53      54      