using Engage.Application.Services.Trainings.Models;

namespace Engage.Application.Services.Trainings.Queries;

public class TrainingVmQuery : GetByIdQuery, IRequest<TrainingVm>
{
}

public class TrainingVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingVmQuery, TrainingVm>
{
    public TrainingVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingVm> Handle(TrainingVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Trainings.Include(e => e.TrainingProvider)
                                             .Include(e => e.TrainingType)
                                             .Include(e => e.EngageRegion)
                                             .Include(e => e.TrainingCategory)
                                             .Include(e => e.EmployeeTrainings)
                                             .Include(e => e.TrainingDuration)
                                             .SingleAsync(e => e.TrainingId == request.Id, cancellationToken);

        var vm = _mapper.Map<Training, TrainingVm>(entity);

        var employees = await _context.EmployeeTrainings.Where(e => e.TrainingId == request.Id && e.Disabled == false)
                                                        .Select(e => new OptionDto(e.EmployeeId, e.Employee.FirstName + " " + e.Employee.LastName + " - " + e.Employee.Code))
                                                        .ToListAsync(cancellationToken);

        if (employees.Count > 0)
        {
            employees = employees.OrderBy(e => e.Name).ToList();
        }

        vm.EmployeeIds = employees;

        var facilitators = await _context.TrainingFacilitators.Where(e => e.TrainingId == request.Id && e.Disabled == false)
                                                  .Select(e => new OptionDto(e.EmployeeId, e.Employee.FirstName + " " + e.Employee.LastName + " - " + e.Employee.Code))
                                                  .ToListAsync(cancellationToken);

        if (facilitators.Count > 0)
        {
            facilitators = facilitators.OrderBy(e => e.Name).ToList();
        }

        vm.FacilitatorIds = facilitators;

        var trainingFiles = await _context.TrainingFiles.Where(e => e.TrainingId == request.Id && e.Disabled == false)
                                                        .ToListAsync(cancellationToken);

        List<JsonFile> registerFiles = new();
        List<JsonFile> invoiceFiles = new();
        List<JsonFile> receiptFiles = new();
        List<JsonFile> accommodationFiles = new();
        List<JsonFile> carhireFiles = new();
        List<JsonFile> cateringFiles = new();
        List<JsonFile> flightsFiles = new();
        List<JsonFile> fuelFiles = new();
        List<JsonFile> stationeryFiles = new();
        List<JsonFile> venueFiles = new();
        List<JsonFile> otherFiles = new();

        if (trainingFiles.Count > 0)
        {
            foreach (var file in trainingFiles)
            {
                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Register)
                {
                    registerFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Invoice)
                {
                    invoiceFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Receipt)
                {
                    receiptFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Accommodation)
                {
                    accommodationFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.CarHire)
                {
                    carhireFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Catering)
                {
                    cateringFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Flights)
                {
                    flightsFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Fuel)
                {
                    fuelFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Stationery)
                {
                    stationeryFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Venue)
                {
                    venueFiles.AddRange(file.Files);
                }

                if (file.TrainingFileTypeId == (int)TrainingFileTypeId.Other)
                {
                    otherFiles.AddRange(file.Files);
                }
            }
        }

        vm.FileRegister = registerFiles;
        vm.FileInvoice = invoiceFiles;
        vm.FileReceipt = receiptFiles;
        vm.FileAccommodationCost = accommodationFiles;
        vm.FileCarHireCost = carhireFiles;
        vm.FileCateringCost = cateringFiles;
        vm.FileFlightsCost = flightsFiles;
        vm.FileFuelCost = fuelFiles;
        vm.FileStationeryCost = stationeryFiles;
        vm.FileVenueCost = venueFiles;
        vm.FileOtherCost = otherFiles;

        return vm;
    }
}
