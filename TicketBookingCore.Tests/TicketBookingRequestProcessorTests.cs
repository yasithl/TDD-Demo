using System;
using Domain;
using TicketBookingPersistence;
using Xunit;
using Moq;

namespace TicketBookingCore.Tests;

public class TicketBookingRequestProcessorTests
{
    private readonly TicketBookingRequestProcessor _processor;
    private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMoq;

    public TicketBookingRequestProcessorTests()
    {
        _ticketBookingRepositoryMoq = new Mock<ITicketBookingRepository>();
        _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMoq.Object);
    }

    [Fact]
    public void ShouldReturnTicketBookingResultWithRequestValues()
    {
        var request = new TicketBookingRequest()
        {
            FirstName = "Yasith",
            LastName = "Liyanaarachchi",
            Email = "yasith@gmail.com"
        };

        TicketBookingResponse response = _processor.Book(request);

        Assert.NotNull(response);
        Assert.Equal(request.FirstName, response.FirstName);
        Assert.Equal(request.LastName, response.LastName);
        Assert.Equal(request.Email, response.Email);
    }

    [Fact]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ShouldSaveToDatabase()
    {
        TicketBooking savedTicketBooking = null;

        _ticketBookingRepositoryMoq.Setup(x => x.Save(It.IsAny<TicketBooking>()))
            .Callback<TicketBooking>((ticketBooking) =>
            {
                savedTicketBooking = ticketBooking;
            });

        var request = new TicketBookingRequest()
        {
            FirstName = "Yasith",
            LastName = "Liyanaarachchi",
            Email = "yasith@gmail.com"
        };

        TicketBookingResponse response = _processor.Book(request);

        _ticketBookingRepositoryMoq.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);

        Assert.NotNull(savedTicketBooking);
        Assert.Equal(request.FirstName, savedTicketBooking.FirstName);
        Assert.Equal(request.LastName, savedTicketBooking.LastName);
        Assert.Equal(request.Email, savedTicketBooking.Email);
    }
}