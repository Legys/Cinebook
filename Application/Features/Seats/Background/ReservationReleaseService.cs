
using Cinebook.Application.Features.Seats.Command;
using MediatR;

namespace Cinebook.Application.Features.Seats.Background;
public class ReservationReleaseService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                await sender.Send(new ReleaseReservedSeatsCommand(), cancellationToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }
}
