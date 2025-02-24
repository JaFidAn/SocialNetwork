using Application.Core;
using Application.Features.Activities.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditActivityDto ActivityDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.ActivityDto.Id], cancellationToken);

            if (activity is null) return Result<Unit>.Failure("Activity not found", 404);

            mapper.Map(request.ActivityDto, activity);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update activity", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
