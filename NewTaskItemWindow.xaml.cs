﻿using System.Windows;
using TaskManager.Entities;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewTaskItemWindow : Window
    {
        NewTaskItemViewModel viewModel;
        public NewTaskItemWindow(TaskItem editTaskItem = null)
        {
            InitializeComponent();
            viewModel = new NewTaskItemViewModel(editTaskItem);
            DataContext = viewModel;
            if (editTaskItem != null && editTaskItem.ProjectId != 0)
            {
                AddToProjectCheckBox.Visibility = Visibility.Collapsed;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool result = viewModel.OK();
            if (result)
            {
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToProjectCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ProjectComboBox.Visibility = Visibility.Visible;
        }

        private void AddToProjectCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ProjectComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
