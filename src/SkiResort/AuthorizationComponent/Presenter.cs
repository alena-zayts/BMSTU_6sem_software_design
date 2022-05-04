using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessToDB;
using BL;
using BL.Models;

namespace AuthorizationComponent
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

        public Presenter(IViewsFactory viewsFactory, Facade facade)
        {
            _facade = facade;
            _viewsFactory = viewsFactory;
            
            _permissions = PermissionsEnum.UNAUTHORIZED;

            _exceptionView = _viewsFactory.CreateExceptionView();
        }

        public async Task RunAsync()
        {
            _userID = 777;
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

        
        private void _changeVisibilityForViews()
        {
            _changeVisibilityForMainView();
            if (_profileView != null)
            {
                _changeVisibilityForProfileView();
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
            _changeVisibilityForViews();
        }

        public async Task LogInAsync(object sender, EventArgs e)
        {
            string email = _profileView.Email;
            string password = _profileView.Password;

            try
            {
                await _facade.LogInAsync(_userID, email, password);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Не удалось выполнить вход");
                return;
            }

            _permissions = PermissionsEnum.AUTHORIZED;
            _changeVisibilityForViews();
        }
    }
}
