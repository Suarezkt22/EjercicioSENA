using MediatR;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries.Handlers;

public class GetAllProgramsQueryHandler(IProgramRepository _programRepository)
    : IRequestHandler<GetAllProgramsQuery, Response<List<GetAllProgramsResponse>>>
{
    public async Task<Response<List<GetAllProgramsResponse>>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
    {
        var programs = await _programRepository.GetAll(cancellationToken); 

        var response = GetCoursesPerProgramResponse(programs);

        return new Response<List<GetAllProgramsResponse>>(response);
    }

    private static List<GetAllProgramsResponse> GetCoursesPerProgramResponse(List<DomainProgram> programs)
    {

        return [.. programs
            .Select(program => new GetAllProgramsResponse
            {
                ProgramId = program.Id,
                Name = program.Name,
            })];
    }
}