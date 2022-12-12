using System;
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
        List<Appointment> _appointments;

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

        public List<Appointment> Appointments
        {
            get { return _appointments;  }
            set { _appointments = value; }
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

        public APatient(
            string patientName,
            string patientPhoneNumber,
            string patientHealthCareNumber,
            string patientDOB,
            string patientAddress,
            string patientPreferredDoctor,
            string patientHealthHistory,
            string patientConditions, 
            List<Appointment> appointments)
        {
            _patientName = patientName;
            _patientPhoneNumber = patientPhoneNumber;
            _patientHealthCareNumber = patientHealthCareNumber;
            _patientDOB = patientDOB;
            _patientAddress = patientAddress;
            _patientPreferredDoctor = patientPreferredDoctor;
            _patientHealthHistory = patientHealthHistory;
            _patientConditions = patientConditions;
            _appointments = appointments;
        }

        public APatient(
            string patientName)
        {
            _patientName = patientName;
        }

    }
    public class Appointment
    {
        private APatient _patient;
        private Doctor _doctor;
        private DateTime _startDate;
        private string _reason;
        private string _notes;

        public APatient Patient
        {
            get { return _patient; }
            set { _patient = value; }
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
            get { return _patient.PatientName; }
        }

        public Doctor Doctor
        {
            get { return _doctor; }
        }

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        /*
        public Appointment(
            string firstname,
            string lastname,
            DateTime startDate,
            Doctor doctor)
        {
            _patient = new APatient(firstname + " " + lastname);
            _startDate = startDate;
            _doctor = doctor;
            _reason = "";
            _notes = "";
        }

        public Appointment(
            APatient patient,
            DateTime startDate,
            Doctor doctor)
        {
            _patient = patient;
            _startDate = startDate;
            _doctor = doctor;
            _reason = "";
            _notes = "";
        }
        */
        public Appointment(
            APatient patient,
            DateTime startDate,
            Doctor doctor,
            string reason,
            string notes)
        {
            _patient = patient;
            _startDate = startDate;
            _doctor = doctor;
            _reason = reason;
            _notes = notes;
        }
    }

    public class Doctor
    {
        private string _name;
        private string _specialization;

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string DisplayName
        {
            get { return "Dr. " + _name + ", " + _specialization;  }
        }

        public string Specialization
        {
            get { return _specialization; }
            set { _specialization = value; }
        }

        public Doctor(string name, string specialization)
        {
            _name = name;
            _specialization = specialization;
        }

        public bool Equals(Doctor other)
        {
            if (other == null) { return false; }
            if (object.ReferenceEquals(this, other)) { return true; }
            return this.DisplayName == other.DisplayName;
        }


    }

    // Followed the pattern in: https://stackoverflow.com/a/17779402
    public static class DB
    {
        public static List<Doctor> Doctors
        {
            get
            {
                return (_doctors);
            }
        }

        private static List<Doctor> _doctors = new List<Doctor>()
        {
            new Doctor("Rupert Amodia", "GP"),
            new Doctor("Amr Elhefnawy", "GP"),
            new Doctor("Chirag Asrani", "Chiropractor"),
            new Doctor("Raphael Castillo", "GP"),
            new Doctor("Araiz Asad", "Dermatologist")
        };

        private static List<APatient> _aPatients = new List<APatient>()
        {
            new APatient("Scott Turner", "403-555-1430", "13256-1231", "03/02/1983", "73 5 Ave NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Rosy Usher", "403-555-6122", "11661-1209", "07/15/2002", "65 Hills Rd NE Calgary AB", Doctors[1].DisplayName, "", ""),
            new APatient("Linda Walsh", "403-555-1112", "15671-1200", "01/01/2000", "86 1 ST SE Calgary AB", Doctors[2].DisplayName, "", ""),
            new APatient("Albert Zander", "403-555-1430", "13112-1764", "09/22/1967", "123 Martin Crescent NE Calgary AB", Doctors[3].DisplayName, "", ""),
            new APatient("Antony Simmons", "587-412-8666", "16543-1289", "11/19/2005", "432 Panaroma RD NW Calgary AB", Doctors[4].DisplayName, "", ""),
            new APatient("Bruno Simmons", "587-222-8656", "16552-1139", "05/06/1945", "170 Ridge ST SW Calgary AB", "", "", ""),
            new APatient("Alfred Simmons", "403-855-0215", "17442-1577", "10/10/2010", "170 Ridge ST SW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Jake Peralta", "403-123-4561", "17442-1578", "01/01/1977", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Rosa Diaz", "403-123-4572", "17442-1579", "01/02/1977", "2 Brentwood Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Raymond Holt", "403-397-4542", "10322-1579", "04/02/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Kevin Conzer", "403-397-4543", "10582-3401", "09/29/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Amy Santiago", "403-123-4562", "17992-2134", "03/29/1978", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Charles Boyle", "403-569-5421", "14022-9814", "03/30/1974", "42 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),

            new APatient("James Gaudreau", "416-322-2015", "17443-1577", "10/10/2010", "170 Ridge ST SW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Abraham Lloyd", "519-718-1363", "17443-1578", "01/01/1997", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Rosa Diaz", "416-300-2067", "17443-1579", "01/02/1977", "2 Brentwood Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Thomas Austin", "905-807-5164", "10323-1579", "04/02/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Mary Strack", "519-821-2832", "10583-3401", "09/29/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Margaret Theisen", "403-607-1997", "17993-2134", "03/29/1978", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Jeffrey Kelley", "514-217-3364", "14023-9814", "03/30/1974", "42 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),

            new APatient("Calvin Manseau", "902-440-4545", "17444-1577", "10/10/2010", "170 Ridge ST SW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Robert McQuiston", "514-895-8814", "17449-1578", "01/01/1997", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Scott Carroll", "403-309-3050", "17444-1579", "01/02/1977", "2 Brentwood Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Krista Brady", "647-295-2504", "10324-1579", "04/02/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Bryan Osborn", "514-942-5662", "10584-3401", "09/29/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("James Deshotel", "416-630-0329", "17994-2134", "03/29/1978", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Bobbie Weakley", "250-231-0174", "14024-9814", "03/30/1974", "42 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),

            new APatient("Arthur Pilgrim", "403-863-5802", "17445-1577", "10/10/2010", "170 Ridge ST SW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Mary Castillo", "514-940-2896", "17445-1578", "01/01/1997", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Maria Durr", "604-607-3769", "17442-1579", "01/02/1977", "2 Brentwood Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Marion McDowell", "250-372-1807", "10325-1579", "04/02/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Katia McNally", "416-292-9467", "10585-3401", "09/29/1957", "84 Bearspaw Circle NW Calgary AB", "", "", ""),
            new APatient("Ross Mason", "416-338-218", "17995-2134", "03/29/1978", "170 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
            new APatient("Andrew Labrie", "416-352-0750", "14025-9814", "03/30/1974", "42 Dalhousie Drive NW Calgary AB", Doctors[0].DisplayName, "", ""),
        };

        public static List<APatient> APatients
        {
            get
            {
                return (_aPatients);
            }
        }

        // Appointments that have already been added to our 'DB'
        private static List<Appointment> _Appointments = new List<Appointment>()
        {
            // https://uhs.princeton.edu/health-resources/common-illnesses



            new Appointment(APatients[0], DateTime.Parse("2022-12-01 09:00:00"), Doctors[0], "Flu-like Symptoms", ""),
            new Appointment(APatients[2], DateTime.Parse("2022-12-01 09:30:00"), Doctors[2], "Nausea", ""),
            new Appointment(APatients[1], DateTime.Parse("2022-12-01 09:00:00"), Doctors[3], "Muscle/Joint Pain", ""),
            new Appointment(APatients[1], DateTime.Parse("2022-12-01 09:30:00"), Doctors[3], "Muscle/Joint Pain", ""),
            new Appointment(APatients[3], DateTime.Parse("2022-12-02 10:30:00"), Doctors[4], "Stomach Problems", ""),
            new Appointment(APatients[4], DateTime.Parse("2022-12-02 10:30:00"), Doctors[1], "Muscle/Joint Pain", ""),
            new Appointment(APatients[5], DateTime.Parse("2022-12-02 11:30:00"), Doctors[1], "Muscle/Joint Pain", ""),
            new Appointment(APatients[6], DateTime.Parse("2022-12-02 13:00:00"), Doctors[2], "Other", "Feeling sleepy all the time"),
            new Appointment(APatients[7], DateTime.Parse("2022-12-03 13:00:00"), Doctors[4], "Other", "Family Doctor Meet and Greet"),
            new Appointment(APatients[8], DateTime.Parse("2022-12-03 13:30:00"), Doctors[3], "Other", "Frequent nose bleeds"),
            new Appointment(APatients[9], DateTime.Parse("2022-12-03 14:30:00"), Doctors[0], "Other", "Heavy Snoring"),
            new Appointment(APatients[10], DateTime.Parse("2022-12-03 14:00:00"), Doctors[1], "Flu-like Symptoms", ""),
            new Appointment(APatients[11], DateTime.Parse("2022-12-04 14:00:00"), Doctors[2], "Nausea", ""),
            new Appointment(APatients[12], DateTime.Parse("2022-12-04 14:30:00"), Doctors[2], "Stomach Problems", ""),

            new Appointment(APatients[13], DateTime.Parse("2022-11-30 09:00:00"), Doctors[0], "Flu-like Symptoms", ""),
            new Appointment(APatients[14], DateTime.Parse("2022-11-30 09:30:00"), Doctors[1], "Nausea", ""),
            new Appointment(APatients[15], DateTime.Parse("2022-11-30 09:00:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[16], DateTime.Parse("2022-11-29 09:30:00"), Doctors[3], "Muscle/Joint Pain", ""),
            new Appointment(APatients[17], DateTime.Parse("2022-11-29 10:30:00"), Doctors[0], "Stomach Problems", ""),
            new Appointment(APatients[18], DateTime.Parse("2022-11-29 10:30:00"), Doctors[4], "Muscle/Joint Pain", ""),
            new Appointment(APatients[19], DateTime.Parse("2022-11-28 11:30:00"), Doctors[3], "Muscle/Joint Pain", ""),

            new Appointment(APatients[20], DateTime.Parse("2022-11-28 09:00:00"), Doctors[0], "Flu-like Symptoms", ""),
            new Appointment(APatients[21], DateTime.Parse("2022-11-28 09:30:00"), Doctors[1], "Nausea", ""),
            new Appointment(APatients[22], DateTime.Parse("2022-11-27 09:00:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[23], DateTime.Parse("2022-11-27 09:30:00"), Doctors[3], "Muscle/Joint Pain", ""),
            new Appointment(APatients[24], DateTime.Parse("2022-11-27 10:30:00"), Doctors[0], "Stomach Problems", ""),
            new Appointment(APatients[25], DateTime.Parse("2022-11-26 10:30:00"), Doctors[4], "Muscle/Joint Pain", ""),
            new Appointment(APatients[26], DateTime.Parse("2022-11-26 11:30:00"), Doctors[3], "Muscle/Joint Pain", ""),

            new Appointment(APatients[27], DateTime.Parse("2022-12-04 09:00:00"), Doctors[3], "Flu-like Symptoms", ""),
            new Appointment(APatients[28], DateTime.Parse("2022-12-05 09:30:00"), Doctors[1], "Nausea", ""),
            new Appointment(APatients[29], DateTime.Parse("2022-12-05 09:00:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[30], DateTime.Parse("2022-12-05 09:30:00"), Doctors[4], "Muscle/Joint Pain", ""),
            new Appointment(APatients[31], DateTime.Parse("2022-12-06 10:30:00"), Doctors[0], "Stomach Problems", ""),
            new Appointment(APatients[32], DateTime.Parse("2022-12-06 10:30:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[33], DateTime.Parse("2022-12-06 11:30:00"), Doctors[2], "Muscle/Joint Pain", ""),


           /*
            new Appointment(APatients[34], DateTime.Parse("2022-12-07 09:00:00"), Doctors[3], "Flu-like Symptoms", ""),
            new Appointment(APatients[35], DateTime.Parse("2022-12-07 09:30:00"), Doctors[1], "Nausea", ""),
            new Appointment(APatients[36], DateTime.Parse("2022-12-07 09:00:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[37], DateTime.Parse("2022-12-07 09:30:00"), Doctors[4], "Muscle/Joint Pain", ""),
            new Appointment(APatients[38], DateTime.Parse("2022-12-06 10:30:00"), Doctors[0], "Stomach Problems", ""),
            new Appointment(APatients[39], DateTime.Parse("2022-12-05 10:30:00"), Doctors[2], "Muscle/Joint Pain", ""),
            new Appointment(APatients[40], DateTime.Parse("2022-12-03 11:30:00"), Doctors[2], "Muscle/Joint Pain", ""),
           */
        };

        // getter method to retrieve Appointments from our 'DB'
        public static List<Appointment> Appointments
        {
            get
            {
                return (_Appointments);
            }
        }

        /*private static List<Appointment> scottAppointments = new List<Appointment>()
        {
            new Appointment("Scott", "Turner", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };

        private static List<Appointment> rosyAppointments = new List<Appointment>()
        {
            new Appointment("Rosy", "Usher", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };
        private static List<Appointment> lindaAppointments = new List<Appointment>()
        {
            new Appointment("Linda", "Walsh", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };
        private static List<Appointment> albertAppointments = new List<Appointment>()
        {
            new Appointment("Albert", "Zander", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };
        private static List<Appointment> antonyAppointments = new List<Appointment>()
        {
            new Appointment("Antony", "Simmons", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };
        private static List<Appointment> brunoAppointments = new List<Appointment>()
        {
            new Appointment("Bruno", "Simmons", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };
        private static List<Appointment> alfredAppointments = new List<Appointment>()
        {
            new Appointment("Alfred", "Simmons", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP")
        };*/

        /*private static List<APatient> _aPatients = new List<APatient>()
        {
            new APatient("Scott Turner", "403-555-1430", "13256-1231", "03/02/1983", "73 5 Ave NW Calgary AB", "Dr Rupert", "", "", scottAppointments),
            new APatient("Rosy Usher", "403-555-6122", "11661-1209", "07/15/2002", "65 Hills Rd NE Calgary AB", "Dr Amr", "", "", rosyAppointments),
            new APatient("Linda Walsh", "403-555-1112", "15671-1200", "01/01/2000", "86 1 ST SE Calgary AB", "Dr Chirag", "", "", lindaAppointments),
            new APatient("Albert Zander", "403-555-1430", "13112-1764", "09/22/1967", "123 Martin Crescent NE Calgary AB", "Dr Raphael", "", "", albertAppointments),
            new APatient("Antony Simmons", "587-412-8666", "16543-1289", "11/19/2005", "432 Panaroma RD NW Calgary AB", "Dr Araiz", "", "", antonyAppointments),
            new APatient("Bruno Simmons", "587-222-8656", "16552-1139", "05/06/1945", "170 Ridge ST SW Calgary AB", "", "", "", brunoAppointments),
            new APatient("Alfred Simmons", "403-855-0215", "17442-1577", "10/10/2010", "170 Ridge ST SW Calgary AB", "Dr Rupert", "", "", alfredAppointments)
        };*/

        /*public static List<APatient> APatients
        {
            get
            {
                return (_aPatients); 
            }
        }*/
    }
}
