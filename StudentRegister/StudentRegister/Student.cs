﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StudentRegister
{
    /// <summary>
    /// A class representing a student
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        private List<CourseResult> courseHistory;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertChanged(string propertyname = "")
        {
            //?. if not null do...
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private string first;
        /// <summary>
        /// Gets and sets the first name
        /// </summary>
        public string First
        {
            get
            {
                return first;
            }
            set
            {
                first = value;
                NotifyPropertChanged("First");
            }
        }
        private string last;
        /// <summary>
        /// Gets and sets the last name
        /// </summary>
        public string Last
        {
            get
            {
                return last;
            }
            set
            {
                last = value;
                NotifyPropertChanged("Last");
            }
        }

        /// <summary>
        /// Gets the course history
        /// </summary>
        public CourseResult[] CourseHistory
        {
            get
            {
                return courseHistory.ToArray();
            }
        }

        /// <summary>
        /// Gets the student's GPA
        /// </summary>
        public double GPA
        {
            get
            {
                double credits = 0;
                double hours = 0;
                courseHistory.ForEach(course =>
                {
                    switch (course.Grade)
                    {
                        case Grade.A:
                            credits += 4 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.B:
                            credits += 3 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.C:
                            credits += 2 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.D:
                            credits += course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.F:
                        case Grade.XF:
                            hours += course.CreditHours;
                            break;
                    }
                });
                return credits / hours;
            }
        }

        /// <summary>
        /// Constructs a new student instance
        /// </summary>
        /// <param name="first">The first name</param>
        /// <param name="last">The last name</param>
        public Student(string first, string last)
        {
            First = first;
            Last = last;
            courseHistory = new List<CourseResult>();
        }

        public void CourseComplete(string name, uint hours, Grade grade, string semester)
        {
            CourseResult cr = new CourseResult(name, hours, grade, semester);
            courseHistory.Add(cr);
            NotifyPropertChanged("GPA");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Last}, {First}, ({GPA})";
        }

        
    }
}
