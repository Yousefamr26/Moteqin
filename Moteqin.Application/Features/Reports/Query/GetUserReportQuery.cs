using MediatR;
using Moteqin.Domain.Common;

public class GetUserReportQuery : IRequest<Result<ReportDto>>
{
    public string Type { get; set; } // Weekly / Monthly
}