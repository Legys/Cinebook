using Cinebook.Domain.Enums;
using Cinebook.Infrastructure.Errors;
using Cinebook.Resources;

namespace Cinebook.Domain.Rules.TickerPrice;

public static class TicketPrice
{
    private static double MapSeatTypeToPriceCoefficient(SeatTypeEnum seatType)
    {
        return seatType switch
        {
            SeatTypeEnum.Front => 0,
            SeatTypeEnum.Regular => 0.2,
            SeatTypeEnum.Lux => 0.5,
            _ => throw new LogicErrorException(ApplicationErrors.LogicErrorException_Message)
        };
    }

    /// <summary>
    /// Calculates ticket price based on base ticket price, movie session start time and seat type.
    /// </summary>
    /// <param name="baseTicketPrice">Base ticket price</param>
    /// <param name="movieSessionStartTime">Movie session start time</param>
    /// <param name="seatType">Seat type</param>
    /// <returns>Ticket price</returns>
    public static double GetTicketPrice(double baseTicketPrice, DateTime movieSessionStartTime, SeatTypeEnum seatType)
    {
        var dayTime = MovieSessionTimeEnum.CreateFrom(movieSessionStartTime);
        var seatTypeCoefficient = MapSeatTypeToPriceCoefficient(seatType);

        if (dayTime is null)
            throw new LogicErrorException(ApplicationErrors.LogicErrorException_Message);

        // Calculate ticket price using base price and proportional coefficients.
        // The formula applies a series of relative scaling factors to the baseTicketPrice.
        // Each coefficient (dayTime.PriceCoefficient, seatTypeCoefficient) starts from 0,
        // representing no additional charge, and scales the price proportionally.
        // For example, a coefficient of 0.2 implies a 20% increase over the base price.

        return baseTicketPrice * (1 + dayTime.PriceCoefficient + seatTypeCoefficient);
    }
}