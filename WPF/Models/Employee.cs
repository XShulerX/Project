﻿using System;
using System.ComponentModel;

namespace WPF.Models
{
    class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Name;

        private Department _Department;

        public int Id { get; set; }

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string SurName { get; set; }

        public Department Department
        {
            get => _Department;
            set
            {
                _Department = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Department)));
            }
        }

        public string Patronymic { get; set; }

        public DateTime DayOfBirth { get; set; }

        public int Age => (int)Math.Floor((DateTime.Now - DayOfBirth).TotalDays / 365);

        public override string ToString() => $"Сотрудник[{Id}]: {SurName}";
    }
}
