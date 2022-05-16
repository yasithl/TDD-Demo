using Domain;

namespace TicketBookingPersistence
{
    public interface ITicketBookingRepository
    {
        void Save(TicketBooking ticket);
    }
}