// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Settings;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CleanArchitecture.Razor.Application.Workflow.Approval.Steps
{
    public class RejectedStep : StepBodyAsync
    {
        private readonly IMailService _mailService;
        private readonly ILogger<RejectedStep> _logger;
        public string WorkId { get; set; }
        public string DocumentName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string From {  get; set; }
        public string Approver { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }
        public RejectedStep(IMailService mailService,
            ILogger<RejectedStep> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            WorkId = context.Workflow.Id;
            Body = $"Your request document has been rejected! DocumentName:{DocumentName}";
            Subject = $"Rejected {DocumentName}";
            var request = new MailRequest();
            request.To = To;
            request.Subject = Subject;
            request.Body = Body;
            await _mailService.SendAsync(request);
            Console.WriteLine($"Your request document has been rejected by {Approver}! DocumentName:{DocumentName}");
            _logger.LogInformation($"Your request document has been rejected by {Approver}! DocumentName:{DocumentName}");
            return ExecutionResult.Next();
        }
    }
}