   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TaskManager.ViewModelsTaskManager.ViewModels  5   	   "    TaskManager.NewProjectWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TaskManager.ViewModels     5   	     � Title$  New Project��5   	   $	��270��6   $	��462��6*    � DataContext 5
      0   *TaskManager.ViewModels.NewProjectViewModel  5   
   .��5      � ���#D5E3F2 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6!   $��New Project��61   $��Center=�6D   $��Center=�6_   $��20(�6|   $��	0,10,0,20q�6�   �� 5   
   $��1��6   $��0��6!   $��Name:��61   $��Center=�6>   $��Right=�6Y   $��10q�6u   �� 5    
   -   5        $
��Name��5    /   $��1��6   $��1��6   $��10q�6;   $	��200��6G   ��6V   �� *ProjectName+�� 5!   
   -   5        $��AddToTeamCheckBox��5!   0   $��2��6   $��1��6    $��Add to team��6I   $��Center=�6_   $
��Left=�6z   $��10q�6�    d� 	IsChecked 6�   �� *AddToTeamChecked+�� 5#   
   -   5        $��TeamComboBox��5#   0   $��3��6   $��1��6    $��10q�6D   $	��200��6P    �� DisplayMemberPath$
 Name��5$   )   ��6   �� *Teams+ �� SelectedItem 6B   �� *SelectedTeam+��5%      �� 5&       �� 
TargetType" ��6   .�5'      ��  �� Property � y�	 �� Value$	 	Collapsed=�63   
 �� Triggers
 5(      �� 5)       �� Binding 6&   �� *AddToTeamChecked+ �� Value True6K   .a�5*      ��  � y�$	 Visible=�6;   5+      5-      5/      �� 51   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��10q�6h   .+�52      �� -   5        $��OK��52      $��80��6"   ���#005A9E
�������6U   �� 53      -   5        $��Cancel��53      $��80��6&   ���#005A9E
�������$��10,0,0,0q�6Y   6k   54      55      56      