using DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Components {
    public class AmountOfAppointments : ViewComponent {
        private IAppointmentRepo _appointmentRepo;
        public AmountOfAppointments(IAppointmentRepo appointmentRepo) {
            _appointmentRepo = appointmentRepo;
        }
        public IViewComponentResult Invoke() {
            //Old controller, can be repusposed for the amount of appointments
            int amountOfAppointments = _appointmentRepo.Count();
            if (amountOfAppointments == 1) {
                return View("Default", $"Er is {amountOfAppointments} afspraak");
            } else {
                return View("Default", $"Er zijn {amountOfAppointments} afspraken");
            }
        }
    }
}
