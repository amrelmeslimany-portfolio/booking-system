using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace api.Config.Filters
{
    public class ValidateFile : ValidationAttribute
    {
        public string[] AllowedFiles { get; set; } =
            ["image/jpeg", "image/png", "image/jpg", "image/webp"];

        public long MaxSizeInBytes { get; set; } = 5000000;

        public override bool IsValid(object? value)
        {
            IFormFile? file = (IFormFile?)value;

            if (file == null)
                return true;

            if (CheckType(file) || file?.Length >= MaxSizeInBytes)
                return false;

            return true;
        }

        private bool CheckType(IFormFile? file)
        {
            return !AllowedFiles!.Contains(file!.ContentType);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageString,
                name,
                string.Join(" | ", AllowedFiles),
                $"{MaxSizeInBytes / (1024 * 1024):0.0}MB"
            );
        }
    }
}
