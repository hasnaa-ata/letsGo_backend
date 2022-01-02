
namespace Classes.Utilities
{
    public interface IBaseAccessPermission
    {
        public bool ViewPermission { get; set; }
        public bool AddPermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DetailsPermission { get; set; }
        public bool DeletePermission { get; set; }
        public bool ExportPermission { get; set; }
        public bool ImportPermission { get; set; }

    }
    public class BaseAccessPermission : IBaseAccessPermission
    {
        private bool _ViewPermission = false;
        private bool _AddPermission = false;
        private bool _EditPermission = false;
        private bool _DetailsPermission = false;
        private bool _DeletePermission = false;
        private bool _ExportPermission = false;
        private bool _ImportPermission = false;

        public bool ViewPermission
        {
            get
            {
                return _ViewPermission;
            }

            set
            {
                _ViewPermission = value;
            }
        }
        public bool AddPermission
        {
            get
            {
                return _AddPermission;
            }

            set
            {
                _AddPermission = value;
            }
        }
        public bool EditPermission
        {
            get
            {
                return _EditPermission;
            }

            set
            {
                _EditPermission = value;
            }
        }
        public bool DetailsPermission
        {
            get
            {
                return _DetailsPermission;
            }

            set
            {
                _DetailsPermission = value;
            }
        }
        public bool DeletePermission
        {
            get
            {
                return _DeletePermission;
            }

            set
            {
                _DeletePermission = value;
            }
        }
        public bool ExportPermission
        {
            get
            {
                return _ExportPermission;
            }

            set
            {
                _ExportPermission = value;
            }
        }
        public bool ImportPermission
        {
            get
            {
                return _ImportPermission;
            }

            set
            {
                _ImportPermission = value;
            }
        }
    }

    public class UserAccessPermission : BaseAccessPermission
    {
        private bool _ChangePasswordPermission = false;
        private bool _SignInPermission = false;
        public bool ChangePasswordPermission
        {
            get
            {
                return _ChangePasswordPermission;
            }

            set
            {
                _ChangePasswordPermission = value;
            }
        }
        public bool SignInPermission
        {
            get
            {
                return _SignInPermission;
            }

            set
            {
                _SignInPermission = value;
            }
        }
    }
}