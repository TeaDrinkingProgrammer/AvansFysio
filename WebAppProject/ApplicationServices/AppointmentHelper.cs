using FluentDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class AppointmentHelper {
        public static bool IsNotDoubleAppointmentCheck(DateRange dateRange, IEnumerable<Appointment> therapistAppointments, IEnumerable<Appointment> patientAppointments) {
            if (therapistAppointments != null) {
                if (!(therapistAppointments.Any())) {
                    foreach (var appointment in therapistAppointments) {
                        if (appointment.DateRange.Includes(dateRange)) {
                            return false;
                        }
                    }
                }
            }
            if (patientAppointments != null) {
                if (patientAppointments.Any()) {
                    foreach (var appointment in patientAppointments) {
                        if (appointment.DateRange.Includes(dateRange)) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool IsAppointmentWithinLimit(DateRange dateRange, IEnumerable<Appointment> patientAppointments, int sessionLimit) {
            List<Appointment> appointmentsInWeek = new();
            DateTime previousMonday = dateRange.Start.DayOfWeek == DayOfWeek.Monday ? dateRange.Start : dateRange.Start.Previous(DayOfWeek.Monday);
            DateTime nextSunday = dateRange.Start.DayOfWeek == DayOfWeek.Sunday ? dateRange.Start : dateRange.Start.Next(DayOfWeek.Sunday);
            DateRange weekToCheck = new DateRange(previousMonday, nextSunday);
            foreach (var appointment in patientAppointments) {
                if (weekToCheck.Includes(appointment.DateRange.Start)) {
                    appointmentsInWeek.Add(appointment);
                }
            }
            return appointmentsInWeek.Count + 1 <= sessionLimit;
        }
        public static Tuple<bool, string, string> Plan(Employee employee, DateRange dateRange, PatientFile patientFile, IEnumerable<Appointment> patientAppointments, int maximumAmountOfSessions, TimeSpan maximumSessionLength) {
            if (patientFile.DateOfAdmission <= dateRange.Start) {
                if (DateTime.Now < dateRange.Start) {
                    if (employee.IsAvailable(dateRange)) {
                        if (IsNotDoubleAppointmentCheck(dateRange, employee.Appointments, patientAppointments)) {
                            if (IsAppointmentWithinLimit(dateRange, patientAppointments, maximumAmountOfSessions)) {
                                if (dateRange.ToTimeSpan() <= maximumSessionLength) {
                                    return new(true, "", "");
                                } else {
                                    return new(false, "StartTime", $"Uw afspraak is langer dan de maximumlengte van {maximumSessionLength}");
                                }
                            } else {
                                return new(false, "Date", $"Afspraak overschrijdt {maximumAmountOfSessions} behandelingen in een week");
                            }
                        } else {
                            return new(false, "StartTime", "U of uw behandelaar heeft op deze tijd al een afspraak");
                        }
                    } else {
                        return new(false, "StartTime", "De behandelaar is op deze tijd of dag niet beschikbaar");
                    }
                } else {
                    return new(false, "Date", "De afspraak moet in de toekomst liggen");
                }
            } else {
                return new(false, "Date", "De datum van de afspraak ligt voor de aanmelddatum");
            }
        }
    }
}
