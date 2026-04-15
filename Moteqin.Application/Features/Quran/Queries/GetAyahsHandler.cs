using AutoMapper;
using MediatR;
using Moteqin.Domain.Interfaces;

public class GetAyahsHandler : IRequestHandler<GetAyahsQuery, Result<List<AyahDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAyahsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<AyahDto>>> Handle(GetAyahsQuery request, CancellationToken cancellationToken)
    {
        var ayahs = await _unitOfWork.Ayahs
            .FindAsync(x => x.SurahId == request.SurahId);

        var data = _mapper.Map<List<AyahDto>>(ayahs);

        return Result<List<AyahDto>>.Success(data);
    }
}