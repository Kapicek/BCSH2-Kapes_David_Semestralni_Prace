   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TaskManager.ViewModelsTaskManager.ViewModels  5   	   #    TaskManager.NewTaskItemWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TaskManager.ViewModels     5   	     � Title$  New Task Item��5   	   $	��400��6   $	��400��6,    � DataContext 5
      1   +TaskManager.ViewModels.NewTaskItemViewModel  5   
   .��5      � ���#D5E3F2 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6!   $��New Task Item��61   $��Center=�6F   $��Center=�6a   $��20(�6~   $��	0,10,0,20q�6�   �� 5!   
   $��1��6   $��0��6!   $��Name:��61   $��Center=�6>   $��Right=�6Y   $��10q�6u   �� 5"   
   -   5        $
��Name��5"   /   $��1��6   $��1��6   $��10q�6;   $	��200��6G   ��6V   �� *Name+�� 5$   
   $��2��6   $��0��6!   $��Description:��61   $��Center=�6E   $��Right=�6`   $��10q�6|   �� 5%   
   -   5        $��Description��5%   /   $��2��6   $��1��6   $��10q�6B   $	��200��6N   ��6]   �� *Description+�� 5'   
   $��3��6   $��0��6!   $��	Priority:��61   $��Center=�6B   $��Right=�6]   $��10q�6y   �� 5(   
   -   5        $��Priority��5(   /   $��3��6   $��1��6   $��10q�6?   $	��200��6K   ��6Z   �� *
Priority+�� 5*   
   $��4��6   $��0��6!   $��Status:��61   $��Center=�6@   $��Right=�6[   $��10q�6w   �� 5+   
   -   5        $��Status��5+   /   $��4��6   $��1��6   $��10q�6=   $	��200��6I   ��6X   �� *Status+�� 5-   
   -   5        $��AddToProjectCheckBox��5-   0   $��5��6   $��1��6    $��Add to project��6L   $��Center=�6e   $
��Left=�6�   $��10q�6�    d� 	IsChecked 6�   �� *AddToProjectChecked+6�   �� 50   
   -   5        $��ProjectComboBox��50   0   $��6��6   $��1��6    $��10q�6G   $	��200��6S   $y�	Collapsed=�6b   ��6y   �� *
Projects+ �� SelectedItem 6�   �� *SelectedProject+51      �� 54   
   $��7��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��10q�6h   .+�55      �� -   5        $��OK��55      $��80��6"   ���#005A9E
�������6S   �� 56      -   5        $��Cancel��56      $��80��6&   ���#005A9E
�������$��10,0,0,0q�6n   57      58      59      