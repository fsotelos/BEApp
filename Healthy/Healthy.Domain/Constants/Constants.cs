using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Domain.Constants
{
    public class Constants
    {
        public const string UserCreatedSucessful = "Usuario Creado Exitosamente";
        public const string UserEditedSucessful = "Usuario Editado Exitosamente";
        public const string UserDeletedSucessful = "Usuario Eliminado Exitosamente";
        public const string UserNotExist = "El usuario no existe";
        public const string GetUsersCallSucessful = "Usuarios Consultados Existosamente";


        #region UserSpecification Messages Validation
        public const string UserSpecificationUserNullError = "El Usuario es Incorrecto";
        public const string UserSpecificationFirstNameNullEmptyOrWithSpace = "El primer nombre del Usuario es Incorrecto";
        public const string UserSpecificationLastFirstNameNullEmptyOrWithSpace = "El primer apellido del Usuario es Incorrecto";
        public const string UserSpecificationNickNameNullEmptyOrWithSpace = "El Nickname del usuario es incorrecto";
        public const string UserSpecificationEmailError = "Correo Electrónico Inválido";
        public const string UserSpecificationMinBirthDay = "El usuario es menor de 18 años";
        public const string UserSpecificationDuplicatedEmail = "El email ingresado ya existe";
        public const string UserSpecificationDuplicatedNickName = "El NickName ingresado ya existe";
        public const string UserSpecificationNotExistEditingUser = "El usuario que desea editar no existe";
        public const string UserSpecificationInvalidPassword = "El password ingresado no es válido";
        public const string UserSpecificationInvalidPhoneNumber = "El número de celular ingresado no es válido";
        public const int UserSpecificationMinYearsOld = 18;
        #endregion
    }
}
