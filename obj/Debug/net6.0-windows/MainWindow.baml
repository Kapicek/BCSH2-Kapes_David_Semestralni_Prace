   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TaskManager.ViewModelsTaskManager.ViewModels  5   	       TaskManager.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TaskManager.ViewModels     5   	     � Title$  
MainWindow��5   	   $	��459��6   $
��1027��6)    � DataContext 5
      *   $TaskManager.ViewModels.MainViewModel  5   
    � 	Resources 5         RecordContextMenu��%�   &	        �� 5   
   .@�5      m� -   5        $
��Edit��5      6%   m� 5      -   5        $��Delete��5      6'   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#007ACC.+�5      �� $��Center=�66   $��Center=�6S   $��20(�6n   
�������$��0,10,0,0q�6�   ��6   �� *CategoryName+6�   �� 5      $��Projects��6   $	��100��6(   $��30��64   $��10q�6@   ��6L   �� *LoadProjectsCommand+�� 5      $��
Task Items��6   $	��100��6*   $��30��66   $��10q�6B   ��6N   �� *LoadTaskItemsCommand+�� 5      $��Teams��6   $	��100��6%   $��30��61   $��10q�6=   ��6I   �� *LoadTeamsCommand+�� 5      $��Workers��6   $	��100��6'   $��30��63   $��10q�6?   ��6K   �� *LoadWorkersCommand+�� 5       $��Assign task��6   $	��100��6+   $��30��67   $��10q�6C   ��6O   �� *AssignTaskCommand+�� 5!      $��Worker info��6   $	��100��6+   $��30��67   $��10q�6C   ��6O   �� *ShowWorkerInfoCommand+�� 5"      $��Add new��6   $��64��6'   $��30��62   $��10q�6>   ��6J   �� *AddNewCommand+�� 5#      $��PTM pro editaci/mazání��6   $��Center=�66   $��Center=�6S   $��10(�6n   
�������$��0,10,0,0q�6�   5$      &   System.Windows.Controls.DataGrid  5&   
   -   5          Name$ dataGrid��5&   >   $��1��6      AutoGenerateColumns$
 True��6#      
IsReadOnly$
 True��6P   ��6b   �� *Items+��5'      �� 5(       �� 
TargetType"  6   .�5)      ��  �� Property � ��	 �� Value	 �#005A9E63   �� 5*       � ��
	 �����63   �� 5+       � ��	 �#003D6A64   �� 5,       � ��$	 1q�68   �� 5-       �@ RowBackground	 �#003D6A66   �� 5.       �@ RowHeaderWidth$	 0Q�67   �� 5/       �@ VerticalGridLinesBrush	 �#003D6A6?   �� 50        �@ HorizontalGridLinesBrush	 �#003D6A6A   51      
   	CellStyle
 54      �� 55      *  $System.Windows.Controls.DataGridCell"  6   .�56      ��  � ��$	 0q�68   �� 57       � ��$	 10q�6/   �� 58       �@��TextAlignment$
	 Left=�6@   �� 59       � ��$	 16(�61   5:         RowStyle 5=      �� 5>      )  #System.Windows.Controls.DataGridRow"  6   .�5?      ��  �@�ContextMenu#	 [  64    �� Triggers 5@      P� 5A       P� Property �@ 
IsSelected P� Value$
 True��68   . �5B      ��  � ��	 �#007ACC6;   �� 5C       � ��
	 �   �6;   5D      5F         ColumnHeaderStyle 5I      �� 5J      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5K      ��  � ��	 �#005A9E63   �� 5L       � ��
	 �����63   �� 5M       � ��$
	 Bold"�63   �� 5N       � ��$	 10q�6/   5O      5Q      5R      5S      