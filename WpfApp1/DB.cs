﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{

    public class APatient
    {
        private string _patientName;
        private string _patientPhoneNumber;
        private string _patientHealthCareNumber;
        private string _patientDOB;
        private string _patientAddress;
        private string _patientPreferredDoctor;
        private string _patientHealthHistory;
        private string _patientConditions;

        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        public string PatientPhoneNumber
        {
            get { return _patientPhoneNumber; }
            set { _patientPhoneNumber = value; }
        }

        public string PatientHealthCareNumber
        {
            get { return _patientHealthCareNumber; }
            set { _patientHealthCareNumber = value; }
        }

        public string PatientDOB
        {
            get { return _patientDOB; }
            set { _patientDOB = value; }
        }

        public string PatientAddress
        {
            get { return _patientAddress; }
            set { _patientAddress = value; }
        }

        public string PatientPreferredDoctor
        {
            get { return _patientPreferredDoctor; }
            set { _patientPreferredDoctor = value; }
        }

        public string PatientHealthHistory
        {
            get { return _patientHealthHistory; }
            set { _patientHealthHistory = value; }
        }

        public string PatientConditions
        {
            get { return _patientConditions; }
            set { _patientConditions = value; }
        }

        public override string ToString()
        {
            return this.PatientName;
        }

        public APatient(
            string patientName,
            string patientPhoneNumber,
            string patientHealthCareNumber,
            string patientDOB,
            string patientAddress,
            string patientPreferredDoctor,
            string patientHealthHistory,
            string patientConditions)
        {
            _patientName = patientName;
            _patientPhoneNumber = patientPhoneNumber;
            _patientHealthCareNumber = patientHealthCareNumber;
            _patientDOB = patientDOB;
            _patientAddress = patientAddress;
            _patientPreferredDoctor = patientPreferredDoctor;
            _patientHealthHistory = patientHealthHistory;
            _patientConditions = patientConditions;
        }

    }
    public class Appointment
    {
        private string _firstName;
        private string _lastName;
        private string _doctor;
        private DateTime _startDate;


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

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _startDate; }
            set { _startDate = value; }
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
            DateTime startDate,
            string doctor)
        {
            _firstName = firstname;
            _lastName = lastname;
            _startDate = startDate;
            _doctor = doctor;
        }
    }

    // Followed the pattern in: https://stackoverflow.com/a/17779402
    public static class DB
    {

        // Appointments that have already been added to our 'DB'
        private static List<Appointment> _Appointments = new List<Appointment>()
        {
            new Appointment("Rupert", "Amodia", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP"),
            new Appointment("Rupert", "Amodia", DateTime.Parse("2022-12-01 09:30:00"), "Dr. Amr, GP"),
            new Appointment("Amy", "Santiago", DateTime.Parse("2022-12-01 12:00:00"), "Dr. Amr, GP"),
            new Appointment("Charles", "Boyle", DateTime.Parse("2022-10-31 14:00:00"), "Dr. Amr, GP"),
            new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Amr, GP"),
            new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:30:00"), "Dr. Amr, GP"),
            new Appointment("Raymond", "Holt", DateTime.Parse("2022-11-07 11:30:00"), "Dr. Amr, GP"),
            new Appointment("Kevin", "Cozner", DateTime.Parse("2022-11-07 13:30:00"), "Dr. Amr, GP"),
            new Appointment("Araiz", "Asad", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Raphael, GP"),
            new Appointment("Elizabeth Chu", "Asad", DateTime.Parse("2022-11-08 15:00:00"), "Dr. Amr, GP"),
            new Appointment("David", "Smith", DateTime.Parse("2022-11-02 11:30:00"), "Dr. Raphael, GP")
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