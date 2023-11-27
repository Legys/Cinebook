using OneOf;

namespace Cinebook.Domain.Rules.TickerPrice;

public class SessionBase(double priceCoefficient)
{
    public double PriceCoefficient { get; } = priceCoefficient;
}

public class MorningSession(double priceCoefficient) : SessionBase(priceCoefficient);

public class AfternoonSession(double priceCoefficient) : SessionBase(priceCoefficient);

public class EveningSession(double priceCoefficient) : SessionBase(priceCoefficient);

[GenerateOneOf]
public partial class MovieSessionTimeEnum : OneOfBase<MorningSession, AfternoonSession, EveningSession>
{
    public double PriceCoefficient => Match(
        morningSession => morningSession.PriceCoefficient,
        afternoonSession => afternoonSession.PriceCoefficient,
        eveningSession => eveningSession.PriceCoefficient
    );

    public static MovieSessionTimeEnum? CreateFrom(DateTime fromTimeUtc)
    {
        var timeRangeUtc = DateTime.Today.ToUniversalTime();
        var morningStartHour = timeRangeUtc.AddHours(10).Hour;
        var morningEndHour = timeRangeUtc.AddHours(12).Hour;
        var afternoonStartHour = timeRangeUtc.AddHours(12).Hour;
        var afternoonEndHour = timeRangeUtc.AddHours(18).Hour;
        var eveningStartHour = timeRangeUtc.AddHours(18).Hour;
        var eveningEndHour = timeRangeUtc.AddHours(22).Hour;

        return fromTimeUtc.Hour switch
        {
            var h when h >= morningStartHour && h < morningEndHour => new MorningSession(0),
            var h when h >= afternoonStartHour && h < afternoonEndHour => new AfternoonSession(0.2),
            var h when h >= eveningStartHour && h < eveningEndHour => new EveningSession(0.35),
            _ => null
        };
    }
}