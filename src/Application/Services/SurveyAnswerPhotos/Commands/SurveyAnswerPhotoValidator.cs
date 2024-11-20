using FluentValidation;

namespace Engage.Application.Services.SurveyAnswerPhotos.Commands
{
    public class SurveyAnswerPhotoValidator<T> : AbstractValidator<T> where T : SurveyAnswerPhotoCommand
    {
        public SurveyAnswerPhotoValidator()
        {          
            RuleFor(x => x.SurveyAnswerId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty();
            RuleFor(x => x.Folder).NotEmpty();
        }
    }

    public class CreateEmployeeSurveyPhotoValidator : SurveyAnswerPhotoValidator<CreateSurveyAnswerPhotoCommand>
    {        
    }

    public class UpdateEmployeeSurveyPhotoValidator : SurveyAnswerPhotoValidator<UpdateSurveyAnswerPhotoCommand>
    {
        public UpdateEmployeeSurveyPhotoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }

    public class BatchInsertEmployeeStoreSurveyPhotoValidator : AbstractValidator<BatchCreateSurveyAnswerPhotoCommand>
    {
        public BatchInsertEmployeeStoreSurveyPhotoValidator()
        {
            RuleFor(x => x.SurveyAnswerId).GreaterThan(0).NotEmpty();
        }
    }
}
