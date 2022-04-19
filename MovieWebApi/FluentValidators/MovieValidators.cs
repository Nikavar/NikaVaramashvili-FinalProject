using FluentValidation;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.FluentValidators
{
    public class MovieValidators : AbstractValidator<Movies>
    {
        public MovieValidators()
        {
             RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

            RuleFor(x => x.StartDate)
               .NotEmpty()
               .LessThanOrEqualTo(x => DateTime.Now)
               .WithMessage("Movie can not be start in the Future! Please enter valid date");

            RuleFor(x => x.ProducedYear)
                .NotEmpty()
                .GreaterThan(x => 1888)
                .LessThanOrEqualTo(x => DateTime.Now.Year);

            RuleFor(x => x.PosterUrl)
                    .NotEmpty();
        }           
    }
}
