using AutoMapper;
using MediatR;
using Moteqin.Domain.Interfaces;

public class GetSurahsHandler : IRequestHandler<GetSurahsQuery, Result<List<SurahDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSurahsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<SurahDto>>> Handle(GetSurahsQuery request, CancellationToken cancellationToken)
    {
        var surahs = await _unitOfWork.Surahs.GetAllSurahsAsync();

        var data = _mapper.Map<List<SurahDto>>(surahs);

        return Result<List<SurahDto>>.Success(data);
    }
}