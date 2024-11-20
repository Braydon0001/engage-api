using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Exceptions;
using Engage.Application.Services.Shared.Commands;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Engage.Application.Models;
using Engage.Application.Interfaces;

namespace Engage.Application.Services.SurveyAnswerPhotos.Commands
{
    public class UpdateSurveyAnswerPhotoCommand : SurveyAnswerPhotoCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public bool SaveChanges { get; set; } = true;
    }

    public class UpdateSurveyAnswerPhotoCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyAnswerPhotoCommand, OperationStatus>
    {
        public UpdateSurveyAnswerPhotoCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(UpdateSurveyAnswerPhotoCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.SurveyAnswerPhotos
                 .FirstOrDefaultAsync(x => x.SurveyAnswerPhotoId == command.Id);

            if (entity == null)
                throw new NotFoundException(nameof(SurveyAnswerPhoto), command.Id);

            _mapper.Map(command, entity);

            if (command.SaveChanges)
            {
                var opStatus = await _context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.SurveyAnswerPhotoId;
                return opStatus;
            }
            return new OperationStatus
            {
                Status = true
            };
        }
    }
}
