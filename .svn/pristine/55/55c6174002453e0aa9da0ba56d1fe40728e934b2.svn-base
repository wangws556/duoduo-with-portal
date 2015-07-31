using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class BlockTypeModel:JsonModel
    {
        public int? Days { get; set; }

        public BlockTypeModel() { }

        public BlockTypeModel(BlockType blockType)
            : base(blockType)
        {
            if (blockType != null)
            {
                Name = blockType.Name;
                Days = blockType.Days;
                Description = blockType.Description;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new BlockType
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Days = Days
            };
        }
    }

    [Serializable]
    public class BlockListModel : JsonModel
    {
        public int User_Id { get; set; }
        public int Application_Id { get; set; }
        public int BlockType_Id { get; set; }
        public string Content { get; set; }

        public BlockListModel() { }

        public BlockListModel(BlockList blockList)
            :base(blockList)
        {
            if(blockList != null)
            {
                Application_Id = blockList.Application_Id;
                BlockType_Id = blockList.BlockType_Id;
                Content = blockList.Content;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new BlockList
            {
                Application_Id = Application_Id,
                BlockType_Id = BlockType_Id,
                Content = Content
            };
        }
    }

    [Serializable]
    public class BlockHistoryModel : JsonModel
    {
        public int OptUser_Id { get; set; }
        public int BlockUser_Id { get; set; }
        public DateTime Time { get; set; }
        public int Application_Id { get; set; }
        public int BlockType_Id { get; set; }
        public int IsBlock { get; set; }
        public string Content { get; set; }

        public BlockHistoryModel() { }
        public BlockHistoryModel(BlockHistory blockHistory)
            : base(blockHistory)
        {
            if (blockHistory != null)
            {
                OptUser_Id = blockHistory.OptUser_Id;
                Time = blockHistory.Time;
                Application_Id = blockHistory.Application_Id;
                BlockType_Id = blockHistory.BlockType_Id;
                IsBlock = blockHistory.IsBlock?1:0;
                Content = blockHistory.Content;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new BlockHistory
            {
                OptUser_Id = OptUser_Id,
                Time = Time,
                Application_Id = Application_Id,
                BlockType_Id = BlockUser_Id,
                IsBlock = IsBlock==1,
                Content = Content
            };
        }
    }
}