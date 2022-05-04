using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessToDB;
using BL;
using BL.Models;

using AccessToDB.Exceptions.SlopeExceptions;

using UI.IViews;

namespace UI
{
    public class Presenter
    {
        private uint _userID;
        private PermissionsEnum _permissions;
        private Facade _facade;

        private IViewsFactory _viewsFactory;

        private IExceptionView _exceptionView;
        private IMainView _mainView;

        private IProfileView? _profileView;
        private ISlopeView? _slopeView;

        public Presenter(IViewsFactory viewsFactory, Facade facade)
        {
            _facade = facade;
            _viewsFactory = viewsFactory;
            
            _permissions = PermissionsEnum.UNAUTHORIZED;

            _exceptionView = _viewsFactory.CreateExceptionView();
        }

        public async Task RunAsync()
        {
            _userID = 7777;
            //_userID = await _facade.AddUnauthorizedUserAsync();
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    _exceptionView.ShowException(ex, "");
            //}
            //User tmp = await _facade.AdminGetUserByIDAsync(1, _userID);
            _mainView = _viewsFactory.CreateMainView();

            _mainView.MessageEnabled = false;
            _mainView.CardReadingEnabled = false;
            _mainView.TurnstileEnabled = false;
            _mainView.UserEnabled = false;

            _mainView.ProfileClicked += OnProfileClicked;
            _mainView.SlopeClicked += OnSlopeClicked;
            _mainView.CloseClicked += OnMainCloseClicked;

            _mainView.Open();
        }

        //MAIN
        public async Task OnMainCloseClicked(object sender, EventArgs e)
        {
            if (_permissions != PermissionsEnum.UNAUTHORIZED)
            {
                await LogOutAsync(sender, e);
            }
        }
        private void _changeVisibilityForViews()
        {
            _changeVisibilityForMainView();
            if (_profileView != null)
            {
                _changeVisibilityForProfileView();
            }
            if (_slopeView != null)
            {
                _changeVisibilityForSlopeView();
            }

            void _changeVisibilityForProfileView()
            {
                if (_permissions == PermissionsEnum.UNAUTHORIZED)
                {
                    _profileView.LogOutEnabled = false;

                    _profileView.LogInEnabled = true;
                    _profileView.RegisterEnabled = true;
                }
                else
                {
                    _profileView.LogOutEnabled = true;

                    _profileView.LogInEnabled = false;
                    _profileView.RegisterEnabled = false;
                }
                _profileView.Refresh();
            }

            void _changeVisibilityForMainView()
            {
                if (_permissions == PermissionsEnum.UNAUTHORIZED)
                {
                    _mainView.MessageEnabled = false;
                    _mainView.UserEnabled = false;
                    _mainView.TurnstileEnabled = false;
                    _mainView.CardReadingEnabled = false;
                }
                else if (_permissions == PermissionsEnum.AUTHORIZED)
                {
                    _mainView.MessageEnabled = true;
                    _mainView.UserEnabled = false;
                    _mainView.TurnstileEnabled = false;
                    _mainView.CardReadingEnabled = false;
                }
                else if (_permissions == PermissionsEnum.SKI_PATROL)
                {
                    _mainView.MessageEnabled = true;
                    _mainView.UserEnabled = false;
                    _mainView.TurnstileEnabled = true;
                    _mainView.CardReadingEnabled = false;
                }
                else
                {
                    _mainView.MessageEnabled = true;
                    _mainView.UserEnabled = true;
                    _mainView.TurnstileEnabled = true;
                    _mainView.CardReadingEnabled = true;
                }
                _mainView.Refresh();
            }
            void _changeVisibilityForSlopeView()
            {
                _slopeView.GetInfoEnabled = true;
                _slopeView.GetInfosEnabled = true;

                if (_permissions == PermissionsEnum.UNAUTHORIZED || _permissions == PermissionsEnum.AUTHORIZED)
                {
                    _slopeView.UpdateEnabled = false;
                    _slopeView.AddEnabled = false;
                    _slopeView.DeleteEnabled = false;
                    _slopeView.AddConnectedLiftEnabled = false;
                    _slopeView.DeleteConnectedLiftEnabled = false;
                }
                else 
                {
                    _slopeView.UpdateEnabled = true;
                    _slopeView.AddEnabled = true;
                    _slopeView.DeleteEnabled = true;
                    _slopeView.AddConnectedLiftEnabled = true;
                    _slopeView.DeleteConnectedLiftEnabled = true;
                }
                _slopeView.Refresh();
            }
        }


