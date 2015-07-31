using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    public partial class RoomViewModel
    {
        public SecureCommand ReserveRoomCommand { get; set; }

        private void ReserveRoomCommandExecute(SecureCommandArgs args)
        {
            
        }

        private bool CanReserveRoomCommandExecute(SecureCommandArgs args)
        {
            return true;
        }

        public SecureCommand RecommendRoomCommand { get; set; }

        private void RecommendRoomCommandExecute(SecureCommandArgs args)
        {

        }

        private bool CanRecommendRoomCommandExecute(SecureCommandArgs args)
        {
            return true;
        }

        protected override void InitializeCommand()
        {
            ReserveRoomCommand = new SecureCommand(ReserveRoomCommandExecute, CanReserveRoomCommandExecute);
            RecommendRoomCommand = new SecureCommand(RecommendRoomCommandExecute, CanRecommendRoomCommandExecute);
            base.InitializeCommand();
        }
    }
}
