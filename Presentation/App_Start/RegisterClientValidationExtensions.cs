using DataAnnotationsExtensions.ClientValidation;
using JetBrains.Annotations;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Tcbcsl.Presentation.RegisterClientValidationExtensions), "Start")]

namespace Tcbcsl.Presentation
{
    public static class RegisterClientValidationExtensions
    {
        [UsedImplicitly]
        public static void Start()
        {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
        }
    }
}