        // PROFILE
        public void OnProfileClicked(object sender, EventArgs e)
        {
            if (_profileView is not null)
            {
                return;
            }
            _profileView = _viewsFactory.CreateProfileView();
            _profileView.RegisterClicked += RegisterAsync;
            _profileView.LogOutClicked += LogOutAsync;
            _profileView.LogInClicked += LogInAsync;
            _profileView.CloseClicked += OnProfileCloseClicked;
            _changeVisibilityForViews();
            _profileView.Open();
        }

        private void OnProfileCloseClicked(object sender, EventArgs e)
        {
            _profileView = null;
        }

        public async Task RegisterAsync(object sender, EventArgs e)
        {
            string email = _profileView.Email;
            string password = _profileView.Password;
            string stringCardID = _profileView.cardID;
            uint cardID;

            if (string.IsNullOrEmpty(stringCardID))
            {
                cardID = 0;
            }
            else
            {
                try
                {
                    cardID = Convert.ToUInt32(stringCardID);

                }
                catch (Exception ex)
                {
                    _exceptionView.ShowException(ex, "Номер карты должен быть целым числом");
                    return;
                }
            }

            try
            {
                await _facade.RegisterAsync(_userID, cardID, email, password);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Не удалось завершить регистрацию");
                return;
            }

            _permissions = PermissionsEnum.AUTHORIZED;
            _changeVisibilityForViews();
        }

        public async Task LogOutAsync(object sender, EventArgs e)
        {
            await _facade.LogOutAsync(_userID);
            _permissions = PermissionsEnum.UNAUTHORIZED;
            _userID = 7777;
            _changeVisibilityForViews(); 
        }

        public async Task LogInAsync(object sender, EventArgs e)
        {
            string email = _profileView.Email;
            string password = _profileView.Password;
            User user;

            try
            {
                user = await _facade.LogInAsync(_userID, email, password);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Не удалось выполнить вход");
                return;
            }

            _permissions = user.Permissions;
            _userID = user.UserID;
            _changeVisibilityForViews();
        }

        //SLOPE
        public void OnSlopeClicked(object sender, EventArgs e)
        {
            if (_slopeView is not null)
            {
                return;
            }
            _slopeView = _viewsFactory.CreateSlopeView();
            _slopeView.CloseClicked += OnSlopeCloseClicked;
            _slopeView.GetInfoClicked += GetSlopeInfoAsync;
            _slopeView.GetInfosClicked += GetSlopesInfoAsync;
            _slopeView.UpdateClicked += UpdateSlopeAsync;
            _slopeView.AddClicked += AddSlopeAsync;
            _slopeView.DeleteClicked += DeleteSlopeAsync;
            _slopeView.AddConnectedLiftClicked += AddConnectedLiftAsync;
            _slopeView.DeleteConnectedLiftClicked += DeleteConnectedLiftAsync;
            _changeVisibilityForViews();
            _slopeView.Open();
        }

        private void OnSlopeCloseClicked(object sender, EventArgs e)
        {
            _slopeView = null;
        }

        private async Task GetSlopesInfoAsync(object sender, EventArgs e)
        {
            List<Slope> slopes = await _facade.GetSlopesInfoAsync(_userID);
            _slopeView.Slopes = slopes;
        }

        private async Task GetSlopeInfoAsync(object sender, EventArgs e)
        {
            string name = _slopeView.Name;
            try
            {
                Slope slope = await _facade.GetSlopeInfoAsync(_userID, name);
                _slopeView.Slopes = new List<Slope>() { slope };
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
        }

        private async Task UpdateSlopeAsync(object sender, EventArgs e)
        {
            string name = _slopeView.Name;
            bool isOpen;
            try
            {
                isOpen = Convert.ToBoolean(_slopeView.IsOpen);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Для поля \"Открыта\" возможны значения \"True\" или \"False\"");
                return;
            }
            uint difficultyLevel;
            try
            {
                difficultyLevel = Convert.ToUInt32(_slopeView.DifficultyLevel);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Уровень сложности должен быть целым неотрицательным числом");
                return;
            }

            try
            {
                await _facade.UpdateSlopeInfoAsync(_userID, name, isOpen, difficultyLevel);
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            await GetSlopeInfoAsync(sender, e);
        }
        private async Task AddSlopeAsync(object sender, EventArgs e)
        {

        }
        private async Task DeleteSlopeAsync(object sender, EventArgs e)
        {

        }
        private async Task AddConnectedLiftAsync(object sender, EventArgs e)
        {

        }

        private async Task DeleteConnectedLiftAsync(object sender, EventArgs e)
        {

        }
    }
}
