   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TaskManager.ViewModelsTaskManager.ViewModels  5   	       TaskManager.NewTeamWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TaskManager.ViewModels     5   	     � Title$  NewTeamWindow��5   	   $	��200��6   $	��400��6,    � DataContext 5
      -   'TaskManager.ViewModels.NewTeamViewModel  5   
   .��5      � ���#D5E3F2 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6!   $��New Team��61   $��Center=�6A   $��Center=�6\   $��20(�6y   $��	0,10,0,20q�6�   �� 5   
   $��1��6   $��0��6!   $��Name:��61   $��Center=�6>   $��Right=�6Y   $��10q�6u   �� 5   
   -   5        $��TeamName��5   /   $��1��6   $��1��6   $��10q�6?   $	��200��6K   ��6Z   �� *
TeamName+�� 5    
   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��10q�6h   .+�5!      �� -   5        $��OK��5!      $��80��6"   ���#005A9E
�������6S   �� 5"      -   5        $��Cancel��5"      $��80��6&   ���#005A9E
�������$��10,0,0,0q�6n   5#      5$      5%      