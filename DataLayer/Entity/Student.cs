﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public bool Orphan { get; set; }
        public bool NeedHostel { get; set; }
        public BasisOfLearning BasisOfLerning { get; set; }
        public string PhoneOfStudent { get; set; }
        public string PhoneOfParents { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Scholarship { get; set; }
        public Group Group { get; set; }
        public int PercentOfAttedence { get; set; }
        public int PercentOfProgress { get; set; }
        public List<EventOfStudent> EventOfStudents { get; set; }
        public List<HistoryChangeStudent> HistoryChangeStudent{ get; set; }
        public DateTime CreateDate { get; set; }
    }
}