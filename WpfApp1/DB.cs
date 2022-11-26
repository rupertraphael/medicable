using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Appointment
    {
        private string _firstName;
        private string _lastName;
        private int _day;
        private string _start;
        private string _end;
        private string _doctor;


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        public string Start
        {
            get { return _start; }
            set { _lastName = value; }
        }

        public string End
        {
            get { return _end; }
            set { _lastName = value; }
        }

        public string StartEnd
        {
            get { return _start + " - " + _end; }
        }

        public string FullName
        {
            get { return _firstName + " " + _lastName; }
        }

        public string Doctor
        {
            get { return _doctor; }
        }

        public Appointment(
            string firstname,
            string lastname,
            int day,
            string start,
            string end,
            string doctor)
        {
            _firstName = firstname;
            _lastName = lastname;
            _day = day;
            _start = start;
            _end = end;
            _doctor = doctor;
        }
    }

    // Followed the pattern in: https://stackoverflow.com/a/17779402
    public static class DB
    {

        // Appointments that have already been added to our 'DB'
        private static List<Appointment> _Appointments = new List<Appointment>()
        {
            new Appointment("Rupert", "Amodia", 1, "9:00AM", "9:30AM", "Dr. Chirag"),
            new Appointment("Araiz", "Asad", 1, "10:00AM", "10:30AM", "Dr. Raphael"),
            new Appointment("David", "Smith", 1, "11:00AM", "11:30AM", "Dr. Raphael"),
            new Appointment("John", "Cena", 1, "12:30PM", "1:00PM", "Dr. Raphael"),
            new Appointment("Matthew", "Murdock", 1, "1:00PM", "1:30PM", "Dr. Amr"),
            new Appointment("Jennifer", "Walters", 1, "1:00PM", "1:30PM", "Dr. Chirag"),
            new Appointment("Muhammad", "Mohammed", 1, "2:00PM", "2:30PM", "Dr. Chirag")
        };

        // getter method to retrieve Appointments from our 'DB'
        public static List<Appointment> Appointments
        {
            get
            {
                return (_Appointments);
            }
        }
    }
}
