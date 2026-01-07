using System;


namespace Cartomatic.Utils.Dto
{
    /// <summary>
    /// IDto utils
    /// </summary>
    public static class IDtoUtils
    {
        /// <summary>
        /// Creates an instance of IDto[TDto]
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public static IDto<TDto> CrateIDtoInstance<TDto>()
            where TDto : class
        {
            if (!(Activator.CreateInstance(typeof(TDto)) is IDto<TDto> inst))
                throw new ArgumentException(
                    "Could not create IDto<TDto> instance. When using the auto DTO feature, TDto object type must implement IDto<TDto>!");

            return inst;
        }
    }
}
