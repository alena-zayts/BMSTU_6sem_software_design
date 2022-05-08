using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessToDB;
using BL;
using BL.Models;

using AccessToDB.Exceptions.SlopeExceptions;
using AccessToDB.Exceptions.LiftExceptions;
using AccessToDB.Exceptions.LiftSlopeExceptions;
using AccessToDB.Exceptions.UserExceptions;
using AccessToDB.Exceptions.MessageExceptions;
using AccessToDB.Exceptions.CardReadingExceptions;
using AccessToDB.Exceptions.CardExceptions;
using BL.Exceptions.UserExceptions;
using BL.Exceptions.MessageExceptions;

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
        private ILiftView? _liftView;
        private IMessageView? _messageView;
        private ITurnstileView? _turnstileView;
        private ICardReadingView? _cardReadingView;
        private IUserView? _userView;

        public Presenter(IViewsFactory viewsFactory, Facade facade)
        {
            _facade = facade;
            _viewsFactory = viewsFactory;

            _permissions = PermissionsEnum.UNAUTHORIZED;

            _exceptionView = _viewsFactory.CreateExceptionView();
        }

        public async Task RunAsync()
        {
            //CHANGEIT

            //unauthorized
            //_userID = 7777;
            //_permissions = PermissionsEnum.UNAUTHORIZED;

            //admin q q
            _userID = 1;
            _permissions = PermissionsEnum.ADMIN;
            //skipatrol ski_patrol_email9 ski_patrol_password9
            //_userID = 1511;
            //_permissions = PermissionsEnum.SKI_PATROL;
            //authorized authorized_email0 authorized_password0
            //_userID = 1002;
            //_permissions = PermissionsEnum.AUTHORIZED;

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
            _mainView.LiftClicked += OnLiftClicked;
            _mainView.MessageClicked += OnMessageClicked;
            _mainView.CardReadingClicked += OnCardReadingClicked;
            _mainView.CloseClicked += OnMainCloseClicked;
            _changeVisibilityForViews();
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
            if (_liftView != null)
            {
                _changeVisibilityForLiftView();
            }
            if (_messageView != null)
            {
                _changeVisibilityForMessageView();
            }


            void _changeVisibilityForProfileView()
            {
                switch (_permissions)
                {
                    case PermissionsEnum.UNAUTHORIZED:
                        _profileView.LogOutEnabled = false;
                        _profileView.LogInEnabled = true;
                        _profileView.RegisterEnabled = true;
                        break;
                    default:
                        _profileView.LogOutEnabled = true;
                        _profileView.LogInEnabled = false;
                        _profileView.RegisterEnabled = false;
                        break;
                }
                _profileView.Refresh();
            }

            void _changeVisibilityForMainView()
            {
                switch (_permissions)
                {
                    case PermissionsEnum.UNAUTHORIZED:
                        _mainView.MessageEnabled = false;
                        _mainView.UserEnabled = false;
                        _mainView.TurnstileEnabled = false;
                        _mainView.CardReadingEnabled = false;
                        break;
                    case PermissionsEnum.AUTHORIZED:
                        _mainView.MessageEnabled = true;
                        _mainView.UserEnabled = false;
                        _mainView.TurnstileEnabled = false;
                        _mainView.CardReadingEnabled = false;
                        break;
                    case PermissionsEnum.SKI_PATROL:
                        _mainView.MessageEnabled = true;
                        _mainView.UserEnabled = false;
                        _mainView.TurnstileEnabled = true;
                        _mainView.CardReadingEnabled = false;
                        break;
                    default:
                        _mainView.MessageEnabled = true;
                        _mainView.UserEnabled = true;
                        _mainView.TurnstileEnabled = true;
                        _mainView.CardReadingEnabled = true;
                        break;
                }
                _mainView.Refresh();
            }
            void _changeVisibilityForSlopeView()
            {
                _slopeView.GetInfoEnabled = true;
                _slopeView.GetInfosEnabled = true;

                switch (_permissions)
                {
                    case PermissionsEnum.UNAUTHORIZED:
                    case PermissionsEnum.AUTHORIZED:
                        _slopeView.UpdateEnabled = false;
                        _slopeView.AddEnabled = false;
                        _slopeView.DeleteEnabled = false;
                        _slopeView.AddConnectedLiftEnabled = false;
                        _slopeView.DeleteConnectedLiftEnabled = false;
                        break;
                    case PermissionsEnum.SKI_PATROL:
                        _slopeView.UpdateEnabled = true;
                        _slopeView.AddEnabled = false;
                        _slopeView.DeleteEnabled = false;
                        _slopeView.AddConnectedLiftEnabled = false;
                        _slopeView.DeleteConnectedLiftEnabled = false;
                        break;
                    default:
                        _slopeView.UpdateEnabled = true;
                        _slopeView.AddEnabled = true;
                        _slopeView.DeleteEnabled = true;
                        _slopeView.AddConnectedLiftEnabled = true;
                        _slopeView.DeleteConnectedLiftEnabled = true;
                        break;
                }
                _slopeView.Refresh();
            }

            void _changeVisibilityForLiftView()
            {
                _liftView.GetInfoEnabled = true;
                _liftView.GetInfosEnabled = true;

                switch (_permissions)
                {
                    case PermissionsEnum.UNAUTHORIZED:
                    case PermissionsEnum.AUTHORIZED:
                        _liftView.UpdateEnabled = false;
                        _liftView.AddEnabled = false;
                        _liftView.DeleteEnabled = false;
                        _liftView.AddConnectedSlopeEnabled = false;
                        _liftView.DeleteConnectedSlopeEnabled = false;
                        break;
                    case PermissionsEnum.SKI_PATROL:
                        _liftView.UpdateEnabled = true;
                        _liftView.AddEnabled = false;
                        _liftView.DeleteEnabled = false;
                        _liftView.AddConnectedSlopeEnabled = false;
                        _liftView.DeleteConnectedSlopeEnabled = false;
                        break;
                    default:
                        _liftView.UpdateEnabled = true;
                        _liftView.AddEnabled = true;
                        _liftView.DeleteEnabled = true;
                        _liftView.AddConnectedSlopeEnabled = true;
                        _liftView.DeleteConnectedSlopeEnabled = true;
                        break;
                }
                _liftView.Refresh();
            }

            void _changeVisibilityForMessageView()
            {
                _messageView.Messages = new List<BL.Models.Message> { };
                switch (_permissions)
                {
                    case PermissionsEnum.UNAUTHORIZED:
                        _messageView.GetMessageEnabled = false;
                        _messageView.GetMessagesEnabled = false;
                        _messageView.SendEnabled = false;
                        _messageView.UpdateEnabled = false;
                        _messageView.DeleteEnabled = false;
                        _messageView.MarkCheckedEnabled = false;
                        break;
                    case PermissionsEnum.AUTHORIZED:
                        _messageView.GetMessageEnabled = false;
                        _messageView.GetMessagesEnabled = false;
                        _messageView.SendEnabled = true;
                        _messageView.UpdateEnabled = false;
                        _messageView.DeleteEnabled = false;
                        _messageView.MarkCheckedEnabled = false;
                        break;
                    case PermissionsEnum.SKI_PATROL:
                        _messageView.GetMessageEnabled = true;
                        _messageView.GetMessagesEnabled = true;
                        _messageView.SendEnabled = false;
                        _messageView.UpdateEnabled = false;
                        _messageView.DeleteEnabled = false;
                        _messageView.MarkCheckedEnabled = true;
                        break;
                    default:
                        _messageView.GetMessageEnabled = true;
                        _messageView.GetMessagesEnabled = true;
                        _messageView.SendEnabled = true;
                        _messageView.UpdateEnabled = true;
                        _messageView.DeleteEnabled = true;
                        _messageView.MarkCheckedEnabled = false;
                        break;
                }
                _messageView.Refresh();
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
            //await _facade.LogOutAsync(_userID);
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
            catch (UserNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Пользователь с таким email не найден");
                return;
            }
            catch (UserAuthorizationException ex)
            {
                _exceptionView.ShowException(ex, "Неверный пароль");
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
                await _facade.AdminAddAutoIncrementSlopeAsync(_userID, name, isOpen, difficultyLevel);
            }
            catch (SlopeAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем уже существует");
            }
            await GetSlopeInfoAsync(sender, e);
        }
        private async Task DeleteSlopeAsync(object sender, EventArgs e)
        {
            string name = _slopeView.Name;
            try
            {

                await _facade.AdminDeleteSlopeAsync(_userID, name);
            }
            catch (SlopeDeleteException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            await GetSlopesInfoAsync(sender, e);
        }
        private async Task AddConnectedLiftAsync(object sender, EventArgs e)
        {
            string slopeName = _slopeView.Name;
            string liftName = _slopeView.LiftName;

            try
            {
                await _facade.AdminAddAutoIncrementLiftSlopeAsync(_userID, liftName, slopeName);
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
            catch (LiftSlopeAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Данный спуск уже связан с указанным подъемником");
            }
            await GetSlopeInfoAsync(sender, e);
        }

        private async Task DeleteConnectedLiftAsync(object sender, EventArgs e)
        {
            string slopeName = _slopeView.Name;
            string liftName = _slopeView.LiftName;

            try
            {
                await _facade.AdminDeleteLiftSlopeAsync(_userID, liftName, slopeName);
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
            catch (LiftSlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Данный спуск не связан с указанным подъемником");
            }
            await GetSlopeInfoAsync(sender, e);
        }




















        //LIFT
        public void OnLiftClicked(object sender, EventArgs e)
        {
            if (_liftView is not null)
            {
                return;
            }
            _liftView = _viewsFactory.CreateLiftView();
            _liftView.CloseClicked += OnLiftCloseClicked;
            _liftView.GetInfoClicked += GetLiftInfoAsync;
            _liftView.GetInfosClicked += GetLiftsInfoAsync;
            _liftView.UpdateClicked += UpdateLiftAsync;
            _liftView.AddClicked += AddLiftAsync;
            _liftView.DeleteClicked += DeleteLiftAsync;
            _liftView.AddConnectedSlopeClicked += AddConnectedSlopeAsync;
            _liftView.DeleteConnectedSlopeClicked += DeleteConnectedSlopeAsync;
            _changeVisibilityForViews();
            _liftView.Open();
        }

        private void OnLiftCloseClicked(object sender, EventArgs e)
        {
            _liftView = null;
        }

        private async Task GetLiftsInfoAsync(object sender, EventArgs e)
        {
            List<Lift> lifts = await _facade.GetLiftsInfoAsync(_userID);
            _liftView.Lifts = lifts;
        }

        private async Task GetLiftInfoAsync(object sender, EventArgs e)
        {
            string name = _liftView.Name;
            try
            {
                Lift lift = await _facade.GetLiftInfoAsync(_userID, name);
                _liftView.Lifts = new List<Lift>() { lift };
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
        }

        private async Task UpdateLiftAsync(object sender, EventArgs e)
        {
            string name = _liftView.Name;
            bool isOpen;
            try
            {
                isOpen = Convert.ToBoolean(_liftView.IsOpen);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Для поля \"Открыт\" возможны значения \"True\" или \"False\"");
                return;
            }
            uint seatsAmount, liftingTime;
            try
            {
                seatsAmount = Convert.ToUInt32(_liftView.SeatsAmount);
                liftingTime = Convert.ToUInt32(_liftView.LiftingTime);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Количество мест и время подъема должны быть целыми неотрицательными числами");
                return;
            }

            try
            {
                await _facade.UpdateLiftInfoAsync(_userID, name, isOpen, seatsAmount, liftingTime);
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
            await GetLiftInfoAsync(sender, e);
        }
        private async Task AddLiftAsync(object sender, EventArgs e)
        {
            string name = _liftView.Name;
            bool isOpen;
            try
            {
                isOpen = Convert.ToBoolean(_liftView.IsOpen);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Для поля \"Открыта\" возможны значения \"True\" или \"False\"");
                return;
            }
            uint seatsAmount, liftingTime;
            try
            {
                seatsAmount = Convert.ToUInt32(_liftView.SeatsAmount);
                liftingTime = Convert.ToUInt32(_liftView.LiftingTime);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "Количество мест и время подъема должны быть целыми неотрицательными числами");
                return;
            }

            try
            {
                await _facade.AdminAddAutoIncrementLiftAsync(_userID, name, isOpen, seatsAmount, liftingTime);
            }
            catch (LiftAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем уже существует");
            }
            await GetLiftInfoAsync(sender, e);
        }

        private async Task DeleteLiftAsync(object sender, EventArgs e)
        {
            string name = _liftView.Name;
            try
            {

                await _facade.AdminDeleteLiftAsync(_userID, name);
            }
            catch (LiftDeleteException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден или найдены связанные с ним турникеты");
            }
            await GetLiftsInfoAsync(sender, e);
        }
        private async Task AddConnectedSlopeAsync(object sender, EventArgs e)
        {
            string liftName = _liftView.Name;
            string slopeName = _liftView.SlopeName;

            try
            {
                await _facade.AdminAddAutoIncrementLiftSlopeAsync(_userID, liftName, slopeName);
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
            catch (LiftSlopeAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Данный подъемник уже связан с указанным спуском");
            }
            await GetLiftInfoAsync(sender, e);
        }

        private async Task DeleteConnectedSlopeAsync(object sender, EventArgs e)
        {
            string liftName = _liftView.Name;
            string slopeName = _liftView.SlopeName;

            try
            {
                await _facade.AdminDeleteLiftSlopeAsync(_userID, liftName, slopeName);
            }
            catch (SlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Спуск с таким именем не найден");
            }
            catch (LiftNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Подъемник с таким именем не найден");
            }
            catch (LiftSlopeNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Данный подъемник не связан с указанным спуском");
            }
            await GetLiftInfoAsync(sender, e);
        }









        // Message
        public void OnMessageClicked(object sender, EventArgs e)
        {
            if (_messageView is not null)
            {
                return;
            }
            _messageView = _viewsFactory.CreateMessageView();

            _messageView.GetMessageClicked += GetMessageAsync;
            _messageView.GetMessagesClicked += GetMessagesAsync;
            _messageView.MarkCheckedClicked += MarkMessageCheckedAsync;
            _messageView.SendClicked += SendMessageAsync;
            _messageView.DeleteClicked += DeleteMessageAsync;
            _messageView.UpdateClicked += UpdateMessageAsync;
            _messageView.CloseClicked += OnMessageCloseClicked;


            _changeVisibilityForViews();
            _messageView.Open();
    }

        private void OnMessageCloseClicked(object sender, EventArgs e)
        {
            _messageView = null;
        }

        private async Task GetMessagesAsync(object sender, EventArgs e)
        {
            List<BL.Models.Message> messages = await _facade.GetMessagesAsync(_userID);
            _messageView.Messages = messages;
        }

        private async Task GetMessageAsync(object sender, EventArgs e)
        {
            string stringMessageID = _messageView.MessageID;
            uint messageID;
            try
            {
                messageID = Convert.ToUInt32(stringMessageID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID сообщения должно быть целым неотрицательным числом");
                return;
            }

            try
            {
                BL.Models.Message message = await _facade.GetMessageAsync(_userID, messageID);
                _messageView.Messages = new List<BL.Models.Message>() { message };
            }
            catch (MessageNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Сообщение с таким ID не найдено");
            }
        }

        private async Task UpdateMessageAsync(object sender, EventArgs e)
        {
            string text = _messageView.MessageText;
            string stringMessageID = _messageView.MessageID;
            string stringCheckedByID = _messageView.CheckedByID;
            string stringSenderID = _messageView.SenderID;
            uint messageID, checkedByID, senderID;
            try
            {
                messageID = Convert.ToUInt32(stringMessageID);
                checkedByID = Convert.ToUInt32(stringCheckedByID);
                senderID = Convert.ToUInt32(stringSenderID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID сообщения, отправителя, прочитавшего должны быть целыми неотрицательными числами");
                return;
            }

            try
            {
                await _facade.AdminUpdateMessageAsync(_userID, messageID, senderID, checkedByID, text);
            }
            catch (MessageNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Сообщение с таким ID не найдено");
            }
            await GetMessageAsync(sender, e);
        }
        private async Task SendMessageAsync(object sender, EventArgs e)
        {
            string text = _messageView.MessageText;

            try
            {
                uint messageID = await _facade.SendMessageAsync(_userID, text);
                _messageView.MessageID = Convert.ToString(messageID);
                await GetMessageAsync(sender, e);
            }
            catch (MessageAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Не удалось отправить сообщение");
            }
        }

        private async Task DeleteMessageAsync(object sender, EventArgs e)
        {
            string stringMessageID = _messageView.MessageID;
            uint messageID;
            try
            {
                messageID = Convert.ToUInt32(stringMessageID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID сообщения должно быть целым неотрицательным числом");
                return;
            }

            try
            {
                await _facade.AdminDeleteMessageAsync(_userID, messageID);
            }
            catch (MessageDeleteException ex)
            {
                _exceptionView.ShowException(ex, "Сообщение с таким ID не найдено");
            }
            await GetMessagesAsync(sender, e);
        }

        private async Task MarkMessageCheckedAsync(object sender, EventArgs e)
        {
            string stringMessageID = _messageView.MessageID;
            uint messageID;
            try
            {
                messageID = Convert.ToUInt32(stringMessageID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID сообщения должно быть целым неотрицательным числом");
                return;
            }

            try
            {
                await _facade.MarkMessageReadByUserAsync(_userID, messageID);
            }
            catch (MessageNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Сообщение с таким ID не найдено");
            }
            catch (MessageCheckingException ex)
            {
                _exceptionView.ShowException(ex, "Это сообщение уже проверено другим пользователем");
            }
            await GetMessagesAsync(sender, e);
        }


















        // CardReading
        public void OnCardReadingClicked(object sender, EventArgs e)
        {
            if (_cardReadingView is not null)
            {
                return;
            }
            _cardReadingView = _viewsFactory.CreateCardReadingView();

            _cardReadingView.GetCardReadingClicked += GetCardReadingAsync;
            _cardReadingView.GetCardReadingsClicked += GetCardReadingsAsync;
            _cardReadingView.UpdateClicked += UpdateCardReadingAsync;
            _cardReadingView.AddClicked += AddCardReadingAsync;
            _cardReadingView.DeleteClicked += DeleteCardReadingAsync;
            _cardReadingView.CloseClicked += OnCardReadingCloseClicked;
        _changeVisibilityForViews();
            _cardReadingView.Open();
        }

        private void OnCardReadingCloseClicked(object sender, EventArgs e)
        {
            _cardReadingView = null;
        }

        private async Task GetCardReadingsAsync(object sender, EventArgs e)
        {
            List<CardReading> cardReadings = await _facade.AdminGetCardReadingsAsync(_userID);
            _cardReadingView.CardReadings = cardReadings;
        }

        private async Task GetCardReadingAsync(object sender, EventArgs e)
        {
            string stringRecordID = _cardReadingView.RecordID;
            uint recordID;
            try
            {
                recordID = Convert.ToUInt32(stringRecordID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID записи должно быть целым неотрицательным числом");
                return;
            }

            try
            {
                CardReading cardReading = await _facade.AdminGetCardReadingAsync(_userID, recordID);
                _cardReadingView.CardReadings = new List<CardReading>() { cardReading };
            }
            catch (CardReadingNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Чтение с таким ID не найдено");
            }
        }

        private async Task UpdateCardReadingAsync(object sender, EventArgs e)
        {
            string stringRecordID = _cardReadingView.RecordID;
            string stringTurnstileID = _cardReadingView.TurnstileID;
            string stringCardID = _cardReadingView.CardID;
            uint recordID, turnstileID, cardID;
            try
            {
                recordID = Convert.ToUInt32(stringRecordID);
                turnstileID = Convert.ToUInt32(stringTurnstileID);
                cardID = Convert.ToUInt32(stringCardID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID записи, турникета и карты должны быть целыми неотрицательными числами");
                return;
            }

            DateTimeOffset readingTime = _cardReadingView.ReadingTime;

            try
            {
                await _facade.AdminUpdateCardReadingAsync(_userID, recordID, turnstileID, cardID, readingTime);
            }
            catch (CardReadingNotFoundException ex)
            {
                _exceptionView.ShowException(ex, "Чтение с таким ID не найдено");
            }
            await GetCardReadingAsync(sender, e);
        }
        private async Task AddCardReadingAsync(object sender, EventArgs e)
        {
            string stringTurnstileID = _cardReadingView.TurnstileID;
            string stringCardID = _cardReadingView.CardID;
            uint recordID, turnstileID, cardID;
            try
            {
                turnstileID = Convert.ToUInt32(stringTurnstileID);
                cardID = Convert.ToUInt32(stringCardID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID турникета и карты должны быть целыми неотрицательными числами");
                return;
            }

            DateTimeOffset readingTime = _cardReadingView.ReadingTime;

            try
            {
                uint cardReadingID = await _facade.AdminAddAutoIncrementCardReadingAsync(_userID, turnstileID, cardID, readingTime);
                _cardReadingView.RecordID = Convert.ToString(cardReadingID);
                await GetCardReadingAsync(sender, e);
            }
            catch (CardReadingAddAutoIncrementException ex)
            {
                _exceptionView.ShowException(ex, "Не удалось добавить чтение");
            }
        }

        private async Task DeleteCardReadingAsync(object sender, EventArgs e)
        {
            string stringRecordID = _cardReadingView.RecordID;
            uint recordID;
            try
            {
                recordID = Convert.ToUInt32(stringRecordID);
            }
            catch (Exception ex)
            {
                _exceptionView.ShowException(ex, "ID чтения должно быть целым неотрицательным числом");
                return;
            }

            try
            {
                await _facade.AdminDeleteCardReadingAsync(_userID, recordID);
            }
            catch (CardReadingDeleteException ex)
            {
                _exceptionView.ShowException(ex, "Чтение с таким ID не найдено");
            }
            await GetCardReadingsAsync(sender, e);
        }

    }

}