   M S B A M L   `   `   `  ����A  =course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:coursecourse  5   	       course.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:course     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��500��6)    � 
ResizeMode$ NoResize=�65   .��5	      � 
���   � � RowDefinitions 5
   
   �� 5      $��50 �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��207* �6   6.   .+�5   
   �� $3��-Регистрация/Авторизация��6   
�������$��	170 0 0 0q�6F   $��2��6Y   �� 5   
   $��1��6   $	��100��6"   $��	0,0,0,171q�6/   $��2��6B   .+�5      �� $��
Логин��6   
�������6%   �� 5      ��6   �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Пароль��6   
�������6&   �� 5       � ToolTip$k eЕсли при регистрации не был указан пароль то он будет (0)��6U   ��6   �� *
Password+$ PropertyChanged=�6U   �� 5      -   5         ��Name$
 chek��5      
���������6&   �� *Authorization+$ PropertyChanged=�6�   5      �� 5   
   -   5         ��Name$ button2��5      $��30��6"   $	��250��6.   $��0 0 0 0q�6:   $��Вход��6K   
�������
���   �$��1��6�   ��6�   �� *
entrance+�� 5   
   -   5        $ button1��5      $��30��6"   $	��250��6.   $��	0 100 0 0q�6:   $��Регистрация��6M   
�������
���   �$��1��6�   ��6�   �� *OpenWindowsCommand1+5      5      