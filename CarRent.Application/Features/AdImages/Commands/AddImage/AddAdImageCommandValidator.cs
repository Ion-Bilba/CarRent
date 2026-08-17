using FluentValidation;
using System.IO;
using System.Linq;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public class AddAdImageCommandValidator : AbstractValidator<AddAdImageCommand>
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        public AddAdImageCommandValidator()
        {
            RuleFor(x => x.AdId)
                .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");

            RuleFor(x => x.Request.File)
                .NotNull().WithMessage("Fișierul imagine este obligatoriu.")
                .Must(file => file != null && file.Length > 0)
                .WithMessage("Fișierul încărcat este gol.")
                .Must(file => file != null && file.Length <= MaxFileSizeBytes)
                .WithMessage("Dimensiunea imaginii nu poate depăși 5 MB.")
                .Must(file => file != null && AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
                .WithMessage("Formatul fișierului nu este acceptat. Sunt permise doar extensiile .jpg, .jpeg, .png și .webp.");

            RuleFor(x => x.Request.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinea de afișare nu poate fi negativă.");
        }
    }
}