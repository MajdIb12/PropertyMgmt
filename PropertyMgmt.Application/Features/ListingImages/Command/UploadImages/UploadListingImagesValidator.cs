using FluentValidation;

namespace PropertyMgmt.Application.Features.ListingImages.Command.UploadImages;

public class UploadListingImagesValidator : AbstractValidator<UploadListingImagesCommand>
{
    public UploadListingImagesValidator()
    {
        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("ListingId is required.");

        RuleFor(x => x.Files)
            .NotNull().WithMessage("Files are required.")
            .Must(files => files.Count > 0).WithMessage("At least one file must be uploaded.")
            .Must(x => x.All(file => file.Length < 2 * 1024 * 1024)).WithMessage("Each file must be less than 2MB.")
            .Must(x => x.All(file =>{
                 var extension = Path.GetExtension(file.FileName).ToLower();
                return extension == ".jpg" || extension == ".jpeg" || extension == ".png";}))
                .WithMessage("Invalid file extension. Only .jpg, .jpeg, and .png are allowed.")
            .Must(x => x.All(file => file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/jpg"))
            .WithMessage("Only JPEG, JPG, and PNG formats are allowed.");
    }
}