using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Delete
{
    public class DeleteInvoiceDetailCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedInvoiceDetailsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteInvoiceDetailCommandHandler : 
                 IRequestHandler<DeleteInvoiceDetailCommand, Result>,
                 IRequestHandler<DeleteCheckedInvoiceDetailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteInvoiceDetailCommandHandler> _localizer;
        public DeleteInvoiceDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteInvoiceDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
        
           var item = await _context.InvoiceDetails.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.InvoiceDetails.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedInvoiceDetailsCommand request, CancellationToken cancellationToken)
        {
        
           var items = await _context.InvoiceDetails.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.InvoiceDetails.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}