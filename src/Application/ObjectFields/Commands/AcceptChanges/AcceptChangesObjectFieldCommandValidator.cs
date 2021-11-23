// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.AcceptChanges;

    public class AcceptChangesObjectFieldCommandValidator : AbstractValidator<AcceptChangesObjectFieldsCommand>
    {
        public AcceptChangesObjectFieldCommandValidator()
        {

             RuleFor(v => v.Items)
                  .NotNull()
                 .NotEmpty();
          
             
        }
    }
