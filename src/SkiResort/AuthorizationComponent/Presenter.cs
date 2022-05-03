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

        public async void RunAsync()
        {
            _userID = await _facade.AddUnauthorizedUserAsync();

            _mainView = _viewsFactory.CreateMainView();

            _mainView.MessageEnabled = false;
            _mainView.CardReadingEnabled = false;
            _mainView.TurnstileEnabled = false;
            _mainView.UserEnabled = false;

            _mainView.ProfileClicked += OnProfileClicked;

            _mainView.Open();
        }

        public void OnProfileClicked(object sender, EventArgs e)
        {
            if (_profileView is not null)
            {
                return;
            }
            _profileView = _viewsFactory.CreateProfileView();
            _profileView.RegisterClicked += RegisterAsync;
            _setVisibilityForProfileView();
            _profileView.Open();
        }
        private void _setVisibilityForProfileView()
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

        }

        public async Task RegisterAsync(object sender, EventArgs e)
        {
            string email = _profileView.Email;
            string password = _profileView.Password;
            string stringCardID = _profileView.cardID;
            uint cardID;

            try
            {
                cardID = Convert.ToUInt32(stringCardID);
             
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Номер карты должен быть целым числом");
                return;
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
            _setVisibilityForProfileView();
            _profileView.Refresh();
        }
    }
}
