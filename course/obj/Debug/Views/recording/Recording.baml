   M S B A M L   `   `   `  ����A  =course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:coursecourse  5   	       course.Recording   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:course     5   	     � Title$  	Recording��5   	   $	��370��6   $	��500��6)    � 
ResizeMode$ NoResize=�65   .��5	      � 
���   � � RowDefinitions 5
   
   �� 5      $��50 �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��207* �6   6.   .+�5   
   �� $��Регистрация��6   
�������$��0,17,0,0q�6:    �� TextAlignment$ Center=�6L   �� 5   
   -   5         ��Name$ button2��5      $��103,200,128,32q�6"   $*��$Зарегистрироваться��6:   
�������
���   �$��1��6~   ��6�   �� *
checkfor+�� 5   
   $��1��6   $��0,0,0,96q�6"   .+�5      �� $��
Логин��6   
�������$ Center=�68   �� 5       � ToolTip$� �В логине должна быть минимум одна цифра, одна буква(английская) и любой знак(и), который не цифра и не буква, максимальная длина пароля 16 символов.��6Z   ��6   �� *Login_for_reg+ �� UpdateSourceTrigger$ PropertyChanged=�6Z   �� 5      $��Пароль��6   
�������$ Center=�69   �� 5      ��6   �� *Password_for_reg+$ PropertyChanged=��� 5      $��E-mail��6   
�������$ Center=�69   �� 5      ��6   �� *E_mail_for_reg+$ PropertyChanged=��� 5      -   5         ��Name$ chek1��5      $��51��6d   
���������6'   �� *Status+$ PropertyChanged=�6p   5      5      5      