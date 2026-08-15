using FluentValidation;
using System;

namespace CarRent.Application.Common.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Formatul URL-ului este invalid.");
        }
    }
}