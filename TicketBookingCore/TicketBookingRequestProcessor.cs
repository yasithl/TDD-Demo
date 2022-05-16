using Domain;
using TicketBookingPersistence;

namespace TicketBookingCore;
public class TicketBookingRequestProcessor
{
    private readonly ITicketBookingRepository _bookingRepository;
    public TicketBookingRequestProcessor(ITicketBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public TicketBookingResponse Book(TicketBookingRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _bookingRepository.Save(new TicketBooking()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        });

        TicketBookingResponse response = new TicketBookingResponse()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        return response;
    }
}
