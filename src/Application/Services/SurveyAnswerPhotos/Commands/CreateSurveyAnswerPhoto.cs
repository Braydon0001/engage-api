using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using Engage.Domain.Entities;
using MediatR;

namespace Engage.Application.Services.SurveyAnswerPhotos.Commands
{
    public class CreateSurveyAnswerPhotoCommand : SurveyAnswerPhotoCommand, IRequest<OperationStatus>
    {
        public bool SaveChanges { get; set; } = true;
    }

    public class CreateSurveyAnswerPhotoCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyAnswerPhotoCommand, OperationStatus>
    {
        public CreateSurveyAnswerPhotoCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateSurveyAnswerPhotoCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateSurveyAnswerPhotoCommand, SurveyAnswerPhoto>(command);            
            _context.SurveyAnswerPhotos.Add(entity);

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
