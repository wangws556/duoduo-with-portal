using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model
{
    public class Applications
    {
        public class _9258App 
        {
            public static bool CommandNeedsBroadcast(int cmdId)
            {
                switch (cmdId)
                {
                    case UserCommands.KickOutOfRoomCommandId:
                    case UserCommands.BlockHornHallHornCommandId:
                    case UserCommands.BlockUserIdCommandId:
                    case UserCommands.BlockUserIpCommandId:
                    case UserCommands.BlockUserMacCommandId:
                    case UserCommands.AllowConnectPrivateMicCommandId:
                    case UserCommands.AllowConnectSecretMicCommandId:
                        return false;
                    default:
                        return true;
                }
            }
            public class FrontendCommands
            {
                public const int EnterVIPRoomCommandId = 10001;
                public const int EnterFullRoomCommandId = 10002;
                public const int EnterClosedRoomCommandId = 10003;
                public const int SendGiftCommandId = 10004;
                public const int ReceiveGiftCommandId = 10005;
                public const int ScoreExchangeCashCommandId = 10006;
                public const int ScoreExchangeMoneyCommandId = 10007;
                public const int MoneyExchangeGameMoneyCommandId = 10008;
                public const int MoneyExchangeQQMoneyCommandId = 10009;
                public const int FreePulicMicCommandId = 10010;
                public const int FreePrivateMicCommandId = 10011;
                public const int RoomInfoCommandId = 10012;
                public const int ConfigureRoomCommandId = 10013;
                public const int PublishRoomAnnoucementCommandId = 10014;
                public const int CheckIpCommandId = 10015;
                public const int CheckPrivateChatCommandId = 10016;
                public const int CheckScoreMoneyCommandId = 10017;
                public const int HideInRoomCommandId = 10018;
                public const int BlockMACCommandId = 10019;
                public const int DisableRoomVideoCommandId = 10020;
                public const int DisableRoomChatCommandId = 10021;
                public const int DisableHornVideoChatCommandId = 10022;
                public const int HornCommandId = 10023;
                public const int HallHornCommandId = 10024;
                public const int GlobalHornCommandId = 10025;
                public const int FreeConnectPrivateSecretMicCommandId = 10026;
                public const int EnterRoomHideCommandId = 10027;
                public const int FontChangeIfManagerCommandId = 10028;
                public const int ConfinementCommandId = 10029;

                //Normal Commands
                public const int OnMicCommandId = 1010001;
            }

            public class UserCommands
            {
                public const int KickOutOfRoomCommandId = 10030;
                public const int BlockUserIpCommandId = 10031;
                public const int BlockUserIdCommandId = 10032;
                public const int BlockUserMacCommandId = 10033;
                public const int UpDownUserPublicMicCommandId = 10034;
                public const int UpDownUserPrivateMicCommandId = 10035;
                public const int AddToBlackListCommandId = 10036;
                public const int AllowConnectPrivateMicCommandId = 10037;
                public const int AllowConnectSecretMicCommandId = 10038;
                public const int SetOrCancelRoomManagerCommandId = 10039;
                public const int BlockHornHallHornCommandId = 10040;
                public const int UpDownTenPeopleRoomPublicMicCommandId = 10041;
                public const int KickOutOfTenPeopleRoomCommandId = 10042;
                public const int DisableChatInTenPeopleRoomCommandId = 10043;
            }
            
        }
    }
}
